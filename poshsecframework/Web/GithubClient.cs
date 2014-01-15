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
        public String GetReadMe(String OwnerName, String RepositoryName, String Branch = StringValue.DefaultBranch)
        {
            return GetContent(OwnerName, RepositoryName, "readme", Branch);
        }

        public String GetContent(String OwnerName, String RepositoryName, String PathtoFile, String Branch = StringValue.DefaultBranch)
        {
            errors.Clear();
            String rtn = "";
            Collection<GithubJsonItem> ghi = Get(Path.Combine(StringValue.GithubURI, String.Format(StringValue.FileFormat, OwnerName, RepositoryName, PathtoFile, Branch)));
            if (ghi != null)
            {
                rtn = Decode(ghi[0].Content);
            }
            return rtn;
        }

        public GithubRepository GetRepository(String OwnerName, String RepositoryName, String Branch = StringValue.DefaultBranch)
        {
            GithubRepository repo = new GithubRepository();
            Collection<GithubJsonItem> ghr = Get(Path.Combine(StringValue.GithubURI, String.Format(StringValue.ContentsFormat, OwnerName, RepositoryName, "/", Branch)));
            if (ghr != null)
            {
                RecurseRepository(repo, ghr);
            }
            return repo;
        }

        public Collection<FileInfo> SaveFile(String GitURL, String TargetDirectory)
        {
            errors.Clear();
            Collection<FileInfo> rtn = new Collection<FileInfo>();
            if (!Directory.Exists(TargetDirectory))
            {
                Directory.CreateDirectory(TargetDirectory);
            }
            Collection<GithubJsonItem> ghi = Get(GitURL);
            if (ghi != null)
            {
                foreach (GithubJsonItem ghitem in ghi)
                {
                    String path = Path.Combine(TargetDirectory, ghitem.Path);
                    StreamWriter sr = File.CreateText(path);
                    if (ghitem.Encoding == "base64")
                    {
                        sr.Write(Decode(ghitem.Content));
                    }
                    else
                    {
                        sr.Write(ghitem.Content);
                    }
                    sr.Close();
                    rtn.Add(new FileInfo(path));
                }
            }
            return rtn;
        }

        public Collection<FileInfo> SaveFile(String OwnerName, String RepositoryName, String PathToFile, String TargetDirectory, String Branch = StringValue.DefaultBranch)
        {
            String giturl = Path.Combine(StringValue.GithubURI, String.Format(StringValue.FileFormat, OwnerName, RepositoryName, PathToFile, Branch));
            return SaveFile(giturl, TargetDirectory);
        }
        #endregion

        #region Private Methods
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
                                if (resp != "[{")
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

        private void RecurseRepository(GithubRepository repo, Collection<GithubJsonItem> ghi)
        {
            if (ghi != null && ghi.Count() > 0)
            {
                foreach (GithubJsonItem ghitem in ghi)
                {
                    repo.Content.Add(ghitem);
                    if (ghitem.Type == "dir")
                    {
                        Collection<GithubJsonItem> ghdiritms = Get(ghitem.URL);
                        RecurseRepository(repo, ghdiritms);
                    }
                }
            }
        }

        private String Decode(String encodedstring)
        {
            String rtn = "";
            try
            {
                UTF8Encoding enc = new UTF8Encoding();
                Decoder dec = enc.GetDecoder();
                byte[] bytcode = Convert.FromBase64String(encodedstring.Replace("\\n", ""));
                char[] decchars = new char[dec.GetCharCount(bytcode, 0, bytcode.Length)];
                dec.GetChars(bytcode, 0, bytcode.Length, decchars, 0);
                rtn = new String(decchars);
            }
            catch (Exception e)
            {
                errors.Add("Decode failed: " + e.Message);
                rtn = encodedstring;
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
