using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace PoshSec.Framework.Web
{
    class GithubJsonItem
    {
        #region Private Variables
        private string _name = "";
        private string _path = "";
        private string _sha = "";
        private string _size = "";
        private string _url = "";
        private string _html_url = "";
        private string _git_url = "";
        private string _type = "";
        private string _content = "";
        private string _encoding = "";
        private string _date = "";
        #endregion

        #region Public Methods
        public GithubJsonItem(String json)
        {
            json = json.Replace("{", "").Replace("}", "");
            string[] elements = json.Split(',');
            if (elements != null && elements.Count() > 0)
            {
                foreach (string element in elements)
                {
                    AssignValue(element);
                }
            }
        }
        #endregion

        #region Private Methods
        private void AssignValue(string element)
        {
            String[] split = new String[] { "\":" };
            string[] elemvalue = element.Split(split, StringSplitOptions.None);
            if (elemvalue != null && elemvalue.Count() > 0)
            {
                FieldInfo[] fields = typeof(GithubJsonItem).GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                if (fields != null && fields.Count() > 0)
                {
                    bool found = false;
                    int idx = 0;
                    do
                    {
                        FieldInfo field = fields[idx];
                        if (field.Name.Replace("_", "") == elemvalue[0].Replace("_", "").Replace("\"", ""))
                        {
                            found = true;
                            field.SetValue(this, elemvalue[1].Replace("\"", ""));
                        }
                        idx++;
                    } while (!found && idx < fields.Count());
                }
            }
        }
        #endregion

        #region Public Properties
        public string Name { get { return _name; } }
        public string Path { get { return _path; } }
        public string SHA { get { return _sha; } }
        public string Size { get { return _size; } }
        public string URL { get { return _url; } }
        public string HTML_URL { get { return _html_url; } }
        public string Git_URL { get { return _git_url; } }
        public string Type { get { return _type; } }
        public string Content { get { return _content; } }
        public string Encoding { get { return _encoding; } }
        public string Date { get { return _date; } }
        #endregion
    }
}
