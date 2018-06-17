using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using PoshSec.Framework.Strings;

namespace PoshSec.Framework.Web
{
    /// <summary>
    /// A WebRequest wrapper class for the Github API.
    /// </summary>
    class GithubClient
    {
        #region Private Variables

        private List<String> _errors = new List<string>();
        private int _ratelimitremaining = 0;
        private string _lastmodified = "";
        private bool _restart = false;
        private string _token = "";

        #endregion

        public GithubClient()
        {
            if (Properties.Settings.Default.GithubAPIKey.Trim() != "")
            {
                _token = String.Format(StringValue.AccessToken, Properties.Settings.Default.GithubAPIKey.Trim());
            }
        }

        #region Public Methods
        /// <summary>
        /// Requests the JSON response from the Github API for a list of available branches for the given repository.
        /// </summary>
        /// <param name="OwnerName">The owner of the repository.</param>
        /// <param name="RepositoryName">The name of the repository.</param>
        /// <returns></returns>
        public Collection<GithubJsonItem> GetBranches(String OwnerName, String RepositoryName)
        {
            Collection<GithubJsonItem> ghi = Get(Path.Combine(StringValue.GithubURI, String.Format(StringValue.BranchFormat, System.Net.WebUtility.UrlEncode(OwnerName), System.Net.WebUtility.UrlEncode(RepositoryName)) + _token));
            return ghi;
        }

        public String GetLastModified(String OwnerName, String RepositoryName, String BranchName, String LastModified)
        {
            string branch = "?sha=" + BranchName;
            if (_token != "")
            {
                branch = "&sha=" + BranchName;
            }
            String url = Path.Combine(StringValue.GithubURI, String.Format(StringValue.LastModifiedFormat, System.Net.WebUtility.UrlEncode(OwnerName), System.Net.WebUtility.UrlEncode(RepositoryName)) + _token + branch);
            GetLastModDate(url, LastModified);
            return _lastmodified;
        }

        /// <summary>
        /// Downloads the zipball of the specified repository and branch and unzips it to the Module Directory.
        /// </summary>
        /// <param name="OwnerName">The owner of the repository.</param>
        /// <param name="RepositoryName">The name of the repository.</param>
        /// <param name="BranchName">The selected branch item.</param>
        /// <param name="ModuleDirectory">The target directory for the zipball to be extracted into.</param>
        public void GetArchive(String OwnerName, String RepositoryName, String BranchName, String ModuleDirectory)
        {
            String tmpfile = Path.GetTempFileName();
            FileInfo savedfile = Download(Path.Combine(StringValue.GithubURI, String.Format(StringValue.ArchiveFormat, System.Net.WebUtility.UrlEncode(OwnerName), System.Net.WebUtility.UrlEncode(RepositoryName), BranchName) + _token), tmpfile);
            if (savedfile != null)
            {
                try
                {
                    String target = Path.Combine(ModuleDirectory, RepositoryName);
                    System.IO.Compression.ZipArchive za = System.IO.Compression.ZipFile.Open(savedfile.FullName, System.IO.Compression.ZipArchiveMode.Read);
                    if (za != null && za.Entries.Count() > 0)
                    {
                        using (za)
                        {
                            if (IsValidPSModule(za))
                            {
                                String parentfolder = za.Entries[0].FullName;
                                System.IO.Compression.ZipFile.ExtractToDirectory(savedfile.FullName, ModuleDirectory);
                                String newfolder = Path.Combine(ModuleDirectory, parentfolder);
                                if (Directory.Exists(newfolder))
                                {
                                    DirectoryInfo di = new DirectoryInfo(newfolder);
                                    _restart = false;
                                    try
                                    {
                                        if (Directory.Exists(target))
                                        {
                                            Directory.Delete(target, true);
                                        }
                                    }
                                    catch
                                    {
                                        _restart = true;
                                    }
                                    if (!_restart)
                                    {
                                        di.MoveTo(target);
                                    }
                                    else
                                    {
                                        StreamWriter wtr = File.AppendText(Path.Combine(Properties.Settings.Default.ModulePath, StringValue.ModRestartFilename));
                                        wtr.WriteLine(parentfolder + ">>" + RepositoryName);
                                        wtr.Flush();
                                        wtr.Close();
                                    }
                                    di = null;
                                }
                            }
                            else
                            {
                                _errors.Add(String.Format(StringValue.InvalidPSModule, RepositoryName, BranchName));
                            }
                        }
                    }
                    za = null;
                }
                catch (Exception ex)
                {
                    _errors.Add(ex.Message);
                }
            }
            try
            {
                File.Delete(savedfile.FullName);
            }
            catch (Exception dex)
            {
                _errors.Add(dex.Message);

            }
            savedfile = null;
        }

