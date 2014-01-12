using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace poshsecframework.Web
{
    class GithubContentItem
    {
        private String _filename;
        private String _type;
        private String _url;

        public String Filename
        {
            get { return _filename; }
        }

        public String Type
        {
            get { return _type; }
        }

        public String URL
        {
            get { return _url; }
        }
    }
}
