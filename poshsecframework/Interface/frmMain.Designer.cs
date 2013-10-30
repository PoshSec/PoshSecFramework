namespace poshsecframework
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Local Network");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Networks", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuScan = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCheckforUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPSFWiki = new System.Windows.Forms.ToolStripMenuItem();
            this.stsMain = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblsbSpacer = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.tbMain = new System.Windows.Forms.ToolStrip();
            this.btnAddNetwork = new System.Windows.Forms.ToolStripButton();
            this.btnAddSystem = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lvwScripts = new System.Windows.Forms.ListView();
            this.chScriptName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmnuScripts = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmbtnRunScript = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbtnSchedScript = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbtnViewScript = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuScrHyphen1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuScriptGetHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.imgList16 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.btnRefreshScripts = new System.Windows.Forms.ToolStripButton();
            this.btnRunScript = new System.Windows.Forms.ToolStripButton();
            this.btnViewScript = new System.Windows.Forms.ToolStripButton();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lvwCommands = new System.Windows.Forms.ListView();
            this.chLibName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLibModule = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmnuCommands = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCmdGetHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.imgListLibrary = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.cmbLibraryTypes = new System.Windows.Forms.ToolStripComboBox();
            this.btnLibraryRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnShowAliases = new System.Windows.Forms.ToolStripButton();
            this.btnShowFunctions = new System.Windows.Forms.ToolStripButton();
            this.btnShowCmdlets = new System.Windows.Forms.ToolStripButton();
            this.tvwNetworks = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnRefreshNetworks = new System.Windows.Forms.ToolStripButton();
            this.btnCancelScan = new System.Windows.Forms.ToolStripButton();
            this.pnlSystems = new System.Windows.Forms.SplitContainer();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tbpSystems = new System.Windows.Forms.TabPage();
            this.lvwSystems = new System.Windows.Forms.ListView();
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMAC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chClientInstalled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAlerts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLastScan = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbpPowerShell = new System.Windows.Forms.TabPage();
            this.txtPShellOutput = new poshsecframework.Controls.RichTextBoxCaret();
            this.tbpSchedScripts = new System.Windows.Forms.TabPage();
            this.lvwSchedule = new System.Windows.Forms.ListView();
            this.chSchScriptName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSchParams = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSchSchedule = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSchRunAs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tcSystem = new System.Windows.Forms.TabControl();
            this.tbpAlerts = new System.Windows.Forms.TabPage();
            this.lvwAlerts = new System.Windows.Forms.ListView();
            this.chSeverity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTimeStamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chScript = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imgListAlerts = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.btnClearAlerts = new System.Windows.Forms.ToolStripButton();
            this.btnAlert_MarkResolved = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslblDisplay = new System.Windows.Forms.ToolStripLabel();
            this.btnAlert_Delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
            this.tbpScripts = new System.Windows.Forms.TabPage();
            this.lvwActiveScripts = new System.Windows.Forms.ListView();
            this.chActScrScriptName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chActScrStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmnuActiveScripts = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmbtnCancelScript = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip5 = new System.Windows.Forms.ToolStrip();
            this.cmnuHosts = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.powerShellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.waucheckps1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain.SuspendLayout();
            this.stsMain.SuspendLayout();
            this.tbMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.Panel1.SuspendLayout();
            this.pnlMain.Panel2.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.cmnuScripts.SuspendLayout();
            this.toolStrip4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.cmnuCommands.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSystems)).BeginInit();
            this.pnlSystems.Panel1.SuspendLayout();
            this.pnlSystems.Panel2.SuspendLayout();
            this.pnlSystems.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tbpSystems.SuspendLayout();
            this.tbpPowerShell.SuspendLayout();
            this.tbpSchedScripts.SuspendLayout();
            this.tcSystem.SuspendLayout();
            this.tbpAlerts.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.tbpScripts.SuspendLayout();
            this.cmnuActiveScripts.SuspendLayout();
            this.cmnuHosts.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuTools,
            this.mnuHelp});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(1205, 24);
            this.mnuMain.TabIndex = 0;
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuScan,
            this.mnuExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuScan
            // 
            this.mnuScan.Name = "mnuScan";
            this.mnuScan.Size = new System.Drawing.Size(99, 22);
            this.mnuScan.Text = "Scan";
            this.mnuScan.Click += new System.EventHandler(this.mnuScan_Click);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(99, 22);
            this.mnuExit.Text = "E&xit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOptions});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(48, 20);
            this.mnuTools.Text = "&Tools";
            // 
            // mnuOptions
            // 
            this.mnuOptions.Image = global::poshsecframework.Properties.Resources.systemsettings;
            this.mnuOptions.Name = "mnuOptions";
            this.mnuOptions.Size = new System.Drawing.Size(125, 22);
            this.mnuOptions.Text = "Options...";
            this.mnuOptions.Click += new System.EventHandler(this.mnuOptions_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCheckforUpdates,
            this.mnuPSFWiki});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuCheckforUpdates
            // 
            this.mnuCheckforUpdates.Name = "mnuCheckforUpdates";
            this.mnuCheckforUpdates.Size = new System.Drawing.Size(206, 22);
            this.mnuCheckforUpdates.Text = "Check for Updates...";
            this.mnuCheckforUpdates.Click += new System.EventHandler(this.mnuCheckforUpdates_Click);
            // 
            // mnuPSFWiki
            // 
            this.mnuPSFWiki.Name = "mnuPSFWiki";
            this.mnuPSFWiki.Size = new System.Drawing.Size(206, 22);
            this.mnuPSFWiki.Text = "PoshSec Framework Wiki";
            this.mnuPSFWiki.Click += new System.EventHandler(this.mnuPSFWiki_Click);
            // 
            // stsMain
            // 
            this.stsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.lblsbSpacer,
            this.pbStatus});
            this.stsMain.Location = new System.Drawing.Point(0, 623);
            this.stsMain.Name = "stsMain";
            this.stsMain.Size = new System.Drawing.Size(1205, 22);
            this.stsMain.TabIndex = 1;
            this.stsMain.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(39, 17);
            this.lblStatus.Text = "Ready";
            // 
            // lblsbSpacer
            // 
            this.lblsbSpacer.Name = "lblsbSpacer";
            this.lblsbSpacer.Size = new System.Drawing.Size(1151, 17);
            this.lblsbSpacer.Spring = true;
            // 
            // pbStatus
            // 
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(200, 16);
            this.pbStatus.Visible = false;
            // 
            // tbMain
            // 
            this.tbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddNetwork,
            this.btnAddSystem});
            this.tbMain.Location = new System.Drawing.Point(0, 24);
            this.tbMain.Name = "tbMain";
            this.tbMain.Size = new System.Drawing.Size(1205, 25);
            this.tbMain.TabIndex = 2;
            // 
            // btnAddNetwork
            // 
            this.btnAddNetwork.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddNetwork.Image = global::poshsecframework.Properties.Resources.Diagram;
            this.btnAddNetwork.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddNetwork.Name = "btnAddNetwork";
            this.btnAddNetwork.Size = new System.Drawing.Size(23, 22);
            this.btnAddNetwork.Click += new System.EventHandler(this.btnAddNetwork_Click);
            // 
            // btnAddSystem
            // 
            this.btnAddSystem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddSystem.Image = global::poshsecframework.Properties.Resources.ServerExecute;
            this.btnAddSystem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddSystem.Name = "btnAddSystem";
            this.btnAddSystem.Size = new System.Drawing.Size(23, 22);
            this.btnAddSystem.Click += new System.EventHandler(this.btnAddSystem_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.pnlMain.Location = new System.Drawing.Point(0, 49);
            this.pnlMain.Name = "pnlMain";
            // 
            // pnlMain.Panel1
            // 
            this.pnlMain.Panel1.Controls.Add(this.panel1);
            this.pnlMain.Panel1.Controls.Add(this.splitter2);
            this.pnlMain.Panel1.Controls.Add(this.splitter1);
            this.pnlMain.Panel1.Controls.Add(this.panel2);
            this.pnlMain.Panel1.Controls.Add(this.tvwNetworks);
            this.pnlMain.Panel1.Controls.Add(this.toolStrip1);
            // 
            // pnlMain.Panel2
            // 
            this.pnlMain.Panel2.Controls.Add(this.pnlSystems);
            this.pnlMain.Size = new System.Drawing.Size(1205, 574);
            this.pnlMain.SplitterDistance = 245;
            this.pnlMain.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lvwScripts);
            this.panel1.Controls.Add(this.toolStrip4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 151);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(245, 235);
            this.panel1.TabIndex = 10;
            // 
            // lvwScripts
            // 
            this.lvwScripts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chScriptName});
            this.lvwScripts.ContextMenuStrip = this.cmnuScripts;
            this.lvwScripts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwScripts.FullRowSelect = true;
            this.lvwScripts.HideSelection = false;
            this.lvwScripts.Location = new System.Drawing.Point(0, 25);
            this.lvwScripts.MultiSelect = false;
            this.lvwScripts.Name = "lvwScripts";
            this.lvwScripts.Size = new System.Drawing.Size(245, 210);
            this.lvwScripts.SmallImageList = this.imgList16;
            this.lvwScripts.TabIndex = 1;
            this.lvwScripts.UseCompatibleStateImageBehavior = false;
            this.lvwScripts.View = System.Windows.Forms.View.Details;
            this.lvwScripts.SelectedIndexChanged += new System.EventHandler(this.lvwScripts_SelectedIndexChanged);
            this.lvwScripts.DoubleClick += new System.EventHandler(this.lvwScripts_DoubleClick);
            // 
            // chScriptName
            // 
            this.chScriptName.Text = "Script Name";
            this.chScriptName.Width = 220;
            // 
            // cmnuScripts
            // 
            this.cmnuScripts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmbtnRunScript,
            this.cmbtnSchedScript,
            this.cmbtnViewScript,
            this.mnuScrHyphen1,
            this.mnuScriptGetHelp});
            this.cmnuScripts.Name = "contextMenuStrip1";
            this.cmnuScripts.Size = new System.Drawing.Size(156, 98);
            this.cmnuScripts.Opening += new System.ComponentModel.CancelEventHandler(this.cmnuScripts_Opening);
            // 
            // cmbtnRunScript
            // 
            this.cmbtnRunScript.Name = "cmbtnRunScript";
            this.cmbtnRunScript.Size = new System.Drawing.Size(155, 22);
            this.cmbtnRunScript.Text = "Run Script";
            this.cmbtnRunScript.Click += new System.EventHandler(this.cmbtnRunScript_Click);
            // 
            // cmbtnSchedScript
            // 
            this.cmbtnSchedScript.Name = "cmbtnSchedScript";
            this.cmbtnSchedScript.Size = new System.Drawing.Size(155, 22);
            this.cmbtnSchedScript.Text = "Schedule Script";
            this.cmbtnSchedScript.Click += new System.EventHandler(this.cmbtnSchedScript_Click);
            // 
            // cmbtnViewScript
            // 
            this.cmbtnViewScript.Name = "cmbtnViewScript";
            this.cmbtnViewScript.Size = new System.Drawing.Size(155, 22);
            this.cmbtnViewScript.Text = "View Script";
            this.cmbtnViewScript.Click += new System.EventHandler(this.cmbtnViewScript_Click);
            // 
            // mnuScrHyphen1
            // 
            this.mnuScrHyphen1.Name = "mnuScrHyphen1";
            this.mnuScrHyphen1.Size = new System.Drawing.Size(152, 6);
            // 
            // mnuScriptGetHelp
            // 
            this.mnuScriptGetHelp.Name = "mnuScriptGetHelp";
            this.mnuScriptGetHelp.Size = new System.Drawing.Size(155, 22);
            this.mnuScriptGetHelp.Text = "Get-Help";
            this.mnuScriptGetHelp.Click += new System.EventHandler(this.mnuScriptGetHelp_Click);
            // 
            // imgList16
            // 
            this.imgList16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList16.ImageStream")));
            this.imgList16.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList16.Images.SetKeyName(0, "FolderClosed.png");
            this.imgList16.Images.SetKeyName(1, "FolderOpen.png");
            this.imgList16.Images.SetKeyName(2, "Server.png");
            this.imgList16.Images.SetKeyName(3, "Diagram.png");
            this.imgList16.Images.SetKeyName(4, "psscript.ico");
            this.imgList16.Images.SetKeyName(5, "view-calendar-tasks.png");
            this.imgList16.Images.SetKeyName(6, "psficon.ico");
            this.imgList16.Images.SetKeyName(7, "computer-server.png");
            // 
            // toolStrip4
            // 
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefreshScripts,
            this.btnRunScript,
            this.btnViewScript});
            this.toolStrip4.Location = new System.Drawing.Point(0, 0);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.Size = new System.Drawing.Size(245, 25);
            this.toolStrip4.TabIndex = 0;
            this.toolStrip4.Text = "toolStrip4";
            // 
            // btnRefreshScripts
            // 
            this.btnRefreshScripts.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefreshScripts.Image = global::poshsecframework.Properties.Resources.viewrefresh7;
            this.btnRefreshScripts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefreshScripts.Name = "btnRefreshScripts";
            this.btnRefreshScripts.Size = new System.Drawing.Size(23, 22);
            this.btnRefreshScripts.ToolTipText = "Refresh Scripts";
            this.btnRefreshScripts.Click += new System.EventHandler(this.btnRefreshScripts_Click);
            // 
            // btnRunScript
            // 
            this.btnRunScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRunScript.Enabled = false;
            this.btnRunScript.Image = global::poshsecframework.Properties.Resources.run;
            this.btnRunScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRunScript.Name = "btnRunScript";
            this.btnRunScript.Size = new System.Drawing.Size(23, 22);
            this.btnRunScript.ToolTipText = "Run Script";
            this.btnRunScript.Click += new System.EventHandler(this.btnRunScript_Click);
            // 
            // btnViewScript
            // 
            this.btnViewScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnViewScript.Enabled = false;
            this.btnViewScript.Image = global::poshsecframework.Properties.Resources.documentopen7;
            this.btnViewScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnViewScript.Name = "btnViewScript";
            this.btnViewScript.Size = new System.Drawing.Size(23, 22);
            this.btnViewScript.ToolTipText = "View Script";
            this.btnViewScript.Click += new System.EventHandler(this.btnViewScript_Click);
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter2.Location = new System.Drawing.Point(0, 144);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(245, 7);
            this.splitter2.TabIndex = 9;
            this.splitter2.TabStop = false;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 386);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(245, 7);
            this.splitter1.TabIndex = 8;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lvwCommands);
            this.panel2.Controls.Add(this.toolStrip2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 393);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(245, 181);
            this.panel2.TabIndex = 7;
            // 
            // lvwCommands
            // 
            this.lvwCommands.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chLibName,
            this.chLibModule});
            this.lvwCommands.ContextMenuStrip = this.cmnuCommands;
            this.lvwCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwCommands.FullRowSelect = true;
            this.lvwCommands.HideSelection = false;
            this.lvwCommands.Location = new System.Drawing.Point(0, 25);
            this.lvwCommands.MultiSelect = false;
            this.lvwCommands.Name = "lvwCommands";
            this.lvwCommands.ShowItemToolTips = true;
            this.lvwCommands.Size = new System.Drawing.Size(245, 156);
            this.lvwCommands.SmallImageList = this.imgListLibrary;
            this.lvwCommands.TabIndex = 2;
            this.lvwCommands.UseCompatibleStateImageBehavior = false;
            this.lvwCommands.View = System.Windows.Forms.View.Details;
            this.lvwCommands.DoubleClick += new System.EventHandler(this.lvwCommands_DoubleClick);
            // 
            // chLibName
            // 
            this.chLibName.Text = "Name";
            this.chLibName.Width = 102;
            // 
            // chLibModule
            // 
            this.chLibModule.Text = "Module";
            this.chLibModule.Width = 118;
            // 
            // cmnuCommands
            // 
            this.cmnuCommands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCmdGetHelp});
            this.cmnuCommands.Name = "cmnuCommands";
            this.cmnuCommands.Size = new System.Drawing.Size(123, 26);
            // 
            // mnuCmdGetHelp
            // 
            this.mnuCmdGetHelp.Name = "mnuCmdGetHelp";
            this.mnuCmdGetHelp.Size = new System.Drawing.Size(122, 22);
            this.mnuCmdGetHelp.Text = "Get-Help";
            this.mnuCmdGetHelp.Click += new System.EventHandler(this.mnuCmdGetHelp_Click);
            // 
            // imgListLibrary
            // 
            this.imgListLibrary.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListLibrary.ImageStream")));
            this.imgListLibrary.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListLibrary.Images.SetKeyName(0, "tag-blue.png");
            this.imgListLibrary.Images.SetKeyName(1, "tag-green.png");
            this.imgListLibrary.Images.SetKeyName(2, "tag-orange.png");
            this.imgListLibrary.Images.SetKeyName(3, "tag-red.png");
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmbLibraryTypes,
            this.btnLibraryRefresh,
            this.btnShowAliases,
            this.btnShowFunctions,
            this.btnShowCmdlets});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(245, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // cmbLibraryTypes
            // 
            this.cmbLibraryTypes.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmbLibraryTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLibraryTypes.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cmbLibraryTypes.Items.AddRange(new object[] {
            "All",
            "PoshSecFramework"});
            this.cmbLibraryTypes.Name = "cmbLibraryTypes";
            this.cmbLibraryTypes.Size = new System.Drawing.Size(121, 25);
            this.cmbLibraryTypes.SelectedIndexChanged += new System.EventHandler(this.cmbLibraryTypes_SelectedIndexChanged);
            // 
            // btnLibraryRefresh
            // 
            this.btnLibraryRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLibraryRefresh.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.btnLibraryRefresh.Image = global::poshsecframework.Properties.Resources.viewrefresh7;
            this.btnLibraryRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLibraryRefresh.Name = "btnLibraryRefresh";
            this.btnLibraryRefresh.Size = new System.Drawing.Size(23, 22);
            this.btnLibraryRefresh.Text = "Refresh";
            this.btnLibraryRefresh.ToolTipText = "Refresh";
            this.btnLibraryRefresh.Click += new System.EventHandler(this.btnLibraryRefresh_Click);
            // 
            // btnShowAliases
            // 
            this.btnShowAliases.Checked = true;
            this.btnShowAliases.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnShowAliases.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowAliases.Image = global::poshsecframework.Properties.Resources.tagred;
            this.btnShowAliases.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowAliases.Name = "btnShowAliases";
            this.btnShowAliases.Size = new System.Drawing.Size(23, 22);
            this.btnShowAliases.ToolTipText = "Show Aliases";
            this.btnShowAliases.Click += new System.EventHandler(this.btnShowAliases_Click);
            // 
            // btnShowFunctions
            // 
            this.btnShowFunctions.Checked = true;
            this.btnShowFunctions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnShowFunctions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowFunctions.Image = global::poshsecframework.Properties.Resources.tagblue;
            this.btnShowFunctions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowFunctions.Name = "btnShowFunctions";
            this.btnShowFunctions.Size = new System.Drawing.Size(23, 22);
            this.btnShowFunctions.Text = "toolStripButton5";
            this.btnShowFunctions.ToolTipText = "Show Functions";
            this.btnShowFunctions.Click += new System.EventHandler(this.btnShowFunctions_Click);
            // 
            // btnShowCmdlets
            // 
            this.btnShowCmdlets.Checked = true;
            this.btnShowCmdlets.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnShowCmdlets.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowCmdlets.Image = global::poshsecframework.Properties.Resources.taggreen;
            this.btnShowCmdlets.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowCmdlets.Name = "btnShowCmdlets";
            this.btnShowCmdlets.Size = new System.Drawing.Size(23, 22);
            this.btnShowCmdlets.Text = "toolStripButton6";
            this.btnShowCmdlets.ToolTipText = "Show Cmdlets";
            this.btnShowCmdlets.Click += new System.EventHandler(this.btnShowCmdlets_Click);
            // 
            // tvwNetworks
            // 
            this.tvwNetworks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tvwNetworks.Dock = System.Windows.Forms.DockStyle.Top;
            this.tvwNetworks.FullRowSelect = true;
            this.tvwNetworks.HideSelection = false;
            this.tvwNetworks.ImageIndex = 0;
            this.tvwNetworks.ImageList = this.imgList16;
            this.tvwNetworks.Location = new System.Drawing.Point(0, 25);
            this.tvwNetworks.Name = "tvwNetworks";
            treeNode1.ImageKey = "Diagram.png";
            treeNode1.Name = "ndNone";
            treeNode1.SelectedImageKey = "Diagram.png";
            treeNode1.Tag = "1";
            treeNode1.Text = "Local Network";
            treeNode2.Name = "ndNetwork";
            treeNode2.Text = "Networks";
            this.tvwNetworks.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.tvwNetworks.SelectedImageIndex = 1;
            this.tvwNetworks.ShowPlusMinus = false;
            this.tvwNetworks.ShowRootLines = false;
            this.tvwNetworks.Size = new System.Drawing.Size(245, 119);
            this.tvwNetworks.TabIndex = 5;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefreshNetworks,
            this.btnCancelScan});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(245, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnRefreshNetworks
            // 
            this.btnRefreshNetworks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefreshNetworks.Image = global::poshsecframework.Properties.Resources.viewrefresh7;
            this.btnRefreshNetworks.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefreshNetworks.Name = "btnRefreshNetworks";
            this.btnRefreshNetworks.Size = new System.Drawing.Size(23, 22);
            this.btnRefreshNetworks.ToolTipText = "Refresh Networks";
            this.btnRefreshNetworks.Click += new System.EventHandler(this.btnRefreshNetworks_Click);
            // 
            // btnCancelScan
            // 
            this.btnCancelScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelScan.Enabled = false;
            this.btnCancelScan.Image = global::poshsecframework.Properties.Resources.dialogcancel;
            this.btnCancelScan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelScan.Name = "btnCancelScan";
            this.btnCancelScan.Size = new System.Drawing.Size(23, 22);
            this.btnCancelScan.ToolTipText = "Cancel Scan";
            this.btnCancelScan.Click += new System.EventHandler(this.btnCancelScan_Click);
            // 
            // pnlSystems
            // 
            this.pnlSystems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSystems.Location = new System.Drawing.Point(0, 0);
            this.pnlSystems.Name = "pnlSystems";
            this.pnlSystems.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // pnlSystems.Panel1
            // 
            this.pnlSystems.Panel1.Controls.Add(this.tcMain);
            // 
            // pnlSystems.Panel2
            // 
            this.pnlSystems.Panel2.Controls.Add(this.tcSystem);
            this.pnlSystems.Size = new System.Drawing.Size(956, 574);
            this.pnlSystems.SplitterDistance = 340;
            this.pnlSystems.TabIndex = 0;
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tbpSystems);
            this.tcMain.Controls.Add(this.tbpPowerShell);
            this.tcMain.Controls.Add(this.tbpSchedScripts);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.ImageList = this.imgList16;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(956, 340);
            this.tcMain.TabIndex = 0;
            this.tcMain.Selected += new System.Windows.Forms.TabControlEventHandler(this.tcMain_Selected);
            // 
            // tbpSystems
            // 
            this.tbpSystems.Controls.Add(this.lvwSystems);
            this.tbpSystems.ImageIndex = 7;
            this.tbpSystems.Location = new System.Drawing.Point(4, 23);
            this.tbpSystems.Name = "tbpSystems";
            this.tbpSystems.Padding = new System.Windows.Forms.Padding(3);
            this.tbpSystems.Size = new System.Drawing.Size(948, 313);
            this.tbpSystems.TabIndex = 0;
            this.tbpSystems.Text = "Systems";
            this.tbpSystems.UseVisualStyleBackColor = true;
            // 
            // lvwSystems
            // 
            this.lvwSystems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvwSystems.CheckBoxes = true;
            this.lvwSystems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chIP,
            this.chMAC,
            this.chStatus,
            this.chClientInstalled,
            this.chAlerts,
            this.chLastScan});
            this.lvwSystems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwSystems.FullRowSelect = true;
            this.lvwSystems.HideSelection = false;
            this.lvwSystems.Location = new System.Drawing.Point(3, 3);
            this.lvwSystems.Name = "lvwSystems";
            this.lvwSystems.Size = new System.Drawing.Size(942, 307);
            this.lvwSystems.SmallImageList = this.imgList16;
            this.lvwSystems.TabIndex = 1;
            this.lvwSystems.UseCompatibleStateImageBehavior = false;
            this.lvwSystems.View = System.Windows.Forms.View.Details;
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 182;
            // 
            // chIP
            // 
            this.chIP.Text = "IP Address";
            this.chIP.Width = 105;
            // 
            // chMAC
            // 
            this.chMAC.Text = "MAC Address";
            this.chMAC.Width = 117;
            // 
            // chStatus
            // 
            this.chStatus.Text = "Status";
            this.chStatus.Width = 101;
            // 
            // chClientInstalled
            // 
            this.chClientInstalled.Text = "Client Installed";
            this.chClientInstalled.Width = 101;
            // 
            // chAlerts
            // 
            this.chAlerts.Text = "Alerts";
            // 
            // chLastScan
            // 
            this.chLastScan.Text = "Last Scan";
            this.chLastScan.Width = 173;
            // 
            // tbpPowerShell
            // 
            this.tbpPowerShell.BackColor = System.Drawing.Color.SteelBlue;
            this.tbpPowerShell.Controls.Add(this.txtPShellOutput);
            this.tbpPowerShell.ImageIndex = 4;
            this.tbpPowerShell.Location = new System.Drawing.Point(4, 23);
            this.tbpPowerShell.Name = "tbpPowerShell";
            this.tbpPowerShell.Padding = new System.Windows.Forms.Padding(3);
            this.tbpPowerShell.Size = new System.Drawing.Size(948, 313);
            this.tbpPowerShell.TabIndex = 1;
            this.tbpPowerShell.Text = "PowerShell";
            // 
            // txtPShellOutput
            // 
            this.txtPShellOutput.BackColor = System.Drawing.Color.SteelBlue;
            this.txtPShellOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPShellOutput.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtPShellOutput.DetectUrls = false;
            this.txtPShellOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPShellOutput.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPShellOutput.ForeColor = System.Drawing.Color.White;
            this.txtPShellOutput.Location = new System.Drawing.Point(3, 3);
            this.txtPShellOutput.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.txtPShellOutput.Name = "txtPShellOutput";
            this.txtPShellOutput.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtPShellOutput.Size = new System.Drawing.Size(942, 307);
            this.txtPShellOutput.TabIndex = 0;
            this.txtPShellOutput.Text = "psf > ";
            this.txtPShellOutput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPShellOutput_KeyDown);
            // 
            // tbpSchedScripts
            // 
            this.tbpSchedScripts.Controls.Add(this.lvwSchedule);
            this.tbpSchedScripts.ImageIndex = 5;
            this.tbpSchedScripts.Location = new System.Drawing.Point(4, 23);
            this.tbpSchedScripts.Name = "tbpSchedScripts";
            this.tbpSchedScripts.Padding = new System.Windows.Forms.Padding(3);
            this.tbpSchedScripts.Size = new System.Drawing.Size(948, 313);
            this.tbpSchedScripts.TabIndex = 2;
            this.tbpSchedScripts.Text = "Scheduled Scripts";
            this.tbpSchedScripts.UseVisualStyleBackColor = true;
            // 
            // lvwSchedule
            // 
            this.lvwSchedule.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chSchScriptName,
            this.chSchParams,
            this.chSchSchedule,
            this.chSchRunAs});
            this.lvwSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwSchedule.Location = new System.Drawing.Point(3, 3);
            this.lvwSchedule.Name = "lvwSchedule";
            this.lvwSchedule.Size = new System.Drawing.Size(942, 307);
            this.lvwSchedule.SmallImageList = this.imgList16;
            this.lvwSchedule.TabIndex = 0;
            this.lvwSchedule.UseCompatibleStateImageBehavior = false;
            this.lvwSchedule.View = System.Windows.Forms.View.Details;
            // 
            // chSchScriptName
            // 
            this.chSchScriptName.Text = "Script Name";
            this.chSchScriptName.Width = 150;
            // 
            // chSchParams
            // 
            this.chSchParams.Text = "Parameters";
            this.chSchParams.Width = 150;
            // 
            // chSchSchedule
            // 
            this.chSchSchedule.Text = "Schedule";
            this.chSchSchedule.Width = 210;
            // 
            // chSchRunAs
            // 
            this.chSchRunAs.Text = "Run As";
            this.chSchRunAs.Width = 150;
            // 
            // tcSystem
            // 
            this.tcSystem.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tcSystem.Controls.Add(this.tbpAlerts);
            this.tcSystem.Controls.Add(this.tbpScripts);
            this.tcSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSystem.Location = new System.Drawing.Point(0, 0);
            this.tcSystem.Name = "tcSystem";
            this.tcSystem.SelectedIndex = 0;
            this.tcSystem.Size = new System.Drawing.Size(956, 230);
            this.tcSystem.TabIndex = 0;
            // 
            // tbpAlerts
            // 
            this.tbpAlerts.Controls.Add(this.lvwAlerts);
            this.tbpAlerts.Controls.Add(this.toolStrip3);
            this.tbpAlerts.Location = new System.Drawing.Point(4, 4);
            this.tbpAlerts.Name = "tbpAlerts";
            this.tbpAlerts.Padding = new System.Windows.Forms.Padding(3);
            this.tbpAlerts.Size = new System.Drawing.Size(948, 204);
            this.tbpAlerts.TabIndex = 1;
            this.tbpAlerts.Text = "Alerts (0)";
            this.tbpAlerts.UseVisualStyleBackColor = true;
            // 
            // lvwAlerts
            // 
            this.lvwAlerts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chSeverity,
            this.chMessage,
            this.chTimeStamp,
            this.chScript});
            this.lvwAlerts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwAlerts.FullRowSelect = true;
            this.lvwAlerts.HideSelection = false;
            this.lvwAlerts.Location = new System.Drawing.Point(3, 28);
            this.lvwAlerts.Name = "lvwAlerts";
            this.lvwAlerts.Size = new System.Drawing.Size(942, 173);
            this.lvwAlerts.SmallImageList = this.imgListAlerts;
            this.lvwAlerts.TabIndex = 1;
            this.lvwAlerts.UseCompatibleStateImageBehavior = false;
            this.lvwAlerts.View = System.Windows.Forms.View.Details;
            // 
            // chSeverity
            // 
            this.chSeverity.Text = "Severity";
            this.chSeverity.Width = 100;
            // 
            // chMessage
            // 
            this.chMessage.Text = "Message";
            this.chMessage.Width = 535;
            // 
            // chTimeStamp
            // 
            this.chTimeStamp.Text = "Timestamp";
            this.chTimeStamp.Width = 133;
            // 
            // chScript
            // 
            this.chScript.Text = "Script";
            this.chScript.Width = 150;
            // 
            // imgListAlerts
            // 
            this.imgListAlerts.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListAlerts.ImageStream")));
            this.imgListAlerts.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListAlerts.Images.SetKeyName(0, "dialog-information-3.png");
            this.imgListAlerts.Images.SetKeyName(1, "dialog-error-4.png");
            this.imgListAlerts.Images.SetKeyName(2, "dialog-warning-3.png");
            this.imgListAlerts.Images.SetKeyName(3, "dialog-warning-2.png");
            this.imgListAlerts.Images.SetKeyName(4, "exclamation.png");
            // 
            // toolStrip3
            // 
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClearAlerts,
            this.btnAlert_MarkResolved,
            this.toolStripSeparator1,
            this.tslblDisplay,
            this.btnAlert_Delete,
            this.toolStripButton6,
            this.toolStripButton7,
            this.toolStripButton8,
            this.toolStripButton9});
            this.toolStrip3.Location = new System.Drawing.Point(3, 3);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(942, 25);
            this.toolStrip3.TabIndex = 0;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // btnClearAlerts
            // 
            this.btnClearAlerts.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClearAlerts.Image = global::poshsecframework.Properties.Resources.editclearlist;
            this.btnClearAlerts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClearAlerts.Name = "btnClearAlerts";
            this.btnClearAlerts.Size = new System.Drawing.Size(23, 22);
            this.btnClearAlerts.Text = "toolStripButton3";
            this.btnClearAlerts.ToolTipText = "Clear All Alerts";
            this.btnClearAlerts.Click += new System.EventHandler(this.btnClearAlerts_Click);
            // 
            // btnAlert_MarkResolved
            // 
            this.btnAlert_MarkResolved.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAlert_MarkResolved.Image = global::poshsecframework.Properties.Resources.dialogaccept;
            this.btnAlert_MarkResolved.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAlert_MarkResolved.Name = "btnAlert_MarkResolved";
            this.btnAlert_MarkResolved.Size = new System.Drawing.Size(23, 22);
            this.btnAlert_MarkResolved.Text = "toolStripButton3";
            this.btnAlert_MarkResolved.ToolTipText = "Mark Resolved";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tslblDisplay
            // 
            this.tslblDisplay.Name = "tslblDisplay";
            this.tslblDisplay.Size = new System.Drawing.Size(48, 22);
            this.tslblDisplay.Text = "Display:";
            // 
            // btnAlert_Delete
            // 
            this.btnAlert_Delete.Checked = true;
            this.btnAlert_Delete.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnAlert_Delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAlert_Delete.Image = global::poshsecframework.Properties.Resources.dialoginformation4;
            this.btnAlert_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAlert_Delete.Name = "btnAlert_Delete";
            this.btnAlert_Delete.Size = new System.Drawing.Size(23, 22);
            this.btnAlert_Delete.Text = "toolStripButton5";
            this.btnAlert_Delete.ToolTipText = "Show Information Alerts";
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.Checked = true;
            this.toolStripButton6.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = global::poshsecframework.Properties.Resources.dialogerror4;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton6.Text = "toolStripButton6";
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.Checked = true;
            this.toolStripButton7.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton7.Image = global::poshsecframework.Properties.Resources.dialogwarning3;
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton7.Text = "toolStripButton7";
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.Checked = true;
            this.toolStripButton8.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton8.Image = global::poshsecframework.Properties.Resources.dialogwarning2;
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton8.Text = "toolStripButton8";
            // 
            // toolStripButton9
            // 
            this.toolStripButton9.Checked = true;
            this.toolStripButton9.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton9.Image = global::poshsecframework.Properties.Resources.exclamation;
            this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton9.Name = "toolStripButton9";
            this.toolStripButton9.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton9.Text = "toolStripButton9";
            // 
            // tbpScripts
            // 
            this.tbpScripts.BackColor = System.Drawing.Color.Transparent;
            this.tbpScripts.Controls.Add(this.lvwActiveScripts);
            this.tbpScripts.Controls.Add(this.toolStrip5);
            this.tbpScripts.Location = new System.Drawing.Point(4, 4);
            this.tbpScripts.Name = "tbpScripts";
            this.tbpScripts.Padding = new System.Windows.Forms.Padding(3);
            this.tbpScripts.Size = new System.Drawing.Size(948, 204);
            this.tbpScripts.TabIndex = 2;
            this.tbpScripts.Text = "Active Scripts (0)";
            // 
            // lvwActiveScripts
            // 
            this.lvwActiveScripts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chActScrScriptName,
            this.chActScrStatus});
            this.lvwActiveScripts.ContextMenuStrip = this.cmnuActiveScripts;
            this.lvwActiveScripts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwActiveScripts.FullRowSelect = true;
            this.lvwActiveScripts.HideSelection = false;
            this.lvwActiveScripts.Location = new System.Drawing.Point(3, 28);
            this.lvwActiveScripts.Name = "lvwActiveScripts";
            this.lvwActiveScripts.Size = new System.Drawing.Size(942, 173);
            this.lvwActiveScripts.SmallImageList = this.imgList16;
            this.lvwActiveScripts.TabIndex = 1;
            this.lvwActiveScripts.UseCompatibleStateImageBehavior = false;
            this.lvwActiveScripts.View = System.Windows.Forms.View.Details;
            // 
            // chActScrScriptName
            // 
            this.chActScrScriptName.Text = "Script Name";
            this.chActScrScriptName.Width = 185;
            // 
            // chActScrStatus
            // 
            this.chActScrStatus.Text = "Status";
            this.chActScrStatus.Width = 242;
            // 
            // cmnuActiveScripts
            // 
            this.cmnuActiveScripts.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cmnuActiveScripts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmbtnCancelScript});
            this.cmnuActiveScripts.Name = "cmnuActiveScripts";
            this.cmnuActiveScripts.Size = new System.Drawing.Size(137, 26);
            this.cmnuActiveScripts.Opening += new System.ComponentModel.CancelEventHandler(this.cmnuActiveScripts_Opening);
            // 
            // cmbtnCancelScript
            // 
            this.cmbtnCancelScript.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cmbtnCancelScript.Name = "cmbtnCancelScript";
            this.cmbtnCancelScript.Size = new System.Drawing.Size(136, 22);
            this.cmbtnCancelScript.Text = "Cancel Script";
            this.cmbtnCancelScript.Click += new System.EventHandler(this.cmbtnCancelScript_Click);
            // 
            // toolStrip5
            // 
            this.toolStrip5.Location = new System.Drawing.Point(3, 3);
            this.toolStrip5.Name = "toolStrip5";
            this.toolStrip5.Size = new System.Drawing.Size(942, 25);
            this.toolStrip5.TabIndex = 0;
            this.toolStrip5.Text = "toolStrip5";
            // 
            // cmnuHosts
            // 
            this.cmnuHosts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.powerShellToolStripMenuItem});
            this.cmnuHosts.Name = "cmnuHosts";
            this.cmnuHosts.Size = new System.Drawing.Size(136, 26);
            // 
            // powerShellToolStripMenuItem
            // 
            this.powerShellToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.windowsUpdatesToolStripMenuItem});
            this.powerShellToolStripMenuItem.Name = "powerShellToolStripMenuItem";
            this.powerShellToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.powerShellToolStripMenuItem.Text = "Power Shell";
            // 
            // windowsUpdatesToolStripMenuItem
            // 
            this.windowsUpdatesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.waucheckps1ToolStripMenuItem});
            this.windowsUpdatesToolStripMenuItem.Name = "windowsUpdatesToolStripMenuItem";
            this.windowsUpdatesToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.windowsUpdatesToolStripMenuItem.Text = "Windows Updates";
            // 
            // waucheckps1ToolStripMenuItem
            // 
            this.waucheckps1ToolStripMenuItem.Name = "waucheckps1ToolStripMenuItem";
            this.waucheckps1ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.waucheckps1ToolStripMenuItem.Text = "waucheck.ps1";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 645);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.tbMain);
            this.Controls.Add(this.stsMain);
            this.Controls.Add(this.mnuMain);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PoshSec Framework";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.stsMain.ResumeLayout(false);
            this.stsMain.PerformLayout();
            this.tbMain.ResumeLayout(false);
            this.tbMain.PerformLayout();
            this.pnlMain.Panel1.ResumeLayout(false);
            this.pnlMain.Panel1.PerformLayout();
            this.pnlMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.cmnuScripts.ResumeLayout(false);
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.cmnuCommands.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnlSystems.Panel1.ResumeLayout(false);
            this.pnlSystems.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlSystems)).EndInit();
            this.pnlSystems.ResumeLayout(false);
            this.tcMain.ResumeLayout(false);
            this.tbpSystems.ResumeLayout(false);
            this.tbpPowerShell.ResumeLayout(false);
            this.tbpSchedScripts.ResumeLayout(false);
            this.tcSystem.ResumeLayout(false);
            this.tbpAlerts.ResumeLayout(false);
            this.tbpAlerts.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.tbpScripts.ResumeLayout(false);
            this.tbpScripts.PerformLayout();
            this.cmnuActiveScripts.ResumeLayout(false);
            this.cmnuHosts.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.StatusStrip stsMain;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStrip tbMain;
        private System.Windows.Forms.SplitContainer pnlMain;
        private System.Windows.Forms.ToolStripMenuItem mnuScan;
        private System.Windows.Forms.SplitContainer pnlSystems;
        private System.Windows.Forms.ImageList imgList16;
        private System.Windows.Forms.TabControl tcSystem;
        private System.Windows.Forms.TabPage tbpAlerts;
        private System.Windows.Forms.ToolStripButton btnAddNetwork;
        private System.Windows.Forms.ToolStripButton btnAddSystem;
        private System.Windows.Forms.ToolStripStatusLabel lblsbSpacer;
        private System.Windows.Forms.ToolStripProgressBar pbStatus;
        private System.Windows.Forms.TabPage tbpScripts;
        private System.Windows.Forms.ContextMenuStrip cmnuHosts;
        private System.Windows.Forms.ToolStripMenuItem powerShellToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowsUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem waucheckps1ToolStripMenuItem;
        private System.Windows.Forms.TreeView tvwNetworks;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ListView lvwAlerts;
        private System.Windows.Forms.ColumnHeader chSeverity;
        private System.Windows.Forms.ColumnHeader chMessage;
        private System.Windows.Forms.ColumnHeader chScript;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton btnClearAlerts;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tbpSystems;
        private System.Windows.Forms.ListView lvwSystems;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chIP;
        private System.Windows.Forms.ColumnHeader chMAC;
        private System.Windows.Forms.ColumnHeader chStatus;
        private System.Windows.Forms.ColumnHeader chClientInstalled;
        private System.Windows.Forms.ColumnHeader chAlerts;
        private System.Windows.Forms.ColumnHeader chLastScan;
        private System.Windows.Forms.TabPage tbpPowerShell;
        private System.Windows.Forms.ImageList imgListAlerts;
        private System.Windows.Forms.ImageList imgListLibrary;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView lvwCommands;
        private System.Windows.Forms.ColumnHeader chLibName;
        private System.Windows.Forms.ColumnHeader chLibModule;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripComboBox cmbLibraryTypes;
        private System.Windows.Forms.ToolStripButton btnLibraryRefresh;
        private System.Windows.Forms.ToolStripButton btnShowAliases;
        private System.Windows.Forms.ToolStripButton btnShowFunctions;
        private System.Windows.Forms.ToolStripButton btnShowCmdlets;
        private System.Windows.Forms.ToolStripButton btnAlert_MarkResolved;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnAlert_Delete;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ToolStripButton toolStripButton8;
        private System.Windows.Forms.ToolStripButton toolStripButton9;
        private System.Windows.Forms.ToolStripLabel tslblDisplay;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView lvwScripts;
        private System.Windows.Forms.ColumnHeader chScriptName;
        private System.Windows.Forms.ToolStrip toolStrip4;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TabPage tbpSchedScripts;
        private System.Windows.Forms.ListView lvwActiveScripts;
        private System.Windows.Forms.ToolStrip toolStrip5;
        private System.Windows.Forms.ColumnHeader chActScrScriptName;
        private System.Windows.Forms.ColumnHeader chActScrStatus;
        private System.Windows.Forms.ContextMenuStrip cmnuActiveScripts;
        private System.Windows.Forms.ToolStripMenuItem cmbtnCancelScript;
        //private System.Windows.Forms.RichTextBox txtPShellOutput;
        private poshsecframework.Controls.RichTextBoxCaret txtPShellOutput;
        private System.Windows.Forms.ToolStripButton btnRefreshScripts;
        private System.Windows.Forms.ToolStripButton btnViewScript;
        private System.Windows.Forms.ToolStripButton btnRunScript;
        private System.Windows.Forms.ContextMenuStrip cmnuScripts;
        private System.Windows.Forms.ToolStripMenuItem cmbtnRunScript;
        private System.Windows.Forms.ToolStripMenuItem cmbtnViewScript;
        private System.Windows.Forms.ToolStripMenuItem mnuTools;
        private System.Windows.Forms.ToolStripMenuItem mnuOptions;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuCheckforUpdates;
        private System.Windows.Forms.ToolStripMenuItem mnuPSFWiki;
        private System.Windows.Forms.ToolStripButton btnRefreshNetworks;
        private System.Windows.Forms.ColumnHeader chTimeStamp;
        private System.Windows.Forms.ToolStripButton btnCancelScan;
        private System.Windows.Forms.ToolStripMenuItem mnuScriptGetHelp;
        private System.Windows.Forms.ContextMenuStrip cmnuCommands;
        private System.Windows.Forms.ToolStripMenuItem mnuCmdGetHelp;
        private System.Windows.Forms.ListView lvwSchedule;
        private System.Windows.Forms.ColumnHeader chSchScriptName;
        private System.Windows.Forms.ColumnHeader chSchParams;
        private System.Windows.Forms.ColumnHeader chSchSchedule;
        private System.Windows.Forms.ColumnHeader chSchRunAs;
        private System.Windows.Forms.ToolStripMenuItem cmbtnSchedScript;
        private System.Windows.Forms.ToolStripSeparator mnuScrHyphen1;
    }
}

