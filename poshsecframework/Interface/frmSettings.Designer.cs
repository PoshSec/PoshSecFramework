namespace PoshSec.Framework.Interface
{
    partial class frmSettings
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbpGeneral = new System.Windows.Forms.TabPage();
            this.panel6 = new System.Windows.Forms.Panel();
            this.gbNameChecking = new System.Windows.Forms.GroupBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.ckShowinTaskbar = new System.Windows.Forms.CheckBox();
            this.lblShowinTaskbar = new System.Windows.Forms.Label();
            this.panel12 = new System.Windows.Forms.Panel();
            this.ckSaveSystems = new System.Windows.Forms.CheckBox();
            this.lblSaveSystems = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.ckNameCheck = new System.Windows.Forms.CheckBox();
            this.lblNameChecking = new System.Windows.Forms.Label();
            this.gbFirstTime = new System.Windows.Forms.GroupBox();
            this.cmbFirstTime = new System.Windows.Forms.ComboBox();
            this.lblShowFirstTime = new System.Windows.Forms.Label();
            this.gbScriptSetting = new System.Windows.Forms.GroupBox();
            this.cmbScriptDefAction = new System.Windows.Forms.ComboBox();
            this.lblScriptDefAction = new System.Windows.Forms.Label();
            this.pnlGithubAPIKey = new System.Windows.Forms.Panel();
            this.txtGithubAPIKey = new System.Windows.Forms.TextBox();
            this.btnGithubHelp = new System.Windows.Forms.Button();
            this.lblGithubAPIKey = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.txtPSExecPath = new System.Windows.Forms.TextBox();
            this.btnBrowsePSExec = new System.Windows.Forms.Button();
            this.lblPSExecPath = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.txtSchFile = new System.Windows.Forms.TextBox();
            this.btnBrowseSchFile = new System.Windows.Forms.Button();
            this.lblSchFile = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtModuleDirectory = new System.Windows.Forms.TextBox();
            this.btnBrowseModule = new System.Windows.Forms.Button();
            this.lblModuleDirectory = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtScriptDirectory = new System.Windows.Forms.TextBox();
            this.btnBrowseScript = new System.Windows.Forms.Button();
            this.lblScriptDirectory = new System.Windows.Forms.Label();
            this.tcSettings = new System.Windows.Forms.TabControl();
            this.tbpLogging = new System.Windows.Forms.TabPage();
            this.gbSyslogInfo = new System.Windows.Forms.GroupBox();
            this.panel10 = new System.Windows.Forms.Panel();
            this.txtSyslogServer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSyslogPort = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.ckUseSyslog = new System.Windows.Forms.CheckBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.txtAlertLog = new System.Windows.Forms.TextBox();
            this.btnBrowseAlertLog = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ckAlertLog = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtOutputLog = new System.Windows.Forms.TextBox();
            this.btnBrowseOutputLog = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ckOutputLog = new System.Windows.Forms.CheckBox();
            this.tbpModules = new System.Windows.Forms.TabPage();
            this.lvwModules = new System.Windows.Forms.ListView();
            this.chModName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chRepository = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chBranch = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLastModified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.tbModules = new System.Windows.Forms.ToolStrip();
            this.btnAddModule = new System.Windows.Forms.ToolStripButton();
            this.pbStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.lblStatus = new System.Windows.Forms.ToolStripLabel();
            this.lblRestartRequired = new System.Windows.Forms.ToolStripLabel();
            this.btnEditModule = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteModule = new System.Windows.Forms.ToolStripButton();
            this.btnCheckUpdates = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.tabProxy = new System.Windows.Forms.TabPage();
            this.proxyPreferenceGroupBox1 = new ProxyPreferenceGroupBox();
            this.radNoProxy = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.radSystemProxy = new System.Windows.Forms.RadioButton();
            this.txbProxyPort = new System.Windows.Forms.TextBox();
            this.radManualProxy = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.txbProxyHost = new System.Windows.Forms.TextBox();
            this.ttPSFHelp = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.tbpGeneral.SuspendLayout();
            this.panel6.SuspendLayout();
            this.gbNameChecking.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel11.SuspendLayout();
            this.gbFirstTime.SuspendLayout();
            this.gbScriptSetting.SuspendLayout();
            this.pnlGithubAPIKey.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tcSettings.SuspendLayout();
            this.tbpLogging.SuspendLayout();
            this.gbSyslogInfo.SuspendLayout();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSyslogPort)).BeginInit();
            this.panel9.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tbpModules.SuspendLayout();
            this.tbModules.SuspendLayout();
            this.tabProxy.SuspendLayout();
            this.proxyPreferenceGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 249);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(531, 34);
            this.panel1.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(368, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(449, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tbpGeneral
            // 
            this.tbpGeneral.Controls.Add(this.panel6);
            this.tbpGeneral.Controls.Add(this.pnlGithubAPIKey);
            this.tbpGeneral.Controls.Add(this.panel7);
            this.tbpGeneral.Controls.Add(this.panel8);
            this.tbpGeneral.Controls.Add(this.panel4);
            this.tbpGeneral.Controls.Add(this.panel2);
            this.tbpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tbpGeneral.Name = "tbpGeneral";
            this.tbpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tbpGeneral.Size = new System.Drawing.Size(523, 223);
            this.tbpGeneral.TabIndex = 0;
            this.tbpGeneral.Text = "General";
            this.tbpGeneral.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.gbNameChecking);
            this.panel6.Controls.Add(this.gbFirstTime);
            this.panel6.Controls.Add(this.gbScriptSetting);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(3, 133);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(517, 84);
            this.panel6.TabIndex = 7;
            // 
            // gbNameChecking
            // 
            this.gbNameChecking.Controls.Add(this.panel5);
            this.gbNameChecking.Controls.Add(this.panel12);
            this.gbNameChecking.Controls.Add(this.panel11);
            this.gbNameChecking.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbNameChecking.Location = new System.Drawing.Point(318, 0);
            this.gbNameChecking.Name = "gbNameChecking";
            this.gbNameChecking.Padding = new System.Windows.Forms.Padding(0);
            this.gbNameChecking.Size = new System.Drawing.Size(199, 84);
            this.gbNameChecking.TabIndex = 2;
            this.gbNameChecking.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.ckShowinTaskbar);
            this.panel5.Controls.Add(this.lblShowinTaskbar);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 61);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(199, 24);
            this.panel5.TabIndex = 2;
            // 
            // ckShowinTaskbar
            // 
            this.ckShowinTaskbar.Appearance = System.Windows.Forms.Appearance.Button;
            this.ckShowinTaskbar.Checked = true;
            this.ckShowinTaskbar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckShowinTaskbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.ckShowinTaskbar.Image = global::PoshSec.Framework.Properties.Resources.dialogyes;
            this.ckShowinTaskbar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ckShowinTaskbar.Location = new System.Drawing.Point(118, 0);
            this.ckShowinTaskbar.Name = "ckShowinTaskbar";
            this.ckShowinTaskbar.Size = new System.Drawing.Size(81, 24);
            this.ckShowinTaskbar.TabIndex = 3;
            this.ckShowinTaskbar.Text = "Yes";
            this.ckShowinTaskbar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ttPSFHelp.SetToolTip(this.ckShowinTaskbar, "If set to No, this will hide the program from the Taskbar\r\nwhen minimized.");
            this.ckShowinTaskbar.UseVisualStyleBackColor = true;
            this.ckShowinTaskbar.CheckedChanged += new System.EventHandler(this.ckShowinTaskbar_CheckedChanged);
            // 
            // lblShowinTaskbar
            // 
            this.lblShowinTaskbar.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblShowinTaskbar.Location = new System.Drawing.Point(0, 0);
            this.lblShowinTaskbar.Name = "lblShowinTaskbar";
            this.lblShowinTaskbar.Size = new System.Drawing.Size(118, 24);
            this.lblShowinTaskbar.TabIndex = 2;
            this.lblShowinTaskbar.Text = "Show in Taskbar";
            this.lblShowinTaskbar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.ckSaveSystems);
            this.panel12.Controls.Add(this.lblSaveSystems);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel12.Location = new System.Drawing.Point(0, 37);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(199, 24);
            this.panel12.TabIndex = 1;
            // 
            // ckSaveSystems
            // 
            this.ckSaveSystems.Appearance = System.Windows.Forms.Appearance.Button;
            this.ckSaveSystems.Checked = true;
            this.ckSaveSystems.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckSaveSystems.Dock = System.Windows.Forms.DockStyle.Top;
            this.ckSaveSystems.Image = global::PoshSec.Framework.Properties.Resources.dialogyes;
            this.ckSaveSystems.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ckSaveSystems.Location = new System.Drawing.Point(118, 0);
            this.ckSaveSystems.Name = "ckSaveSystems";
            this.ckSaveSystems.Size = new System.Drawing.Size(81, 24);
            this.ckSaveSystems.TabIndex = 3;
            this.ckSaveSystems.Text = "Yes";
            this.ckSaveSystems.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ttPSFHelp.SetToolTip(this.ckSaveSystems, "This option is to retain the systems listed in the Systems tab\r\nand to reload the" +
        "m on startup.\r\nIf set to No, it will delete any currently saved systems from the" +
        "\r\nsettings file.");
            this.ckSaveSystems.UseVisualStyleBackColor = true;
            this.ckSaveSystems.CheckedChanged += new System.EventHandler(this.ckSaveSystems_CheckedChanged);
            // 
            // lblSaveSystems
            // 
            this.lblSaveSystems.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSaveSystems.Location = new System.Drawing.Point(0, 0);
            this.lblSaveSystems.Name = "lblSaveSystems";
            this.lblSaveSystems.Size = new System.Drawing.Size(118, 24);
            this.lblSaveSystems.TabIndex = 2;
            this.lblSaveSystems.Text = "Save Systems";
            this.lblSaveSystems.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.ckNameCheck);
            this.panel11.Controls.Add(this.lblNameChecking);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel11.Location = new System.Drawing.Point(0, 14);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(199, 23);
            this.panel11.TabIndex = 0;
            // 
            // ckNameCheck
            // 
            this.ckNameCheck.Appearance = System.Windows.Forms.Appearance.Button;
            this.ckNameCheck.Checked = true;
            this.ckNameCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckNameCheck.Dock = System.Windows.Forms.DockStyle.Top;
            this.ckNameCheck.Image = global::PoshSec.Framework.Properties.Resources.dialogyes;
            this.ckNameCheck.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ckNameCheck.Location = new System.Drawing.Point(118, 0);
            this.ckNameCheck.Margin = new System.Windows.Forms.Padding(0);
            this.ckNameCheck.Name = "ckNameCheck";
            this.ckNameCheck.Size = new System.Drawing.Size(81, 24);
            this.ckNameCheck.TabIndex = 3;
            this.ckNameCheck.Text = "On";
            this.ckNameCheck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ttPSFHelp.SetToolTip(this.ckNameCheck, "When On, it will check to ensure the Functions / Cmdlets follow PowerShell naming" +
        " standards.\r\nWhen Off it will add -DisableNameChecking when loading the modules." +
        "");
            this.ckNameCheck.UseVisualStyleBackColor = true;
            this.ckNameCheck.CheckedChanged += new System.EventHandler(this.ckNameCheck_CheckedChanged);
            // 
            // lblNameChecking
            // 
            this.lblNameChecking.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblNameChecking.Location = new System.Drawing.Point(0, 0);
            this.lblNameChecking.Name = "lblNameChecking";
            this.lblNameChecking.Size = new System.Drawing.Size(118, 23);
            this.lblNameChecking.TabIndex = 2;
            this.lblNameChecking.Text = "Name Checking";
            this.lblNameChecking.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbFirstTime
            // 
            this.gbFirstTime.Controls.Add(this.cmbFirstTime);
            this.gbFirstTime.Controls.Add(this.lblShowFirstTime);
            this.gbFirstTime.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbFirstTime.Location = new System.Drawing.Point(195, 0);
            this.gbFirstTime.Name = "gbFirstTime";
            this.gbFirstTime.Size = new System.Drawing.Size(123, 84);
            this.gbFirstTime.TabIndex = 1;
            this.gbFirstTime.TabStop = false;
            // 
            // cmbFirstTime
            // 
            this.cmbFirstTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbFirstTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFirstTime.FormattingEnabled = true;
            this.cmbFirstTime.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cmbFirstTime.Location = new System.Drawing.Point(3, 40);
            this.cmbFirstTime.Name = "cmbFirstTime";
            this.cmbFirstTime.Size = new System.Drawing.Size(117, 21);
            this.cmbFirstTime.TabIndex = 7;
            // 
            // lblShowFirstTime
            // 
            this.lblShowFirstTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblShowFirstTime.Location = new System.Drawing.Point(3, 17);
            this.lblShowFirstTime.Name = "lblShowFirstTime";
            this.lblShowFirstTime.Size = new System.Drawing.Size(117, 23);
            this.lblShowFirstTime.TabIndex = 0;
            this.lblShowFirstTime.Text = "Show First Time Utility:";
            this.lblShowFirstTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbScriptSetting
            // 
            this.gbScriptSetting.Controls.Add(this.cmbScriptDefAction);
            this.gbScriptSetting.Controls.Add(this.lblScriptDefAction);
            this.gbScriptSetting.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbScriptSetting.Location = new System.Drawing.Point(0, 0);
            this.gbScriptSetting.Name = "gbScriptSetting";
            this.gbScriptSetting.Size = new System.Drawing.Size(195, 84);
            this.gbScriptSetting.TabIndex = 0;
            this.gbScriptSetting.TabStop = false;
            // 
            // cmbScriptDefAction
            // 
            this.cmbScriptDefAction.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbScriptDefAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScriptDefAction.FormattingEnabled = true;
            this.cmbScriptDefAction.Items.AddRange(new object[] {
            "Run Script",
            "View Script"});
            this.cmbScriptDefAction.Location = new System.Drawing.Point(3, 40);
            this.cmbScriptDefAction.Name = "cmbScriptDefAction";
            this.cmbScriptDefAction.Size = new System.Drawing.Size(189, 21);
            this.cmbScriptDefAction.TabIndex = 7;
            // 
            // lblScriptDefAction
            // 
            this.lblScriptDefAction.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblScriptDefAction.Location = new System.Drawing.Point(3, 17);
            this.lblScriptDefAction.Name = "lblScriptDefAction";
            this.lblScriptDefAction.Size = new System.Drawing.Size(189, 23);
            this.lblScriptDefAction.TabIndex = 0;
            this.lblScriptDefAction.Text = "Double Click Default Action:";
            this.lblScriptDefAction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlGithubAPIKey
            // 
            this.pnlGithubAPIKey.Controls.Add(this.txtGithubAPIKey);
            this.pnlGithubAPIKey.Controls.Add(this.btnGithubHelp);
            this.pnlGithubAPIKey.Controls.Add(this.lblGithubAPIKey);
            this.pnlGithubAPIKey.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGithubAPIKey.Location = new System.Drawing.Point(3, 107);
            this.pnlGithubAPIKey.Name = "pnlGithubAPIKey";
            this.pnlGithubAPIKey.Size = new System.Drawing.Size(517, 26);
            this.pnlGithubAPIKey.TabIndex = 1;
            // 
            // txtGithubAPIKey
            // 
            this.txtGithubAPIKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGithubAPIKey.Location = new System.Drawing.Point(111, 0);
            this.txtGithubAPIKey.Name = "txtGithubAPIKey";
            this.txtGithubAPIKey.Size = new System.Drawing.Size(380, 21);
            this.txtGithubAPIKey.TabIndex = 6;
            // 
            // btnGithubHelp
            // 
            this.btnGithubHelp.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnGithubHelp.Image = global::PoshSec.Framework.Properties.Resources.help;
            this.btnGithubHelp.Location = new System.Drawing.Point(491, 0);
            this.btnGithubHelp.Name = "btnGithubHelp";
            this.btnGithubHelp.Size = new System.Drawing.Size(26, 26);
            this.btnGithubHelp.TabIndex = 2;
            this.ttPSFHelp.SetToolTip(this.btnGithubHelp, resources.GetString("btnGithubHelp.ToolTip"));
            this.btnGithubHelp.UseVisualStyleBackColor = true;
            this.btnGithubHelp.Click += new System.EventHandler(this.btnGithubHelp_Click);
            // 
            // lblGithubAPIKey
            // 
            this.lblGithubAPIKey.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblGithubAPIKey.Location = new System.Drawing.Point(0, 0);
            this.lblGithubAPIKey.Name = "lblGithubAPIKey";
            this.lblGithubAPIKey.Size = new System.Drawing.Size(111, 26);
            this.lblGithubAPIKey.TabIndex = 0;
            this.lblGithubAPIKey.Text = "Github Access Token:";
            this.lblGithubAPIKey.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.txtPSExecPath);
            this.panel7.Controls.Add(this.btnBrowsePSExec);
            this.panel7.Controls.Add(this.lblPSExecPath);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(3, 81);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(517, 26);
            this.panel7.TabIndex = 5;
            // 
            // txtPSExecPath
            // 
            this.txtPSExecPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPSExecPath.Location = new System.Drawing.Point(111, 0);
            this.txtPSExecPath.Name = "txtPSExecPath";
            this.txtPSExecPath.Size = new System.Drawing.Size(380, 21);
            this.txtPSExecPath.TabIndex = 6;
            // 
            // btnBrowsePSExec
            // 
            this.btnBrowsePSExec.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBrowsePSExec.Image = global::PoshSec.Framework.Properties.Resources.documentopenfolder;
            this.btnBrowsePSExec.Location = new System.Drawing.Point(491, 0);
            this.btnBrowsePSExec.Name = "btnBrowsePSExec";
            this.btnBrowsePSExec.Size = new System.Drawing.Size(26, 26);
            this.btnBrowsePSExec.TabIndex = 1;
            this.btnBrowsePSExec.UseVisualStyleBackColor = true;
            this.btnBrowsePSExec.Click += new System.EventHandler(this.btnBrowsePSExec_Click);
            // 
            // lblPSExecPath
            // 
            this.lblPSExecPath.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPSExecPath.Location = new System.Drawing.Point(0, 0);
            this.lblPSExecPath.Name = "lblPSExecPath";
            this.lblPSExecPath.Size = new System.Drawing.Size(111, 26);
            this.lblPSExecPath.TabIndex = 0;
            this.lblPSExecPath.Text = "PSExec Path:";
            this.lblPSExecPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.txtSchFile);
            this.panel8.Controls.Add(this.btnBrowseSchFile);
            this.panel8.Controls.Add(this.lblSchFile);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(3, 55);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(517, 26);
            this.panel8.TabIndex = 8;
            // 
            // txtSchFile
            // 
            this.txtSchFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSchFile.Location = new System.Drawing.Point(111, 0);
            this.txtSchFile.Name = "txtSchFile";
            this.txtSchFile.Size = new System.Drawing.Size(380, 21);
            this.txtSchFile.TabIndex = 5;
            // 
            // btnBrowseSchFile
            // 
            this.btnBrowseSchFile.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBrowseSchFile.Image = global::PoshSec.Framework.Properties.Resources.documentopenfolder;
            this.btnBrowseSchFile.Location = new System.Drawing.Point(491, 0);
            this.btnBrowseSchFile.Name = "btnBrowseSchFile";
            this.btnBrowseSchFile.Size = new System.Drawing.Size(26, 26);
            this.btnBrowseSchFile.TabIndex = 1;
            this.btnBrowseSchFile.UseVisualStyleBackColor = true;
            this.btnBrowseSchFile.Click += new System.EventHandler(this.btnBrowseSchFile_Click);
            // 
            // lblSchFile
            // 
            this.lblSchFile.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSchFile.Location = new System.Drawing.Point(0, 0);
            this.lblSchFile.Name = "lblSchFile";
            this.lblSchFile.Size = new System.Drawing.Size(111, 26);
            this.lblSchFile.TabIndex = 0;
            this.lblSchFile.Text = "Schedule File:";
            this.lblSchFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtModuleDirectory);
            this.panel4.Controls.Add(this.btnBrowseModule);
            this.panel4.Controls.Add(this.lblModuleDirectory);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 29);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(517, 26);
            this.panel4.TabIndex = 2;
            // 
            // txtModuleDirectory
            // 
            this.txtModuleDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtModuleDirectory.Location = new System.Drawing.Point(111, 0);
            this.txtModuleDirectory.Name = "txtModuleDirectory";
            this.txtModuleDirectory.Size = new System.Drawing.Size(380, 21);
            this.txtModuleDirectory.TabIndex = 3;
            // 
            // btnBrowseModule
            // 
            this.btnBrowseModule.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBrowseModule.Image = global::PoshSec.Framework.Properties.Resources.documentopenfolder;
            this.btnBrowseModule.Location = new System.Drawing.Point(491, 0);
            this.btnBrowseModule.Name = "btnBrowseModule";
            this.btnBrowseModule.Size = new System.Drawing.Size(26, 26);
            this.btnBrowseModule.TabIndex = 1;
            this.btnBrowseModule.UseVisualStyleBackColor = true;
            this.btnBrowseModule.Click += new System.EventHandler(this.btnBrowseModule_Click);
            // 
            // lblModuleDirectory
            // 
            this.lblModuleDirectory.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblModuleDirectory.Location = new System.Drawing.Point(0, 0);
            this.lblModuleDirectory.Name = "lblModuleDirectory";
            this.lblModuleDirectory.Size = new System.Drawing.Size(111, 26);
            this.lblModuleDirectory.TabIndex = 0;
            this.lblModuleDirectory.Text = "Module Directory:";
            this.lblModuleDirectory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtScriptDirectory);
            this.panel2.Controls.Add(this.btnBrowseScript);
            this.panel2.Controls.Add(this.lblScriptDirectory);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(517, 26);
            this.panel2.TabIndex = 0;
            // 
            // txtScriptDirectory
            // 
            this.txtScriptDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtScriptDirectory.Location = new System.Drawing.Point(111, 0);
            this.txtScriptDirectory.Name = "txtScriptDirectory";
            this.txtScriptDirectory.Size = new System.Drawing.Size(380, 21);
            this.txtScriptDirectory.TabIndex = 2;
            // 
            // btnBrowseScript
            // 
            this.btnBrowseScript.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBrowseScript.Image = global::PoshSec.Framework.Properties.Resources.documentopenfolder;
            this.btnBrowseScript.Location = new System.Drawing.Point(491, 0);
            this.btnBrowseScript.Name = "btnBrowseScript";
            this.btnBrowseScript.Size = new System.Drawing.Size(26, 26);
            this.btnBrowseScript.TabIndex = 1;
            this.btnBrowseScript.UseVisualStyleBackColor = true;
            this.btnBrowseScript.Click += new System.EventHandler(this.btnBrowseScript_Click);
            // 
            // lblScriptDirectory
            // 
            this.lblScriptDirectory.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblScriptDirectory.Location = new System.Drawing.Point(0, 0);
            this.lblScriptDirectory.Name = "lblScriptDirectory";
            this.lblScriptDirectory.Size = new System.Drawing.Size(111, 26);
            this.lblScriptDirectory.TabIndex = 0;
            this.lblScriptDirectory.Text = "Script Directory:";
            this.lblScriptDirectory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tcSettings
            // 
            this.tcSettings.Controls.Add(this.tbpGeneral);
            this.tcSettings.Controls.Add(this.tbpLogging);
            this.tcSettings.Controls.Add(this.tbpModules);
            this.tcSettings.Controls.Add(this.tabProxy);
            this.tcSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSettings.Location = new System.Drawing.Point(0, 0);
            this.tcSettings.Multiline = true;
            this.tcSettings.Name = "tcSettings";
            this.tcSettings.SelectedIndex = 0;
            this.tcSettings.Size = new System.Drawing.Size(531, 249);
            this.tcSettings.TabIndex = 1;
            // 
            // tbpLogging
            // 
            this.tbpLogging.Controls.Add(this.gbSyslogInfo);
            this.tbpLogging.Controls.Add(this.ckUseSyslog);
            this.tbpLogging.Controls.Add(this.panel9);
            this.tbpLogging.Controls.Add(this.panel3);
            this.tbpLogging.Location = new System.Drawing.Point(4, 22);
            this.tbpLogging.Name = "tbpLogging";
            this.tbpLogging.Padding = new System.Windows.Forms.Padding(3);
            this.tbpLogging.Size = new System.Drawing.Size(523, 223);
            this.tbpLogging.TabIndex = 2;
            this.tbpLogging.Text = "Logging";
            this.tbpLogging.UseVisualStyleBackColor = true;
            // 
            // gbSyslogInfo
            // 
            this.gbSyslogInfo.Controls.Add(this.panel10);
            this.gbSyslogInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbSyslogInfo.Enabled = false;
            this.gbSyslogInfo.Location = new System.Drawing.Point(3, 72);
            this.gbSyslogInfo.Name = "gbSyslogInfo";
            this.gbSyslogInfo.Size = new System.Drawing.Size(517, 53);
            this.gbSyslogInfo.TabIndex = 4;
            this.gbSyslogInfo.TabStop = false;
            this.gbSyslogInfo.Text = "Syslog Server Information";
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.txtSyslogServer);
            this.panel10.Controls.Add(this.label4);
            this.panel10.Controls.Add(this.txtSyslogPort);
            this.panel10.Controls.Add(this.label3);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(3, 17);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(511, 26);
            this.panel10.TabIndex = 3;
            // 
            // txtSyslogServer
            // 
            this.txtSyslogServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSyslogServer.Location = new System.Drawing.Point(111, 0);
            this.txtSyslogServer.Name = "txtSyslogServer";
            this.txtSyslogServer.Size = new System.Drawing.Size(296, 21);
            this.txtSyslogServer.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Location = new System.Drawing.Point(407, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 26);
            this.label4.TabIndex = 5;
            this.label4.Text = "Port:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSyslogPort
            // 
            this.txtSyslogPort.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtSyslogPort.Location = new System.Drawing.Point(441, 0);
            this.txtSyslogPort.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txtSyslogPort.Name = "txtSyslogPort";
            this.txtSyslogPort.Size = new System.Drawing.Size(70, 21);
            this.txtSyslogPort.TabIndex = 4;
            this.txtSyslogPort.Value = new decimal(new int[] {
            514,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 26);
            this.label3.TabIndex = 3;
            this.label3.Text = "Server Name / IP:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ckUseSyslog
            // 
            this.ckUseSyslog.AutoSize = true;
            this.ckUseSyslog.Dock = System.Windows.Forms.DockStyle.Top;
            this.ckUseSyslog.Location = new System.Drawing.Point(3, 55);
            this.ckUseSyslog.Name = "ckUseSyslog";
            this.ckUseSyslog.Size = new System.Drawing.Size(517, 17);
            this.ckUseSyslog.TabIndex = 3;
            this.ckUseSyslog.Text = "Use Syslog";
            this.ckUseSyslog.UseVisualStyleBackColor = true;
            this.ckUseSyslog.CheckedChanged += new System.EventHandler(this.ckUseSyslog_CheckedChanged);
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.txtAlertLog);
            this.panel9.Controls.Add(this.btnBrowseAlertLog);
            this.panel9.Controls.Add(this.label2);
            this.panel9.Controls.Add(this.ckAlertLog);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(3, 29);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(517, 26);
            this.panel9.TabIndex = 2;
            // 
            // txtAlertLog
            // 
            this.txtAlertLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAlertLog.Location = new System.Drawing.Point(111, 0);
            this.txtAlertLog.Name = "txtAlertLog";
            this.txtAlertLog.Size = new System.Drawing.Size(333, 21);
            this.txtAlertLog.TabIndex = 5;
            // 
            // btnBrowseAlertLog
            // 
            this.btnBrowseAlertLog.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBrowseAlertLog.Image = global::PoshSec.Framework.Properties.Resources.documentopenfolder;
            this.btnBrowseAlertLog.Location = new System.Drawing.Point(444, 0);
            this.btnBrowseAlertLog.Name = "btnBrowseAlertLog";
            this.btnBrowseAlertLog.Size = new System.Drawing.Size(26, 26);
            this.btnBrowseAlertLog.TabIndex = 4;
            this.btnBrowseAlertLog.UseVisualStyleBackColor = true;
            this.btnBrowseAlertLog.Click += new System.EventHandler(this.btnBrowseAlertLog_Click);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 26);
            this.label2.TabIndex = 3;
            this.label2.Text = "Alert Log:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ckAlertLog
            // 
            this.ckAlertLog.Appearance = System.Windows.Forms.Appearance.Button;
            this.ckAlertLog.Checked = true;
            this.ckAlertLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckAlertLog.Dock = System.Windows.Forms.DockStyle.Right;
            this.ckAlertLog.Image = global::PoshSec.Framework.Properties.Resources.dialogyes;
            this.ckAlertLog.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ckAlertLog.Location = new System.Drawing.Point(470, 0);
            this.ckAlertLog.Name = "ckAlertLog";
            this.ckAlertLog.Size = new System.Drawing.Size(47, 26);
            this.ckAlertLog.TabIndex = 0;
            this.ckAlertLog.Text = "On";
            this.ckAlertLog.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckAlertLog.UseVisualStyleBackColor = true;
            this.ckAlertLog.CheckedChanged += new System.EventHandler(this.ckAlertLog_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtOutputLog);
            this.panel3.Controls.Add(this.btnBrowseOutputLog);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.ckOutputLog);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(517, 26);
            this.panel3.TabIndex = 1;
            // 
            // txtOutputLog
            // 
            this.txtOutputLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutputLog.Location = new System.Drawing.Point(111, 0);
            this.txtOutputLog.Name = "txtOutputLog";
            this.txtOutputLog.Size = new System.Drawing.Size(333, 21);
            this.txtOutputLog.TabIndex = 5;
            // 
            // btnBrowseOutputLog
            // 
            this.btnBrowseOutputLog.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBrowseOutputLog.Image = global::PoshSec.Framework.Properties.Resources.documentopenfolder;
            this.btnBrowseOutputLog.Location = new System.Drawing.Point(444, 0);
            this.btnBrowseOutputLog.Name = "btnBrowseOutputLog";
            this.btnBrowseOutputLog.Size = new System.Drawing.Size(26, 26);
            this.btnBrowseOutputLog.TabIndex = 4;
            this.btnBrowseOutputLog.UseVisualStyleBackColor = true;
            this.btnBrowseOutputLog.Click += new System.EventHandler(this.btnBrowseOutputLog_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "Output Log:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ckOutputLog
            // 
            this.ckOutputLog.Appearance = System.Windows.Forms.Appearance.Button;
            this.ckOutputLog.Checked = true;
            this.ckOutputLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckOutputLog.Dock = System.Windows.Forms.DockStyle.Right;
            this.ckOutputLog.Image = global::PoshSec.Framework.Properties.Resources.dialogyes;
            this.ckOutputLog.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ckOutputLog.Location = new System.Drawing.Point(470, 0);
            this.ckOutputLog.Name = "ckOutputLog";
            this.ckOutputLog.Size = new System.Drawing.Size(47, 26);
            this.ckOutputLog.TabIndex = 0;
            this.ckOutputLog.Text = "On";
            this.ckOutputLog.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckOutputLog.UseVisualStyleBackColor = true;
            this.ckOutputLog.CheckedChanged += new System.EventHandler(this.ckOutputLog_CheckedChanged);
            // 
            // tbpModules
            // 
            this.tbpModules.Controls.Add(this.lvwModules);
            this.tbpModules.Controls.Add(this.tbModules);
            this.tbpModules.Location = new System.Drawing.Point(4, 22);
            this.tbpModules.Name = "tbpModules";
            this.tbpModules.Padding = new System.Windows.Forms.Padding(3);
            this.tbpModules.Size = new System.Drawing.Size(523, 223);
            this.tbpModules.TabIndex = 1;
            this.tbpModules.Text = "Modules";
            this.tbpModules.UseVisualStyleBackColor = true;
            // 
            // lvwModules
            // 
            this.lvwModules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chModName,
            this.chRepository,
            this.chBranch,
            this.chLastModified});
            this.lvwModules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwModules.FullRowSelect = true;
            this.lvwModules.Location = new System.Drawing.Point(3, 28);
            this.lvwModules.Name = "lvwModules";
            this.lvwModules.Size = new System.Drawing.Size(517, 192);
            this.lvwModules.SmallImageList = this.imgList;
            this.lvwModules.TabIndex = 1;
            this.lvwModules.UseCompatibleStateImageBehavior = false;
            this.lvwModules.View = System.Windows.Forms.View.Details;
            this.lvwModules.SelectedIndexChanged += new System.EventHandler(this.lvwModules_SelectedIndexChanged);
            // 
            // chModName
            // 
            this.chModName.Text = "Module Name";
            this.chModName.Width = 110;
            // 
            // chRepository
            // 
            this.chRepository.Text = "Repository";
            this.chRepository.Width = 170;
            // 
            // chBranch
            // 
            this.chBranch.Text = "Branch";
            this.chBranch.Width = 80;
            // 
            // chLastModified
            // 
            this.chLastModified.Text = "Last Modified";
            this.chLastModified.Width = 130;
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "package-installed-updated.png");
            this.imgList.Images.SetKeyName(1, "package-install.png");
            this.imgList.Images.SetKeyName(2, "package-broken.png");
            // 
            // tbModules
            // 
            this.tbModules.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddModule,
            this.pbStatus,
            this.lblStatus,
            this.lblRestartRequired,
            this.btnEditModule,
            this.btnDeleteModule,
            this.btnCheckUpdates,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.toolStripButton1});
            this.tbModules.Location = new System.Drawing.Point(3, 3);
            this.tbModules.Name = "tbModules";
            this.tbModules.Size = new System.Drawing.Size(517, 25);
            this.tbModules.TabIndex = 0;
            // 
            // btnAddModule
            // 
            this.btnAddModule.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddModule.Image = global::PoshSec.Framework.Properties.Resources.pagewhiteadd;
            this.btnAddModule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddModule.Name = "btnAddModule";
            this.btnAddModule.Size = new System.Drawing.Size(23, 22);
            this.btnAddModule.ToolTipText = "Add Module";
            this.btnAddModule.Click += new System.EventHandler(this.btnAddModule_Click);
            // 
            // pbStatus
            // 
            this.pbStatus.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(100, 22);
            // 
            // lblStatus
            // 
            this.lblStatus.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 22);
            // 
            // lblRestartRequired
            // 
            this.lblRestartRequired.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lblRestartRequired.ForeColor = System.Drawing.Color.Red;
            this.lblRestartRequired.Name = "lblRestartRequired";
            this.lblRestartRequired.Size = new System.Drawing.Size(93, 22);
            this.lblRestartRequired.Text = "Restart required!";
            this.lblRestartRequired.Visible = false;
            // 
            // btnEditModule
            // 
            this.btnEditModule.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditModule.Enabled = false;
            this.btnEditModule.Image = global::PoshSec.Framework.Properties.Resources.pagewhiteedit;
            this.btnEditModule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditModule.Name = "btnEditModule";
            this.btnEditModule.Size = new System.Drawing.Size(23, 22);
            this.btnEditModule.Text = "Edit Module";
            this.btnEditModule.Click += new System.EventHandler(this.btnEditModule_Click);
            // 
            // btnDeleteModule
            // 
            this.btnDeleteModule.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDeleteModule.Enabled = false;
            this.btnDeleteModule.Image = global::PoshSec.Framework.Properties.Resources.pagewhitedelete;
            this.btnDeleteModule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteModule.Name = "btnDeleteModule";
            this.btnDeleteModule.Size = new System.Drawing.Size(23, 22);
            this.btnDeleteModule.Text = "Delete Module";
            this.btnDeleteModule.Click += new System.EventHandler(this.btnDeleteModule_Click);
            // 
            // btnCheckUpdates
            // 
            this.btnCheckUpdates.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCheckUpdates.Enabled = false;
            this.btnCheckUpdates.Image = global::PoshSec.Framework.Properties.Resources.pagewhiteget;
            this.btnCheckUpdates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCheckUpdates.Name = "btnCheckUpdates";
            this.btnCheckUpdates.Size = new System.Drawing.Size(23, 22);
            this.btnCheckUpdates.Text = "Check for Updates";
            this.btnCheckUpdates.ToolTipText = "Check for Updates";
            this.btnCheckUpdates.Click += new System.EventHandler(this.btnCheckUpdates_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(81, 22);
            this.toolStripLabel1.Text = "Update Alerts:";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::PoshSec.Framework.Properties.Resources.dialogyes;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(43, 22);
            this.toolStripButton1.Text = "On";
            // 
            // tabProxy
            // 
            this.tabProxy.Controls.Add(this.proxyPreferenceGroupBox1);
            this.tabProxy.Location = new System.Drawing.Point(4, 22);
            this.tabProxy.Name = "tabProxy";
            this.tabProxy.Padding = new System.Windows.Forms.Padding(3);
            this.tabProxy.Size = new System.Drawing.Size(523, 223);
            this.tabProxy.TabIndex = 3;
            this.tabProxy.Text = "Proxy";
            this.tabProxy.UseVisualStyleBackColor = true;
            // 
            // proxyPreferenceGroupBox1
            // 
            this.proxyPreferenceGroupBox1.Controls.Add(this.radNoProxy);
            this.proxyPreferenceGroupBox1.Controls.Add(this.label6);
            this.proxyPreferenceGroupBox1.Controls.Add(this.radSystemProxy);
            this.proxyPreferenceGroupBox1.Controls.Add(this.txbProxyPort);
            this.proxyPreferenceGroupBox1.Controls.Add(this.radManualProxy);
            this.proxyPreferenceGroupBox1.Controls.Add(this.label5);
            this.proxyPreferenceGroupBox1.Controls.Add(this.txbProxyHost);
            this.proxyPreferenceGroupBox1.Location = new System.Drawing.Point(8, 6);
            this.proxyPreferenceGroupBox1.Name = "proxyPreferenceGroupBox1";
            this.proxyPreferenceGroupBox1.Selected = ProxyPreference.None;
            this.proxyPreferenceGroupBox1.Size = new System.Drawing.Size(347, 139);
            this.proxyPreferenceGroupBox1.TabIndex = 0;
            this.proxyPreferenceGroupBox1.TabStop = false;
            this.proxyPreferenceGroupBox1.Text = "Proxy";
            // 
            // radNoProxy
            // 
            this.radNoProxy.AutoSize = true;
            this.radNoProxy.Checked = true;
            this.radNoProxy.Location = new System.Drawing.Point(20, 20);
            this.radNoProxy.Name = "radNoProxy";
            this.radNoProxy.Size = new System.Drawing.Size(69, 17);
            this.radNoProxy.TabIndex = 0;
            this.radNoProxy.TabStop = true;
            this.radNoProxy.Tag = "None";
            this.radNoProxy.Text = "No proxy";
            this.radNoProxy.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(217, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Port:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // radSystemProxy
            // 
            this.radSystemProxy.AutoSize = true;
            this.radSystemProxy.Location = new System.Drawing.Point(20, 43);
            this.radSystemProxy.Name = "radSystemProxy";
            this.radSystemProxy.Size = new System.Drawing.Size(111, 17);
            this.radSystemProxy.TabIndex = 1;
            this.radSystemProxy.Tag = "System";
            this.radSystemProxy.Text = "Use system proxy";
            this.radSystemProxy.UseVisualStyleBackColor = true;
            // 
            // txbProxyPort
            // 
            this.txbProxyPort.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::PoshSec.Framework.Properties.Settings.Default, "ProxyPort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txbProxyPort.Location = new System.Drawing.Point(254, 97);
            this.txbProxyPort.Name = "txbProxyPort";
            this.txbProxyPort.Size = new System.Drawing.Size(62, 21);
            this.txbProxyPort.TabIndex = 6;
            this.txbProxyPort.Text = global::PoshSec.Framework.Properties.Settings.Default.ProxyHost;
            // 
            // radManualProxy
            // 
            this.radManualProxy.AutoSize = true;
            this.radManualProxy.Location = new System.Drawing.Point(20, 66);
            this.radManualProxy.Name = "radManualProxy";
            this.radManualProxy.Size = new System.Drawing.Size(160, 17);
            this.radManualProxy.TabIndex = 2;
            this.radManualProxy.Tag = "Manual";
            this.radManualProxy.Text = "Manual proxy configuration:";
            this.radManualProxy.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Proxy host:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txbProxyHost
            // 
            this.txbProxyHost.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::PoshSec.Framework.Properties.Settings.Default, "ProxyHost", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txbProxyHost.Location = new System.Drawing.Point(95, 97);
            this.txbProxyHost.Name = "txbProxyHost";
            this.txbProxyHost.Size = new System.Drawing.Size(100, 21);
            this.txbProxyHost.TabIndex = 4;
            this.txbProxyHost.Text = global::PoshSec.Framework.Properties.Settings.Default.ProxyHost;
            // 
            // ttPSFHelp
            // 
            this.ttPSFHelp.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.ttPSFHelp.ToolTipTitle = "PoshSec Framework Help";
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(531, 283);
            this.ControlBox = false;
            this.Controls.Add(this.tcSettings);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.panel1.ResumeLayout(false);
            this.tbpGeneral.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.gbNameChecking.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.gbFirstTime.ResumeLayout(false);
            this.gbScriptSetting.ResumeLayout(false);
            this.pnlGithubAPIKey.ResumeLayout(false);
            this.pnlGithubAPIKey.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tcSettings.ResumeLayout(false);
            this.tbpLogging.ResumeLayout(false);
            this.tbpLogging.PerformLayout();
            this.gbSyslogInfo.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSyslogPort)).EndInit();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tbpModules.ResumeLayout(false);
            this.tbpModules.PerformLayout();
            this.tbModules.ResumeLayout(false);
            this.tbModules.PerformLayout();
            this.tabProxy.ResumeLayout(false);
            this.proxyPreferenceGroupBox1.ResumeLayout(false);
            this.proxyPreferenceGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabPage tbpGeneral;
        private System.Windows.Forms.TabControl tcSettings;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtScriptDirectory;
        private System.Windows.Forms.Button btnBrowseScript;
        private System.Windows.Forms.Label lblScriptDirectory;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtModuleDirectory;
        private System.Windows.Forms.Button btnBrowseModule;
        private System.Windows.Forms.Label lblModuleDirectory;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.GroupBox gbScriptSetting;
        private System.Windows.Forms.ComboBox cmbScriptDefAction;
        private System.Windows.Forms.Label lblScriptDefAction;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TextBox txtPSExecPath;
        private System.Windows.Forms.Button btnBrowsePSExec;
        private System.Windows.Forms.Label lblPSExecPath;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.TextBox txtSchFile;
        private System.Windows.Forms.Button btnBrowseSchFile;
        private System.Windows.Forms.Label lblSchFile;
        private System.Windows.Forms.GroupBox gbFirstTime;
        private System.Windows.Forms.ComboBox cmbFirstTime;
        private System.Windows.Forms.Label lblShowFirstTime;
        private System.Windows.Forms.TabPage tbpModules;
        private System.Windows.Forms.ListView lvwModules;
        private System.Windows.Forms.ColumnHeader chModName;
        private System.Windows.Forms.ToolStrip tbModules;
        private System.Windows.Forms.ColumnHeader chRepository;
        private System.Windows.Forms.ColumnHeader chBranch;
        private System.Windows.Forms.ToolStripButton btnAddModule;
        private System.Windows.Forms.ToolStripProgressBar pbStatus;
        private System.Windows.Forms.ToolStripLabel lblStatus;
        private System.Windows.Forms.ToolStripLabel lblRestartRequired;
        private System.Windows.Forms.Panel pnlGithubAPIKey;
        private System.Windows.Forms.Label lblGithubAPIKey;
        private System.Windows.Forms.TextBox txtGithubAPIKey;
        private System.Windows.Forms.Button btnGithubHelp;
        private System.Windows.Forms.ToolTip ttPSFHelp;
        private System.Windows.Forms.ColumnHeader chLastModified;
        private System.Windows.Forms.ToolStripButton btnEditModule;
        private System.Windows.Forms.ToolStripButton btnDeleteModule;
        private System.Windows.Forms.ToolStripButton btnCheckUpdates;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.GroupBox gbNameChecking;
        private System.Windows.Forms.TabPage tbpLogging;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.TextBox txtAlertLog;
        private System.Windows.Forms.Button btnBrowseAlertLog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtOutputLog;
        private System.Windows.Forms.Button btnBrowseOutputLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ckOutputLog;
        private System.Windows.Forms.CheckBox ckAlertLog;
        private System.Windows.Forms.GroupBox gbSyslogInfo;
        private System.Windows.Forms.CheckBox ckUseSyslog;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.TextBox txtSyslogServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown txtSyslogPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.CheckBox ckSaveSystems;
        private System.Windows.Forms.Label lblSaveSystems;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.CheckBox ckNameCheck;
        private System.Windows.Forms.Label lblNameChecking;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.CheckBox ckShowinTaskbar;
        private System.Windows.Forms.Label lblShowinTaskbar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.TabPage tabProxy;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txbProxyHost;
        private System.Windows.Forms.RadioButton radManualProxy;
        private System.Windows.Forms.RadioButton radSystemProxy;
        private System.Windows.Forms.RadioButton radNoProxy;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txbProxyPort;
        private ProxyPreferenceGroupBox proxyPreferenceGroupBox1;
    }
}