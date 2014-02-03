using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using poshsecframework.Strings;

namespace poshsecframework.Interface
{
    public partial class frmRepository : Form
    {
        private Web.GithubClient ghc = new Web.GithubClient();
        private String RepoOwner = "";
        private String Repository = "";
        private String curl = "";
        private Web.GithubJsonItem branch = null;
        private Collection<Web.GithubJsonItem> branches = null;
        private bool restart = false;

        public frmRepository()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtURL.Text.Replace("www.", "").Length > StringValue.GithubURL.Length && txtURL.Text.Replace("www.", "").Substring(0, StringValue.GithubURL.Length) == StringValue.GithubURL)
            {
                if (cmbBranch.SelectedIndex > -1)
                {
                    btnOK.Enabled = false;
                    if (GetRepository() == true)
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        this.Close();
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

        private void ListBranches(String GithubUrl)
        {
            ghc.Errors.Clear();
            String[] urlparts = GithubUrl.Replace("https://", "").Replace("http://", "").Replace("/", "|").Split('|');
            if (urlparts != null && urlparts.Length >= 3)
            {
                RepoOwner = urlparts[1];
                Repository = urlparts[2];
                branches = GetBranches(ghc, RepoOwner, Repository);
                if (branches != null && branches.Count() > 0)
                {
                    foreach (Web.GithubJsonItem branchitem in branches)
                    {
                        cmbBranch.Items.Add(branchitem.Name);
                    }
                    cmbBranch.SelectedIndex = 0;
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
            bool rtn = true;
            ghc.Errors.Clear();
            branch = branches[cmbBranch.SelectedIndex];
            lblStatus.Text = "Downloading Repository, please wait...";
            Application.DoEvents();
            pbMain.Visible = true;
            ghc.GetArchive(RepoOwner, Repository, branch.Name, Properties.Settings.Default.ModulePath);
            if (ghc.Errors.Count > 0)
            {
                rtn = false;
                MessageBox.Show(String.Join(Environment.NewLine, ghc.Errors.ToArray()));
            }
            restart = ghc.Restart;
            lblStatus.Text = StringValue.Ready;
            lblRateLimit.Text = ghc.RateLimitRemaining.ToString();
            return rtn;
        }

        private Collection<Web.GithubJsonItem> GetBranches(Web.GithubClient ghc, String Owner, String Repository)
        {
            lblStatus.Text = "Getting branches, please wait...";
            Application.DoEvents();
            Collection<Web.GithubJsonItem> rtn = null;
            rtn = ghc.GetBranches(Owner, Repository);
            lblRateLimit.Text = ghc.RateLimitRemaining.ToString();
            return rtn;
        }

        private void txtURL_Leave(object sender, EventArgs e)
        {
            if (curl != txtURL.Text.Trim())
            {
                InitListBatches();
            }
        }

        private void InitListBatches()
        {
            btnOK.Enabled = false;
            cmbBranch.Enabled = false;
            cmbBranch.Items.Clear();
            cmbBranch.Text = "";
            btnRefresh.Enabled = false;
            if (txtURL.Text.Replace("www.", "").Length > StringValue.GithubURL.Length && txtURL.Text.Replace("www.", "").Substring(0, StringValue.GithubURL.Length) == StringValue.GithubURL)
            {
                curl = txtURL.Text.Trim();
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
        }

        private void llblRateLimit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo(StringValue.RateLimitURL);
                psi.UseShellExecute = true;
                psi.Verb = "open";
                System.Diagnostics.Process prc = new System.Diagnostics.Process();
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

        public bool Restart
        {
            get { return restart; }
        }

        public String RepositoryName
        {
            get { return Repository; }
        }

        public String LocationName
        {
            get { return RepoOwner + "/" + Repository; }
        }

        public String Branch
        {
            get { return branch.Name; }
        }

        public DateTime LastUpdate
        {
            get { return DateTime.Now; }
        }
    }
}
