namespace poshsecframework.Interface
{
    partial class frmRepository
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblInstructions = new System.Windows.Forms.Label();
            this.lblExample = new System.Windows.Forms.Label();
            this.pnlURL = new System.Windows.Forms.Panel();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.lblURL = new System.Windows.Forms.Label();
            this.sbStatus = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbMain = new System.Windows.Forms.ToolStripProgressBar();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.lblRateLimit = new System.Windows.Forms.Label();
            this.llblRateLimit = new System.Windows.Forms.LinkLabel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlBranch = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cmbBranch = new System.Windows.Forms.ComboBox();
            this.lblBranch = new System.Windows.Forms.Label();
            this.pnlURL.SuspendLayout();
            this.sbStatus.SuspendLayout();
            this.pnlControls.SuspendLayout();
            this.pnlBranch.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblInstructions
            // 
            this.lblInstructions.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblInstructions.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstructions.Location = new System.Drawing.Point(0, 0);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(439, 21);
            this.lblInstructions.TabIndex = 1;
            this.lblInstructions.Text = "Enter the URL for the Github Repository then select the Branch and click OK.";
            this.lblInstructions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblExample
            // 
            this.lblExample.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblExample.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExample.Location = new System.Drawing.Point(0, 21);
            this.lblExample.Name = "lblExample";
            this.lblExample.Size = new System.Drawing.Size(439, 19);
            this.lblExample.TabIndex = 2;
            this.lblExample.Text = "For example: https://www.github.com/PoshSec/PoshSec";
            this.lblExample.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlURL
            // 
            this.pnlURL.Controls.Add(this.txtURL);
            this.pnlURL.Controls.Add(this.lblURL);
            this.pnlURL.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlURL.Location = new System.Drawing.Point(0, 40);
            this.pnlURL.Name = "pnlURL";
            this.pnlURL.Size = new System.Drawing.Size(439, 26);
            this.pnlURL.TabIndex = 3;
            // 
            // txtURL
            // 
            this.txtURL.AutoCompleteCustomSource.AddRange(new string[] {
            "https://www.github.com/"});
            this.txtURL.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtURL.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtURL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtURL.Location = new System.Drawing.Point(51, 0);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(388, 21);
            this.txtURL.TabIndex = 1;
            this.txtURL.Leave += new System.EventHandler(this.txtURL_Leave);
            // 
            // lblURL
            // 
            this.lblURL.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblURL.Location = new System.Drawing.Point(0, 0);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(51, 26);
            this.lblURL.TabIndex = 0;
            this.lblURL.Text = "URL:";
            this.lblURL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // sbStatus
            // 
            this.sbStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.pbMain});
            this.sbStatus.Location = new System.Drawing.Point(0, 125);
            this.sbStatus.Name = "sbStatus";
            this.sbStatus.Size = new System.Drawing.Size(439, 22);
            this.sbStatus.SizingGrip = false;
            this.sbStatus.TabIndex = 4;
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(424, 17);
            this.lblStatus.Spring = true;
            this.lblStatus.Text = "Ready";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbMain
            // 
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(150, 16);
            this.pbMain.Visible = false;
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.lblRateLimit);
            this.pnlControls.Controls.Add(this.llblRateLimit);
            this.pnlControls.Controls.Add(this.btnOK);
            this.pnlControls.Controls.Add(this.btnCancel);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlControls.Location = new System.Drawing.Point(0, 95);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(439, 30);
            this.pnlControls.TabIndex = 5;
            // 
            // lblRateLimit
            // 
            this.lblRateLimit.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblRateLimit.Location = new System.Drawing.Point(116, 0);
            this.lblRateLimit.Name = "lblRateLimit";
            this.lblRateLimit.Size = new System.Drawing.Size(50, 30);
            this.lblRateLimit.TabIndex = 6;
            this.lblRateLimit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // llblRateLimit
            // 
            this.llblRateLimit.Dock = System.Windows.Forms.DockStyle.Left;
            this.llblRateLimit.Location = new System.Drawing.Point(0, 0);
            this.llblRateLimit.Name = "llblRateLimit";
            this.llblRateLimit.Size = new System.Drawing.Size(116, 30);
            this.llblRateLimit.TabIndex = 4;
            this.llblRateLimit.TabStop = true;
            this.llblRateLimit.Text = "Rate Limit Remaining:";
            this.llblRateLimit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.llblRateLimit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblRateLimit_LinkClicked);
            // 
            // btnOK
            // 
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(280, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(361, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // pnlBranch
            // 
            this.pnlBranch.Controls.Add(this.btnRefresh);
            this.pnlBranch.Controls.Add(this.cmbBranch);
            this.pnlBranch.Controls.Add(this.lblBranch);
            this.pnlBranch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBranch.Location = new System.Drawing.Point(0, 66);
            this.pnlBranch.Name = "pnlBranch";
            this.pnlBranch.Size = new System.Drawing.Size(439, 26);
            this.pnlBranch.TabIndex = 6;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRefresh.Enabled = false;
            this.btnRefresh.Image = global::poshsecframework.Properties.Resources.viewrefresh7;
            this.btnRefresh.Location = new System.Drawing.Point(240, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(35, 26);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cmbBranch
            // 
            this.cmbBranch.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmbBranch.Enabled = false;
            this.cmbBranch.FormattingEnabled = true;
            this.cmbBranch.Location = new System.Drawing.Point(51, 0);
            this.cmbBranch.Name = "cmbBranch";
            this.cmbBranch.Size = new System.Drawing.Size(189, 21);
            this.cmbBranch.TabIndex = 1;
            // 
            // lblBranch
            // 
            this.lblBranch.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblBranch.Location = new System.Drawing.Point(0, 0);
            this.lblBranch.Name = "lblBranch";
            this.lblBranch.Size = new System.Drawing.Size(51, 26);
            this.lblBranch.TabIndex = 0;
            this.lblBranch.Text = "Branch:";
            this.lblBranch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmRepository
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 147);
            this.ControlBox = false;
            this.Controls.Add(this.pnlBranch);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.sbStatus);
            this.Controls.Add(this.pnlURL);
            this.Controls.Add(this.lblExample);
            this.Controls.Add(this.lblInstructions);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmRepository";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Repository";
            this.pnlURL.ResumeLayout(false);
            this.pnlURL.PerformLayout();
            this.sbStatus.ResumeLayout(false);
            this.sbStatus.PerformLayout();
            this.pnlControls.ResumeLayout(false);
            this.pnlBranch.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.Label lblExample;
        private System.Windows.Forms.Panel pnlURL;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label lblURL;
        private System.Windows.Forms.StatusStrip sbStatus;
        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripProgressBar pbMain;
        private System.Windows.Forms.Panel pnlBranch;
        private System.Windows.Forms.Label lblBranch;
        private System.Windows.Forms.ComboBox cmbBranch;
        private System.Windows.Forms.Label lblRateLimit;
        private System.Windows.Forms.LinkLabel llblRateLimit;
        private System.Windows.Forms.Button btnRefresh;
    }
}