        public void GetPSFScripts(String ScriptDirectory)
        {
            String tmpfile = Path.GetTempFileName();
            if (new DirectoryInfo(ScriptDirectory).GetFiles("*", SearchOption.AllDirectories).Count() > 0)
            {
                try
                {
                    Directory.Delete(ScriptDirectory, true);
                    Directory.CreateDirectory(ScriptDirectory);
                }
                catch
                {
                    if (Directory.Exists(ScriptDirectory) == false)
                    {
                        Directory.CreateDirectory(ScriptDirectory);
                    }
                }
            }
            FileInfo savedfile = Download(Path.Combine(StringValue.GithubURI, StringValue.PSFScriptsPath + _token), tmpfile);
            if (savedfile != null)
            {
                try
                {
                    String target = ScriptDirectory;
                    System.IO.Compression.ZipArchive za = System.IO.Compression.ZipFile.Open(savedfile.FullName, System.IO.Compression.ZipArchiveMode.Read);
                    if (za != null && za.Entries.Count() > 0)
                    {
                        using (za)
                        {
                            String parentfolder = za.Entries[0].FullName;
                            System.IO.Compression.ZipFile.ExtractToDirectory(savedfile.FullName, ScriptDirectory);
                            String newfolder = Path.Combine(ScriptDirectory, parentfolder);
                            newfolder = newfolder.Substring(0, newfolder.Length - 1); // removes trailing slash
                            if (Directory.Exists(newfolder))
                            {
                                DirectoryInfo di = new DirectoryInfo(newfolder);
                                _restart = false;
                                foreach (FileInfo fil in di.GetFiles("*", SearchOption.AllDirectories))
                                {
                                    String dest = target + fil.DirectoryName.Replace(newfolder, "");
                                    if (!Directory.Exists(dest))
                                    {
                                        Directory.CreateDirectory(dest);
                                    }
                                    File.Copy(fil.FullName, Path.Combine(dest, fil.Name));
                                }
                                di.Delete(true);
                                di = null;
                            }
                        }
                    }
                    za = null;
                }
                catch (Exception ex)
                {
                    _errors.Add(ex.Message);
                }
            }
            try
            {
                File.Delete(savedfile.FullName);
            }
            catch (Exception dex)
            {
                _errors.Add(dex.Message);

            }
            savedfile = null;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Checks to see if the downloaded zipball contains a psd1 file in the root folder. This is required for the powershell modules.
        /// </summary>
        /// <param name="zaitem">The ZipArchive item of the zipball.</param>
        /// <returns>True if the zipball contains a psd1 file. Default is false.</returns>
        private bool IsValidPSModule(System.IO.Compression.ZipArchive zaitem)
        {
            bool rtn = false;
            int idx = 0;
            if (zaitem != null)
            {
                string rootfolder = zaitem.Entries[0].FullName;
                do
                {
                    System.IO.Compression.ZipArchiveEntry zaentry = zaitem.Entries[idx];
                    string filename = zaentry.FullName.Replace(rootfolder, "");
                    if (!filename.Contains("/") && filename.Contains(".psd1"))
                    {
                        rtn = true;
                    }
                    idx++;
                } while (!rtn && idx < zaitem.Entries.Count());
            }
            return rtn;
        }

        /// <summary>
        /// Downloads the specific file from the Github API.
        /// </summary>
        /// <param name="uri">The url of the file to download.</param>
        /// <param name="targetfile">The local filename to use.</param>
        /// <returns></returns>
        private FileInfo Download(String uri, String targetfile)
        {
            FileInfo rtn = null;

            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Timeout = 5000;
            request.UserAgent = StringValue.psftitle;
            
            WebResponse response = null;
            try
            {
                response = request.GetResponse();
            }
            catch (Exception e)
            {
                _errors.Add(uri + ":" + e.Message);
            }
            if (response != null)
            {
                Stream ghrs = response.GetResponseStream();
                if (ghrs != null && ghrs.CanRead)
                {
                    Stream str = new FileStream(targetfile, FileMode.Create);
                    ghrs.CopyTo(str);
                    str.Flush();
                    str.Close();
                    str = null;
                    rtn = new FileInfo(targetfile);
                }
                else
                {
                    _errors.Add(uri + ":" + "Failed to get stream. Please check your internet connection and try again.");
                }
            }
            else
            {
                _errors.Add(uri + ":" + "Failed to get stream. Please check your internet connection and try again.");
            }
            return rtn;
        }

        private void GetLastModDate(string uri, string lastModifiedDate)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.UserAgent = StringValue.psftitle;
            request.Timeout = 5000;

            if (DateTime.TryParse(lastModifiedDate, out DateTime lmd) && lmd.Year > 1)
                request.IfModifiedSince = lmd;

            request.AllowAutoRedirect = true;
            WebResponse response = null;
            try
            {
                response = request.GetResponse();
            }
            catch (WebException wex)
            {
                _ratelimitremaining = GetRateLimitRemaining(wex.Response);
                if (wex.Response != null && wex.Response.Headers["Status"] == StringValue.NotModified)
                {
                    _lastmodified = lastModifiedDate;
                }
            }
            catch (Exception e)
            {
                _errors.Add(uri + ":" + e.Message);
            }
            if (response != null)
            {
                _ratelimitremaining = GetRateLimitRemaining(response);
                _lastmodified = GetLastModified(response);
                response.Close();
            }
            request = null;
        }

