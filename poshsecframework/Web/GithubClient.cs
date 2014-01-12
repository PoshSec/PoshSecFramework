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

        public String GetReadMe(String OwnerName, String RepositoryName)
        {
            return GetContent(OwnerName, RepositoryName, "readme");
        }

        public String GetContent(String OwnerName, String RepositoryName, String FileName)
        {
            errors.Clear();
            String rtn = "";
            GithubJsonItem ghi = Get(Path.Combine(StringValue.GithubURI, String.Format(StringValue.FileFormat, OwnerName, RepositoryName, FileName)));
            if (ghi != null)
            {
                rtn = Decode(ghi.Content);
            }            
            return rtn;
        }

        public GithubRepository GetRepository(String OwnerName, String RepositoryName)
        {
            GithubRepository repo = null;
            return repo;
        }

        public FileInfo SaveFile(String OwnerName, String RepositoryName, String FileName, String TargetDirectory)
        {
            errors.Clear();
            FileInfo rtn = null;
            if (!Directory.Exists(TargetDirectory))
            { 
                Directory.CreateDirectory(TargetDirectory);
            }
            GithubJsonItem ghi = Get(Path.Combine(StringValue.GithubURI, String.Format(StringValue.FileFormat, OwnerName, RepositoryName, FileName)));
            if (ghi != null)
            {
                String path = Path.Combine(TargetDirectory, ghi.Path);
                StreamWriter sr = File.CreateText(path);
                if (ghi.Encoding == "base64")
                {
                    sr.Write(Decode(ghi.Content));
                }
                else
                {
                    sr.Write(ghi.Content);
                }
                sr.Close();
                rtn = new FileInfo(path);
            }            
            return rtn;
        }

        private GithubJsonItem Get(String uri)
        {
            GithubJsonItem rtn = null;
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
                        rtn = new GithubJsonItem(response);
                        ghrs.Close();
                        ghrs = null;
                    }
                }
                ghr.Close();
                ghr = null;
            }
            return rtn;
        }

        private String Decode(String encodedstring)
        {
            String rtn = "";
            try
            {
                UTF8Encoding enc = new UTF8Encoding();
                Decoder dec = enc.GetDecoder();
                byte[] bytcode = Convert.FromBase64String(encodedstring.Replace("\\n",""));
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

        public List<String> Errors
        {
            get { return errors; }
        }
    }
}
