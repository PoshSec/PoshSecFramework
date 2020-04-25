using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using PoshSec.Framework.Properties;
using PoshSec.Framework.Strings;
using PoshSec.Framework.Web;

namespace PoshSec.Framework.Interface
{
    public partial class frmRepository : Form
    {
        private readonly GithubClient ghc = new GithubClient();
        private string _repoOwner = "";
        private string _curl = "";
        private GithubJsonItem _branch;
        private Collection<GithubJsonItem> _branches;
        private string _curBranch = "";
        private bool _update;

        public frmRepository()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtURL.Text.Replace("www.", "").Length > StringValue.GithubURL.Length &&
                txtURL.Text.Replace("www.", "").Substring(0, StringValue.GithubURL.Length) == StringValue.GithubURL)
            {
                if (cmbBranch.SelectedIndex > -1)
                {
                    btnOK.Enabled = false;
                    if (GetRepository())
                    {
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        btnOK.Enabled = true;
                    }
                }
            }
            else
            {
                ShowInvalidUrl();
            }
        }

        private void ListBranches(string GithubUrl)
        {
            ghc.Errors.Clear();
            var urlparts = GithubUrl.Replace("https://", "").Replace("http://", "").Replace("/", "|").Split('|');
            if (urlparts != null && urlparts.Length >= 3)
            {
                _repoOwner = urlparts[1];
                RepositoryName = urlparts[2];
                _branches = GetBranches(ghc, _repoOwner, RepositoryName);
                if (_branches != null && _branches.Count() > 0)
                {
                    var idx = -1;
                    var selidx = -1;
                    if (_curBranch == "")
                        selidx = 0;
                    foreach (var branchitem in _branches)
                    {
                        idx++;
                        cmbBranch.Items.Add(branchitem.Name);
                        if (_curBranch != "" && branchitem.Name == _curBranch)
                            selidx = idx;
                    }
                    if (_curBranch != "" && selidx == -1)
                    {
                        MessageBox.Show(string.Format(StringValue.BranchNotFound, _curBranch));
                        selidx = 0;
                    }
                    cmbBranch.SelectedIndex = selidx;
                    cmbBranch.Enabled = true;
                    btnOK.Enabled = true;
                    cmbBranch.Focus();
                    btnRefresh.Enabled = true;
                }
                else
                {
                    ShowInvalidUrl();
                }
            }
            else
            {
                ShowInvalidUrl();
            }
            lblStatus.Text = StringValue.Ready;
        }

        private bool GetRepository()
        {
            var rtn = true;
            ghc.Errors.Clear();
            _branch = _branches[cmbBranch.SelectedIndex];
            lblStatus.Text = "Downloading Repository, please wait...";
            Application.DoEvents();
            pbMain.Visible = true;
            ghc.GetArchive(_repoOwner, RepositoryName, _branch.Name, Settings.Default.ModulePath);
            if (ghc.Errors.Count > 0)
            {
                rtn = false;
                MessageBox.Show(string.Join(Environment.NewLine, ghc.Errors.ToArray()));
            }
            Restart = ghc.Restart;
            lblStatus.Text = StringValue.Ready;
            lblRateLimit.Text = ghc.RateLimitRemaining.ToString();
            LastModified = ghc.LastModified;
            return rtn;
        }

        private Collection<GithubJsonItem> GetBranches(GithubClient ghc, string Owner, string Repository)
        {
            lblStatus.Text = "Getting branches, please wait...";
            Application.DoEvents();
            Collection<GithubJsonItem> rtn = null;
            rtn = ghc.GetBranches(Owner, Repository);
            lblRateLimit.Text = ghc.RateLimitRemaining.ToString();
            return rtn;
        }

        private void txtURL_Leave(object sender, EventArgs e)
        {
            if (_curl != txtURL.Text.Trim())
                if (!btnCancel.Focused)
                    InitListBatches();
        }

        private void InitListBatches()
        {
            btnOK.Enabled = false;
            cmbBranch.Enabled = false;
            cmbBranch.Items.Clear();
            cmbBranch.Text = "";
            btnRefresh.Enabled = false;
            if (txtURL.Text.Replace("www.", "").Length > StringValue.GithubURL.Length &&
                txtURL.Text.Replace("www.", "").Substring(0, StringValue.GithubURL.Length) == StringValue.GithubURL)
            {
                _curl = txtURL.Text.Trim();
                ListBranches(txtURL.Text.Trim());
            }
            else
            {
                ShowInvalidUrl();
            }
        }

        private void ShowInvalidUrl()
        {
            MessageBox.Show(StringValue.InvalidRepositoryURL);
            txtURL.Focus();
            txtURL.SelectionStart = 0;
            txtURL.SelectionLength = txtURL.Text.Length;
            cmbBranch.Enabled = false;
        }

        private void llblRateLimit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                var psi = new ProcessStartInfo(StringValue.RateLimitURL);
                psi.UseShellExecute = true;
                psi.Verb = "open";
                var prc = new Process();
                prc.StartInfo = psi;
                prc.Start();
                prc = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            InitListBatches();
        }

        public bool Restart { get; private set; }

        public string RepositoryName { get; private set; } = "";

        public string LocationName => _repoOwner + "/" + RepositoryName;

        public string Url
        {
            set => txtURL.Text = value;
        }

        public string Branch
        {
            get => _branch.Name;
            set
            {
                cmbBranch.Text = value;
                _curBranch = value;
            }
        }

        public string LastModified { get; private set; } = "";

        public bool DoUpdate
        {
            set => _update = value;
        }

        private void frmRepository_Shown(object sender, EventArgs e)
        {
            if (_update)
            {
                txtURL.Enabled = false;
                btnOK.Enabled = false;
                btnCancel.Enabled = false;
                cmbBranch.Enabled = false;
                if (GetRepository())
                    DialogResult = DialogResult.OK;
                else
                    DialogResult = DialogResult.Cancel;
                Close();
            }
            else
            {
                if (_curBranch != "")
                    cmbBranch.Enabled = true;
            }
        }
    }
}
