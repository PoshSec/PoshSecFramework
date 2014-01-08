using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Runtime.Serialization.Json;
using poshsecframework.Strings;

namespace poshsecframework.Web
{
    static class GithubClient
    {
        #region Private Variables
        static HttpWebRequest ghc = null;
        #endregion

        public static String GetReadMe(String OwnerName, String RepositoryName)
        {
            String rtn = Get(Path.Combine(StringValue.GithubURI, String.Format(StringValue.ReadmeFormat, OwnerName, RepositoryName)));            
            return rtn;
        }

        public static String GetContent(String OwnerName, String RepositoryName, String FileName)
        {
            String rtn = Get(Path.Combine(StringValue.GithubURI, String.Format(StringValue.FileFormat, OwnerName, RepositoryName, FileName)));
            return rtn;
        }

        private static String Get(String uri)
        {
            String rtn = "";
            ghc = (HttpWebRequest)WebRequest.Create(uri);
            ghc.UserAgent = StringValue.psftitle;
            WebResponse ghr = ghc.GetResponse();
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
                        rtn = ParseJson(response);
                        ghrs.Close();
                        ghrs = null;
                    }
                }
                ghr.Close();
                ghr = null;
            }
            return rtn;
        }

        private static String ParseJson(String json)
        {
            String rtn = "";

            return rtn;
        }
    }
}
