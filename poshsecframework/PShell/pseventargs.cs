using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace poshsecframework.PShell
{
    class pseventargs : EventArgs
    {
        #region " Private Variables "
        private String rslts;
        private ListViewItem lvw;
        private bool cancelled;
        #endregion

        #region " Public Methods "
        public pseventargs(String ResultsText, ListViewItem ScriptListView, bool Cancelled)
        {
            rslts = ResultsText;
            lvw = ScriptListView;
            cancelled = Cancelled;
        }
        #endregion

        #region " Public Properties "
        public String Results
        {
            get
            {
                return rslts;
            }
        }

        public ListViewItem ScriptListView
        {
            get { return lvw; }
        }

        public bool Cancelled
        {
            get { return cancelled; }
        }
        #endregion
    }
}
