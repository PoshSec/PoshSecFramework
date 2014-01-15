using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace poshsecframework.Web
{
    class GithubRepository
    {
        #region Private Variables
        private Collection<String> _branches = new Collection<string>();
        private Collection<GithubJsonItem> _content = new Collection<GithubJsonItem>();
        #endregion

        #region Public Properties
        public Collection<String> Branches
        {
            get { return _branches; }
        }

        public Collection<GithubJsonItem> Content
        {
            get { return _content; }
        }
        #endregion
    }
}
