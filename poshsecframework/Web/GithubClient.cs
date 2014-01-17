using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Runtime.Serialization.Json;
using poshsecframework.Strings;

namespace poshsecframework.Web
{
    class GithubClient
    {
        #region Private Variables
        HttpWebRequest ghc = null;
        List<String> errors = new List<string>();
        #endregion

        #region Public Methods
        public Collection<String> GetBranches(String OwnerName, String RepositoryName)
        {
            Collection<String> rtn = new Collection<String>();
            Collection<GithubJsonItem> ghi = Get(Path.Combine(StringValue.GithubURI, String.Format(StringValue.BranchFormat, OwnerName, RepositoryName)));
            if (ghi != null)
            {
                foreach (GithubJsonItem gitem in ghi)
                {
                    rtn.Add(gitem.Name);
                }
            }
            return rtn;
        }

        public void GetArchive(String OwnerName, String RepositoryName, String Branch, String ModuleDirectory)
        {
            String tmpfile = Path.GetTempFileName();
            FileInfo savedfile = Download(Path.Combine(StringValue.GithubURI, String.Format(StringValue.ArchiveFormat, OwnerName, RepositoryName, Branch)), tmpfile);
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
                            String parentfolder = za.Entries[0].FullName;
                            System.IO.Compression.ZipFile.ExtractToDirectory(savedfile.FullName, ModuleDirectory);
                            String newfolder = Path.Combine(ModuleDirectory, parentfolder);
                            if (Directory.Exists(newfolder))
                            {
                                DirectoryInfo di = new DirectoryInfo(newfolder);
                                if (Directory.Exists(target))
                                {
                                    Directory.Delete(target, true);
                                }
                                di.MoveTo(target);
                                di = null;
                            }
                        }                        
                    }
                    za = null; 
                }
                catch (Exception ex)
                {
                    errors.Add(ex.Message);
                }
            } 
            try
            {
                File.Delete(savedfile.FullName);
            }
            catch (Exception dex)
            {
                errors.Add(dex.Message);
            
            }
            savedfile = null;
        }
        #endregion

        #region Private Methods
        private FileInfo Download(String uri, String targetfile)
        {
            FileInfo rtn = null;
            ghc = (HttpWebRequest)WebRequest.Create(uri);
            ghc.UserAgent = StringValue.psftitle;
            WebResponse ghr = null;
            try
            {
                ghr = ghc.GetResponse();
            }
            catch (Exception e)
            {
                errors.Add(uri + ":" + e.Message);
            }
            if (ghr != null)
            {
                try
                {
                    Stream ghrs = ghr.GetResponseStream();
                    int pos = 0;
                    byte[] bytes = new byte[(ghr.ContentLength)];
                    while (pos < bytes.Length)
                    {
                        int bytread = ghrs.Read(bytes, pos, bytes.Length - pos);
                        pos += bytread;
                        //UpdateProgressHere
                    }
                    ghrs.Close();

                    Stream str = new FileStream(targetfile, FileMode.Create);
                    BinaryWriter wtr = new BinaryWriter(str);
                    wtr.Write(bytes);
                    wtr.Flush();
                    wtr.Close();
                    str.Close();
                    wtr = null;
                    str = null;
                    rtn = new FileInfo(targetfile);
                }
                catch (Exception wex)
                {
                    errors.Add(uri + ":" + wex.Message);
                }
            }
            return rtn;
        }

        private Collection<GithubJsonItem> Get(String uri)
        {
            Collection<GithubJsonItem> rtn = new Collection<GithubJsonItem>();
            ghc = (HttpWebRequest)WebRequest.Create(uri);
            ghc.UserAgent = StringValue.psftitle;
            WebResponse ghr = null;
            try
            {
                ghr = ghc.GetResponse();
            }
            catch (Exception e)
            {
                errors.Add(uri + ":" + e.Message);
            }
            if (ghr != null)
            {
                if (ghr.ContentType == StringValue.ContentTypeJSON)
                {
                    Stream ghrs = ghr.GetResponseStream();
                    if (ghrs != null)
                    {
                        StreamReader ghrdr = new StreamReader(ghrs);
                        String response = ghrdr.ReadToEnd();
                        ghrdr.Close();
                        ghrdr = null;
                        String name = "\"name\"";
                        String[] split = new string[] { "\"name\"" };
                        String[] items = response.Split(split, StringSplitOptions.None);
                        if (items != null && items.Count() > 0)
                        {
                            foreach (String resp in items)
                            {
                                if (resp != "[{" && resp != "{")
                                {
                                    GithubJsonItem gjson = new GithubJsonItem(name + resp);
                                    rtn.Add(gjson);
                                }
                            }
                        }
                        ghrs.Close();
                        ghrs = null;
                    }
                }
                ghr.Close();
                ghr = null;
            }
            return rtn;
        }

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
                errors.Add("Decode failed: " + e.Message);
                rtn = null;
            }
            return rtn;
        }
        #endregion

        #region Public Properties
        public List<String> Errors
        {
            get { return errors; }
        }
        #endregion        
    }
}