        /// <summary>
        /// Performs a Get WebRequest to the specified URL and returns a collection of GithubJsonItems.
        /// </summary>
        /// <param name="uri">The url for the Get Request.</param>
        /// <returns></returns>
        private Collection<GithubJsonItem> Get(String uri)
        {
            Collection<GithubJsonItem> rtn = new Collection<GithubJsonItem>();

            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.UserAgent = StringValue.psftitle;
            request.AllowAutoRedirect = true;

            WebResponse response = null;
            try
            {
                response = request.GetResponse();
            }
            catch (WebException wex)
            {
                _ratelimitremaining = GetRateLimitRemaining(wex.Response);
                _errors.Add(uri + ":" + wex.Message);
            }
            catch (Exception e)
            {
                _errors.Add(uri + ":" + e.Message);
            }
            if (response != null)
            {
                _ratelimitremaining = GetRateLimitRemaining(response);
                _lastmodified = GetLastModified(response);

                if (response.ContentType == StringValue.ContentTypeJSON)
                {
                    var responseStream = response.GetResponseStream();
                    if (responseStream != null)
                    {
                        var responseReader = new StreamReader(responseStream);
                        var responseString = responseReader.ReadToEnd();
                        responseReader.Close();
                        responseReader = null;
                        var name = "\"name\"";
                        var split = new string[] { "\"name\"" };
                        var items = responseString.Split(split, StringSplitOptions.None);
                        if (items != null && items.Count() > 0)
                        {
                            foreach (var resp in items)
                            {
                                if (resp != "[{" && resp != "{")
                                {
                                    GithubJsonItem gjson = new GithubJsonItem(name + resp);
                                    rtn.Add(gjson);
                                }
                            }
                        }
                        responseStream.Close();
                        responseStream = null;
                    }
                }
                response.Close();
                response = null;
            }
            return rtn;
        }

        private int GetRateLimitRemaining(WebResponse response)
        {
            int rtn = 0;
            if (response != null && response.Headers != null && response.Headers.Keys.Count > 0)
            {
                int idx = 0;
                bool found = false;
                do
                {
                    if (response.Headers.Keys[idx] == StringValue.RateLimitKey)
                    {
                        found = true;
                    }
                    else
                    {
                        idx++;
                    }
                } while (!found && idx < response.Headers.Keys.Count);
                if (found)
                {
                    string[] vals = response.Headers.GetValues(idx);
                    if (vals != null)
                    {
                        string val = vals[0];
                        int.TryParse(val, out rtn);
                    }
                }
            }
            return rtn;
        }

        private String GetLastModified(WebResponse response)
        {
            String rtn = "";
            if (response.Headers.Keys.Count > 0)
            {
                int idx = 0;
                bool found = false;
                do
                {
                    if (response.Headers.Keys[idx] == StringValue.LastModifiedKey)
                    {
                        found = true;
                    }
                    else
                    {
                        idx++;
                    }
                } while (!found && idx < response.Headers.Keys.Count);
                if (found)
                {
                    string[] vals = response.Headers.GetValues(idx);
                    if (vals != null)
                    {
                        rtn = vals[0];
                    }
                }
            }
            return rtn;
        }

        /// <summary>
        /// Decodes the base64 encoded content.
        /// </summary>
        /// <param name="encodedstring">The base64 encoded content.</param>
        /// <returns></returns>
        private byte[] Decode(String encodedstring)
        {
            byte[] rtn = null;
            try
            {
                UTF8Encoding enc = new UTF8Encoding();
                Decoder dec = enc.GetDecoder();
                rtn = Convert.FromBase64String(encodedstring.Replace("\\n", ""));
            }
            catch (Exception e)
            {
                _errors.Add("Decode failed: " + e.Message);
                rtn = null;
            }
            return rtn;
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// List of any errors that may have occured during any request.
        /// </summary>
        public List<String> Errors
        {
            get { return _errors; }
        }

        public int RateLimitRemaining
        {
            get { return _ratelimitremaining; }
        }

        public string LastModified
        {
            get { return _lastmodified; }
        }

        public bool Restart
        {
            get { return _restart; }
        }

        #endregion        
    }
}
