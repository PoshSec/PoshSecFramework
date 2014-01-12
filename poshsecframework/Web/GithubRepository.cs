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
        private Collection<GithubContentItem> _content = new Collection<GithubContentItem>();
        #endregion

        #region Public Properties
        public Collection<String> Branches
        {
            get { return _branches; }
        }

        public Collection<GithubContentItem> Content
        {
            get { return _content; }
        }
        #endregion
    }
}
