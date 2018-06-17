using PoshSec.Framework.Controls;

namespace PoshSec.Framework
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
            this.btnOptions = new System.Windows.Forms.ToolStripButton();
            this.btnLaunchCmd = new System.Windows.Forms.ToolStripButton();
            this.btnLaunchPShellCmd = new System.Windows.Forms.ToolStripButton();
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
            this.tsScripts = new System.Windows.Forms.ToolStrip();
            this.btnRefreshScripts = new System.Windows.Forms.ToolStripButton();
            this.btnRunScript = new System.Windows.Forms.ToolStripButton();
            this.btnViewScript = new System.Windows.Forms.ToolStripButton();
            this.btnSchedScript = new System.Windows.Forms.ToolStripButton();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lvwCommands = new System.Windows.Forms.ListView();
            this.chLibName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLibModule = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmnuCommands = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCmdGetHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.imgListLibrary = new System.Windows.Forms.ImageList(this.components);
            this.tsModules = new System.Windows.Forms.ToolStrip();
            this.moduleFilterComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.btnRefreshLibrary = new System.Windows.Forms.ToolStripButton();
            this.btnShowAliases = new System.Windows.Forms.ToolStripButton();
            this.btnShowFunctions = new System.Windows.Forms.ToolStripButton();
            this.btnShowCmdlets = new System.Windows.Forms.ToolStripButton();
            this.tvwNetworks = new System.Windows.Forms.TreeView();
            this.tsNetworks = new System.Windows.Forms.ToolStrip();
            this.btnRefreshNetworks = new System.Windows.Forms.ToolStripButton();
            this.btnAddNetwork = new System.Windows.Forms.ToolStripButton();
            this.btnRemoveNetwork = new System.Windows.Forms.ToolStripButton();
            this.btnScan = new System.Windows.Forms.ToolStripButton();
            this.btnCancelScan = new System.Windows.Forms.ToolStripButton();
            this.pnlSystems = new System.Windows.Forms.SplitContainer();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tbpSystems = new System.Windows.Forms.TabPage();
            this.lvwSystems = new System.Windows.Forms.ListView();
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMAC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chClientInstalled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAlerts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLastScan = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tsSystems = new System.Windows.Forms.ToolStrip();
            this.btnAddSystem = new System.Windows.Forms.ToolStripButton();
            this.btnRemoveSystem = new System.Windows.Forms.ToolStripButton();
            this.tsSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportSystems = new System.Windows.Forms.ToolStripButton();
            this.tslSystemCount = new System.Windows.Forms.ToolStripLabel();
            this.tbpPowerShell = new System.Windows.Forms.TabPage();
            this.txtPShellOutput = new PoshSec.Framework.Controls.RichTextBoxCaret();
            this.cmnuPSFConsole = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmbtnCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbtnPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.tbpSchedScripts = new System.Windows.Forms.TabPage();
            this.lvwSchedule = new System.Windows.Forms.ListView();
            this.chSchScriptName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSchParams = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSchSchedule = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSchRunAs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSchLastRun = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSchMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmnuScheduleCommands = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuScheduleItemRunNow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDeleteScheduleItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tcSystem = new System.Windows.Forms.TabControl();
            this.tbpAlerts = new System.Windows.Forms.TabPage();
            this.lvwAlerts = new System.Windows.Forms.ListView();
            this.chSeverity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTimeStamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chScript = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmnuAlerts = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmbtnCopyMessage = new System.Windows.Forms.ToolStripMenuItem();
            this.cmbtnCopyAlert = new System.Windows.Forms.ToolStripMenuItem();
            this.imgListAlerts = new System.Windows.Forms.ImageList(this.components);
            this.tsAlerts = new System.Windows.Forms.ToolStrip();
            this.btnClearAlerts = new System.Windows.Forms.ToolStripButton();
            this.btnAlert_MarkResolved = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tslblDisplay = new System.Windows.Forms.ToolStripLabel();
            this.btnAlert_Information = new System.Windows.Forms.ToolStripButton();
            this.btnAlert_Error = new System.Windows.Forms.ToolStripButton();
            this.btnAlert_Warning = new System.Windows.Forms.ToolStripButton();
            this.btnAlert_Severe = new System.Windows.Forms.ToolStripButton();
            this.btnAlert_Critical = new System.Windows.Forms.ToolStripButton();
            this.tbpScripts = new System.Windows.Forms.TabPage();
            this.lvwActiveScripts = new System.Windows.Forms.ListView();
            this.chActScrScriptName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chActScrStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chActScrProgress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmnuActiveScripts = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmbtnCancelScript = new System.Windows.Forms.ToolStripMenuItem();
            this.tsActiveScripts = new System.Windows.Forms.ToolStrip();
            this.cmnuHosts = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.powerShellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.waucheckps1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nimain = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmnuNotify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnuRestore = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMain.SuspendLayout();
            this.stsMain.SuspendLayout();
            this.tbMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.Panel1.SuspendLayout();
            this.pnlMain.Panel2.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.cmnuScripts.SuspendLayout();
            this.tsScripts.SuspendLayout();
            this.panel2.SuspendLayout();
            this.cmnuCommands.SuspendLayout();
            this.tsModules.SuspendLayout();
            this.tsNetworks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSystems)).BeginInit();
            this.pnlSystems.Panel1.SuspendLayout();
            this.pnlSystems.Panel2.SuspendLayout();
            this.pnlSystems.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tbpSystems.SuspendLayout();
            this.tsSystems.SuspendLayout();
            this.tbpPowerShell.SuspendLayout();
            this.cmnuPSFConsole.SuspendLayout();
            this.tbpSchedScripts.SuspendLayout();
            this.cmnuScheduleCommands.SuspendLayout();
            this.tcSystem.SuspendLayout();
            this.tbpAlerts.SuspendLayout();
            this.cmnuAlerts.SuspendLayout();
            this.tsAlerts.SuspendLayout();
            this.tbpScripts.SuspendLayout();
            this.cmnuActiveScripts.SuspendLayout();
            this.cmnuHosts.SuspendLayout();
            this.cmnuNotify.SuspendLayout();
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
            this.mnuTools.Size = new System.Drawing.Size(47, 20);
            this.mnuTools.Text = "&Tools";
            // 
            // mnuOptions
            // 
            this.mnuOptions.Image = global::PoshSec.Framework.Properties.Resources.systemsettings;
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
            this.btnOptions,
            this.btnLaunchCmd,
            this.btnLaunchPShellCmd});
            this.tbMain.Location = new System.Drawing.Point(0, 24);
            this.tbMain.Name = "tbMain";
            this.tbMain.Size = new System.Drawing.Size(1205, 25);
            this.tbMain.TabIndex = 2;
            // 
            // btnOptions
            // 
            this.btnOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOptions.Image = global::PoshSec.Framework.Properties.Resources.systemsettings;
            this.btnOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(23, 22);
            this.btnOptions.Text = "Options";
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // btnLaunchCmd
            // 
            this.btnLaunchCmd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLaunchCmd.Image = global::PoshSec.Framework.Properties.Resources.applicationxshellscript;
            this.btnLaunchCmd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLaunchCmd.Name = "btnLaunchCmd";
            this.btnLaunchCmd.Size = new System.Drawing.Size(23, 22);
            this.btnLaunchCmd.Text = "Launch Command Shell";
            this.btnLaunchCmd.Click += new System.EventHandler(this.btnLaunchCmd_Click);
            // 
            // btnLaunchPShellCmd
            // 
            this.btnLaunchPShellCmd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLaunchPShellCmd.Image = global::PoshSec.Framework.Properties.Resources.applicationxpowershellscript;
            this.btnLaunchPShellCmd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLaunchPShellCmd.Name = "btnLaunchPShellCmd";
            this.btnLaunchPShellCmd.Size = new System.Drawing.Size(23, 22);
            this.btnLaunchPShellCmd.Text = "Launch PowerShell Console";
            this.btnLaunchPShellCmd.Click += new System.EventHandler(this.btnLaunchPShellCmd_Click);
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
            this.pnlMain.Panel1.Controls.Add(this.tsNetworks);
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
            this.panel1.Controls.Add(this.tsScripts);
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
            this.imgList16.Images.SetKeyName(8, "psscript.png");
            // 
            // tsScripts
            // 
            this.tsScripts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefreshScripts,
            this.btnRunScript,
            this.btnViewScript,
            this.btnSchedScript});
            this.tsScripts.Location = new System.Drawing.Point(0, 0);
            this.tsScripts.Name = "tsScripts";
            this.tsScripts.Size = new System.Drawing.Size(245, 25);
            this.tsScripts.TabIndex = 0;
            this.tsScripts.Text = "toolStrip4";
            // 
            // btnRefreshScripts
            // 
            this.btnRefreshScripts.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefreshScripts.Image = global::PoshSec.Framework.Properties.Resources.viewrefresh7;
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
            this.btnRunScript.Image = global::PoshSec.Framework.Properties.Resources.run;
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
            this.btnViewScript.Image = global::PoshSec.Framework.Properties.Resources.documentopen7;
            this.btnViewScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnViewScript.Name = "btnViewScript";
            this.btnViewScript.Size = new System.Drawing.Size(23, 22);
            this.btnViewScript.ToolTipText = "View Script";
            this.btnViewScript.Click += new System.EventHandler(this.btnViewScript_Click);
            // 
            // btnSchedScript
            // 
            this.btnSchedScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSchedScript.Enabled = false;
            this.btnSchedScript.Image = global::PoshSec.Framework.Properties.Resources.viewcalendartasks;
            this.btnSchedScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSchedScript.Name = "btnSchedScript";
            this.btnSchedScript.Size = new System.Drawing.Size(23, 22);
            this.btnSchedScript.Text = "toolStripButton1";
            this.btnSchedScript.ToolTipText = "Schedule Script";
            this.btnSchedScript.Click += new System.EventHandler(this.btnSchedScript_Click);
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
            this.panel2.Controls.Add(this.tsModules);
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
            // tsModules
            // 
            this.tsModules.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moduleFilterComboBox,
            this.btnRefreshLibrary,
            this.btnShowAliases,
            this.btnShowFunctions,
            this.btnShowCmdlets});
            this.tsModules.Location = new System.Drawing.Point(0, 0);
            this.tsModules.Name = "tsModules";
            this.tsModules.Size = new System.Drawing.Size(245, 25);
            this.tsModules.TabIndex = 1;
            this.tsModules.Text = "toolStrip2";
            // 
            // moduleFilterComboBox
            // 
            this.moduleFilterComboBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.moduleFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.moduleFilterComboBox.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.moduleFilterComboBox.Items.AddRange(new object[] {
            "All"});
            this.moduleFilterComboBox.Name = "moduleFilterComboBox";
            this.moduleFilterComboBox.Size = new System.Drawing.Size(121, 25);
            this.moduleFilterComboBox.SelectedIndexChanged += new System.EventHandler(this.cmbLibraryTypes_SelectedIndexChanged);
            // 
            // btnRefreshLibrary
            // 
            this.btnRefreshLibrary.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefreshLibrary.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.btnRefreshLibrary.Image = global::PoshSec.Framework.Properties.Resources.viewrefresh7;
            this.btnRefreshLibrary.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefreshLibrary.Name = "btnRefreshLibrary";
            this.btnRefreshLibrary.Size = new System.Drawing.Size(23, 22);
            this.btnRefreshLibrary.Text = "Refresh";
            this.btnRefreshLibrary.ToolTipText = "Refresh";
            this.btnRefreshLibrary.Click += new System.EventHandler(this.btnLibraryRefresh_Click);
            // 
            // btnShowAliases
            // 
            this.btnShowAliases.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowAliases.Image = global::PoshSec.Framework.Properties.Resources.tagred;
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
            this.btnShowFunctions.Image = global::PoshSec.Framework.Properties.Resources.tagblue;
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
            this.btnShowCmdlets.Image = global::PoshSec.Framework.Properties.Resources.taggreen;
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
            this.tvwNetworks.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwNetworks_AfterSelect);
            // 
            // tsNetworks
            // 
            this.tsNetworks.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefreshNetworks,
            this.btnAddNetwork,
            this.btnRemoveNetwork,
            this.btnScan,
            this.btnCancelScan});
            this.tsNetworks.Location = new System.Drawing.Point(0, 0);
            this.tsNetworks.Name = "tsNetworks";
            this.tsNetworks.Size = new System.Drawing.Size(245, 25);
            this.tsNetworks.TabIndex = 4;
            this.tsNetworks.Text = "toolStrip1";
            // 
            // btnRefreshNetworks
            // 
            this.btnRefreshNetworks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefreshNetworks.Image = global::PoshSec.Framework.Properties.Resources.viewrefresh7;
            this.btnRefreshNetworks.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefreshNetworks.Name = "btnRefreshNetworks";
            this.btnRefreshNetworks.Size = new System.Drawing.Size(23, 22);
            this.btnRefreshNetworks.ToolTipText = "Refresh Networks";
            this.btnRefreshNetworks.Click += new System.EventHandler(this.btnRefreshNetworks_Click);
            // 
            // btnAddNetwork
            // 
            this.btnAddNetwork.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddNetwork.Image = global::PoshSec.Framework.Properties.Resources.Diagram;
            this.btnAddNetwork.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddNetwork.Name = "btnAddNetwork";
            this.btnAddNetwork.Size = new System.Drawing.Size(23, 22);
            this.btnAddNetwork.ToolTipText = "Add Network";
            this.btnAddNetwork.Click += new System.EventHandler(this.btnAddNetwork_Click);
            // 
            // btnRemoveNetwork
            // 
            this.btnRemoveNetwork.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRemoveNetwork.Enabled = false;
            this.btnRemoveNetwork.Image = global::PoshSec.Framework.Properties.Resources.editdelete6;
            this.btnRemoveNetwork.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemoveNetwork.Name = "btnRemoveNetwork";
            this.btnRemoveNetwork.Size = new System.Drawing.Size(23, 22);
            this.btnRemoveNetwork.Text = "Remove Network";
            this.btnRemoveNetwork.Click += new System.EventHandler(this.btnRemoveNetwork_Click);
            // 
            // btnScan
            // 
            this.btnScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnScan.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.btnScan.Image = global::PoshSec.Framework.Properties.Resources.networktransmitreceive2;
            this.btnScan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(23, 22);
            this.btnScan.Text = "Scan";
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // btnCancelScan
            // 
            this.btnCancelScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelScan.Enabled = false;
            this.btnCancelScan.Image = global::PoshSec.Framework.Properties.Resources.dialogcancel;
            this.btnCancelScan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelScan.Name = "btnCancelScan";
            this.btnCancelScan.Size = new System.Drawing.Size(23, 22);
            this.btnCancelScan.ToolTipText = "Cancel Scan";
            this.btnCancelScan.Click += new System.EventHandler(this.btnCancelScan_Click);
            // 
            // pnlSystems
            // 
            this.pnlSystems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSystems.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
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
            this.tbpSystems.Controls.Add(this.tsSystems);
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
            this.chDescription,
            this.chStatus,
            this.chClientInstalled,
            this.chAlerts,
            this.chLastScan});
            this.lvwSystems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwSystems.FullRowSelect = true;
            this.lvwSystems.HideSelection = false;
            this.lvwSystems.Location = new System.Drawing.Point(3, 28);
            this.lvwSystems.Name = "lvwSystems";
            this.lvwSystems.Size = new System.Drawing.Size(942, 282);
            this.lvwSystems.SmallImageList = this.imgList16;
            this.lvwSystems.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvwSystems.TabIndex = 1;
            this.lvwSystems.UseCompatibleStateImageBehavior = false;
            this.lvwSystems.View = System.Windows.Forms.View.Details;
            this.lvwSystems.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwSystems_ColumnClick);
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 140;
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
            // chDescription
            // 
            this.chDescription.Text = "Description";
            this.chDescription.Width = 125;
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
            // tsSystems
            // 
            this.tsSystems.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddSystem,
            this.btnRemoveSystem,
            this.tsSeparator,
            this.btnExportSystems,
            this.tslSystemCount});
            this.tsSystems.Location = new System.Drawing.Point(3, 3);
            this.tsSystems.Name = "tsSystems";
            this.tsSystems.Size = new System.Drawing.Size(942, 25);
            this.tsSystems.TabIndex = 2;
            this.tsSystems.Text = "toolStrip6";
            // 
            // btnAddSystem
            // 
            this.btnAddSystem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddSystem.Image = global::PoshSec.Framework.Properties.Resources.computeradd;
            this.btnAddSystem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddSystem.Name = "btnAddSystem";
            this.btnAddSystem.Size = new System.Drawing.Size(23, 22);
            this.btnAddSystem.Text = "Add System";
            // 
            // btnRemoveSystem
            // 
            this.btnRemoveSystem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRemoveSystem.Image = global::PoshSec.Framework.Properties.Resources.computerdelete;
            this.btnRemoveSystem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemoveSystem.Name = "btnRemoveSystem";
            this.btnRemoveSystem.Size = new System.Drawing.Size(23, 22);
            this.btnRemoveSystem.Text = "Remove System";
            this.btnRemoveSystem.Click += new System.EventHandler(this.btnRemoveSystem_Click);
            // 
            // tsSeparator
            // 
            this.tsSeparator.Name = "tsSeparator";
            this.tsSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // btnExportSystems
            // 
            this.btnExportSystems.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportSystems.Image = global::PoshSec.Framework.Properties.Resources.documentexport4;
            this.btnExportSystems.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportSystems.Name = "btnExportSystems";
            this.btnExportSystems.Size = new System.Drawing.Size(23, 22);
            this.btnExportSystems.Text = "Export Systems";
            this.btnExportSystems.Click += new System.EventHandler(this.btnExportSystems_Click);
            // 
            // tslSystemCount
            // 
            this.tslSystemCount.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tslSystemCount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tslSystemCount.Name = "tslSystemCount";
            this.tslSystemCount.Size = new System.Drawing.Size(63, 22);
            this.tslSystemCount.Text = "0 Systems";
            // 
            // tbpPowerShell
            // 
            this.tbpPowerShell.BackColor = System.Drawing.Color.SteelBlue;
            this.tbpPowerShell.Controls.Add(this.txtPShellOutput);
            this.tbpPowerShell.ImageIndex = 8;
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
            this.txtPShellOutput.ContextMenuStrip = this.cmnuPSFConsole;
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
            this.txtPShellOutput.Text = "";
            this.txtPShellOutput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPShellOutput_KeyDown);
            // 
            // cmnuPSFConsole
            // 
            this.cmnuPSFConsole.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmbtnCopy,
            this.cmbtnPaste});
            this.cmnuPSFConsole.Name = "cmnuPSFConsole";
            this.cmnuPSFConsole.Size = new System.Drawing.Size(103, 48);
            this.cmnuPSFConsole.Opening += new System.ComponentModel.CancelEventHandler(this.cmnuPSFConsole_Opening);
            // 
            // cmbtnCopy
            // 
            this.cmbtnCopy.Enabled = false;
            this.cmbtnCopy.Name = "cmbtnCopy";
            this.cmbtnCopy.Size = new System.Drawing.Size(102, 22);
            this.cmbtnCopy.Text = "Copy";
            this.cmbtnCopy.Click += new System.EventHandler(this.cmbtnCopy_Click);
            // 
            // cmbtnPaste
            // 
            this.cmbtnPaste.Enabled = false;
            this.cmbtnPaste.Name = "cmbtnPaste";
            this.cmbtnPaste.Size = new System.Drawing.Size(102, 22);
            this.cmbtnPaste.Text = "Paste";
            this.cmbtnPaste.Click += new System.EventHandler(this.cmbtnPaste_Click);
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
            this.chSchRunAs,
            this.chSchLastRun,
            this.chSchMessage});
            this.lvwSchedule.ContextMenuStrip = this.cmnuScheduleCommands;
            this.lvwSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwSchedule.FullRowSelect = true;
            this.lvwSchedule.Location = new System.Drawing.Point(3, 3);
            this.lvwSchedule.Name = "lvwSchedule";
            this.lvwSchedule.Size = new System.Drawing.Size(942, 307);
            this.lvwSchedule.SmallImageList = this.imgList16;
            this.lvwSchedule.TabIndex = 0;
            this.lvwSchedule.UseCompatibleStateImageBehavior = false;
            this.lvwSchedule.View = System.Windows.Forms.View.Details;
            this.lvwSchedule.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvwSchedule_KeyDown);
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
            this.chSchRunAs.Width = 100;
            // 
            // chSchLastRun
            // 
            this.chSchLastRun.Text = "Last Run Timestamp";
            this.chSchLastRun.Width = 130;
            // 
            // chSchMessage
            // 
            this.chSchMessage.Text = "Messages";
            this.chSchMessage.Width = 175;
            // 
            // cmnuScheduleCommands
            // 
            this.cmnuScheduleCommands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuScheduleItemRunNow,
            this.mnuDeleteScheduleItem});
            this.cmnuScheduleCommands.Name = "cmnuScheduleCommands";
            this.cmnuScheduleCommands.Size = new System.Drawing.Size(124, 48);
            this.cmnuScheduleCommands.Opening += new System.ComponentModel.CancelEventHandler(this.cmnuScheduleCommands_Opening);
            // 
            // mnuScheduleItemRunNow
            // 
            this.mnuScheduleItemRunNow.Name = "mnuScheduleItemRunNow";
            this.mnuScheduleItemRunNow.Size = new System.Drawing.Size(123, 22);
            this.mnuScheduleItemRunNow.Text = "Run Now";
            this.mnuScheduleItemRunNow.Click += new System.EventHandler(this.mnuScheduleItemRunNow_Click);
            // 
            // mnuDeleteScheduleItem
            // 
            this.mnuDeleteScheduleItem.Name = "mnuDeleteScheduleItem";
            this.mnuDeleteScheduleItem.Size = new System.Drawing.Size(123, 22);
            this.mnuDeleteScheduleItem.Text = "Delete";
            this.mnuDeleteScheduleItem.Click += new System.EventHandler(this.mnuDeleteScheduleItem_Click);
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
            this.tbpAlerts.Controls.Add(this.tsAlerts);
            this.tbpAlerts.Location = new System.Drawing.Point(4, 4);
            this.tbpAlerts.Name = "tbpAlerts";
            this.tbpAlerts.Padding = new System.Windows.Forms.Padding(3);
            this.tbpAlerts.Size = new System.Drawing.Size(948, 204);
            this.tbpAlerts.TabIndex = 1;
            this.tbpAlerts.Text = "Alerts (0)";
            this.tbpAlerts.UseVisualStyleBackColor = true;
            this.tbpAlerts.TextChanged += new System.EventHandler(this.tbpAlerts_TextChanged);
            // 
            // lvwAlerts
            // 
            this.lvwAlerts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chSeverity,
            this.chMessage,
            this.chTimeStamp,
            this.chScript});
            this.lvwAlerts.ContextMenuStrip = this.cmnuAlerts;
            this.lvwAlerts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwAlerts.FullRowSelect = true;
            this.lvwAlerts.HideSelection = false;
            this.lvwAlerts.Location = new System.Drawing.Point(3, 28);
            this.lvwAlerts.Name = "lvwAlerts";
            this.lvwAlerts.ShowItemToolTips = true;
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
            // cmnuAlerts
            // 
            this.cmnuAlerts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmbtnCopyMessage,
            this.cmbtnCopyAlert});
            this.cmnuAlerts.Name = "cmnuAlerts";
            this.cmnuAlerts.Size = new System.Drawing.Size(164, 48);
            this.cmnuAlerts.Opening += new System.ComponentModel.CancelEventHandler(this.cmnuAlerts_Opening);
            // 
            // cmbtnCopyMessage
            // 
            this.cmbtnCopyMessage.Name = "cmbtnCopyMessage";
            this.cmbtnCopyMessage.Size = new System.Drawing.Size(163, 22);
            this.cmbtnCopyMessage.Text = "Copy Message";
            this.cmbtnCopyMessage.Click += new System.EventHandler(this.cmbtnCopyMessage_Click);
            // 
            // cmbtnCopyAlert
            // 
            this.cmbtnCopyAlert.Name = "cmbtnCopyAlert";
            this.cmbtnCopyAlert.Size = new System.Drawing.Size(163, 22);
            this.cmbtnCopyAlert.Text = "Copy Entire Alert";
            this.cmbtnCopyAlert.Click += new System.EventHandler(this.cmbtnCopyAlert_Click);
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
            // tsAlerts
            // 
            this.tsAlerts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClearAlerts,
            this.btnAlert_MarkResolved,
            this.toolStripSeparator1,
            this.tslblDisplay,
            this.btnAlert_Information,
            this.btnAlert_Error,
            this.btnAlert_Warning,
            this.btnAlert_Severe,
            this.btnAlert_Critical});
            this.tsAlerts.Location = new System.Drawing.Point(3, 3);
            this.tsAlerts.Name = "tsAlerts";
            this.tsAlerts.Size = new System.Drawing.Size(942, 25);
            this.tsAlerts.TabIndex = 0;
            // 
            // btnClearAlerts
            // 
            this.btnClearAlerts.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClearAlerts.Image = global::PoshSec.Framework.Properties.Resources.editclearlist;
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
            this.btnAlert_MarkResolved.Image = global::PoshSec.Framework.Properties.Resources.dialogaccept;
            this.btnAlert_MarkResolved.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAlert_MarkResolved.Name = "btnAlert_MarkResolved";
            this.btnAlert_MarkResolved.Size = new System.Drawing.Size(23, 22);
            this.btnAlert_MarkResolved.Text = "toolStripButton3";
            this.btnAlert_MarkResolved.ToolTipText = "Mark Resolved";
            this.btnAlert_MarkResolved.Click += new System.EventHandler(this.btnAlert_MarkResolved_Click);
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
            // btnAlert_Information
            // 
            this.btnAlert_Information.Checked = true;
            this.btnAlert_Information.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnAlert_Information.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAlert_Information.Image = global::PoshSec.Framework.Properties.Resources.dialoginformation4;
            this.btnAlert_Information.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAlert_Information.Name = "btnAlert_Information";
            this.btnAlert_Information.Size = new System.Drawing.Size(23, 22);
            this.btnAlert_Information.ToolTipText = "Show Information Alerts";
            this.btnAlert_Information.CheckedChanged += new System.EventHandler(this.btnAlert_Information_CheckedChanged);
            this.btnAlert_Information.Click += new System.EventHandler(this.btnAlert_Information_Click);
            // 
            // btnAlert_Error
            // 
            this.btnAlert_Error.Checked = true;
            this.btnAlert_Error.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnAlert_Error.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAlert_Error.Image = global::PoshSec.Framework.Properties.Resources.dialogerror4;
            this.btnAlert_Error.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAlert_Error.Name = "btnAlert_Error";
            this.btnAlert_Error.Size = new System.Drawing.Size(23, 22);
            this.btnAlert_Error.Text = "toolStripButton6";
            this.btnAlert_Error.ToolTipText = "Show Error Alerts";
            this.btnAlert_Error.CheckedChanged += new System.EventHandler(this.btnAlert_Error_CheckedChanged);
            this.btnAlert_Error.Click += new System.EventHandler(this.btnAlert_Error_Click);
            // 
            // btnAlert_Warning
            // 
            this.btnAlert_Warning.Checked = true;
            this.btnAlert_Warning.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnAlert_Warning.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAlert_Warning.Image = global::PoshSec.Framework.Properties.Resources.dialogwarning3;
            this.btnAlert_Warning.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAlert_Warning.Name = "btnAlert_Warning";
            this.btnAlert_Warning.Size = new System.Drawing.Size(23, 22);
            this.btnAlert_Warning.Text = "toolStripButton7";
            this.btnAlert_Warning.ToolTipText = "Show Warning Alerts";
            this.btnAlert_Warning.CheckedChanged += new System.EventHandler(this.btnAlert_Warning_CheckedChanged);
            this.btnAlert_Warning.Click += new System.EventHandler(this.btnAlert_Warning_Click);
            // 
            // btnAlert_Severe
            // 
            this.btnAlert_Severe.Checked = true;
            this.btnAlert_Severe.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnAlert_Severe.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAlert_Severe.Image = global::PoshSec.Framework.Properties.Resources.dialogwarning2;
            this.btnAlert_Severe.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAlert_Severe.Name = "btnAlert_Severe";
            this.btnAlert_Severe.Size = new System.Drawing.Size(23, 22);
            this.btnAlert_Severe.Text = "toolStripButton8";
            this.btnAlert_Severe.ToolTipText = "Show Severe Alerts";
            this.btnAlert_Severe.CheckedChanged += new System.EventHandler(this.btnAlert_Severe_CheckedChanged);
            this.btnAlert_Severe.Click += new System.EventHandler(this.btnAlert_Severe_Click);
            // 
            // btnAlert_Critical
            // 
            this.btnAlert_Critical.Checked = true;
            this.btnAlert_Critical.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnAlert_Critical.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAlert_Critical.Image = global::PoshSec.Framework.Properties.Resources.exclamation;
            this.btnAlert_Critical.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAlert_Critical.Name = "btnAlert_Critical";
            this.btnAlert_Critical.Size = new System.Drawing.Size(23, 22);
            this.btnAlert_Critical.ToolTipText = "Show Critical Alerts";
            this.btnAlert_Critical.CheckedChanged += new System.EventHandler(this.btnAlert_Critical_CheckedChanged);
            this.btnAlert_Critical.Click += new System.EventHandler(this.btnAlert_Critical_Click);
            // 
            // tbpScripts
            // 
            this.tbpScripts.BackColor = System.Drawing.Color.Transparent;
            this.tbpScripts.Controls.Add(this.lvwActiveScripts);
            this.tbpScripts.Controls.Add(this.tsActiveScripts);
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
            this.chActScrStatus,
            this.chActScrProgress});
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
            this.chActScrStatus.Width = 250;
            // 
            // chActScrProgress
            // 
            this.chActScrProgress.Text = "Progress";
            this.chActScrProgress.Width = 250;
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
            // tsActiveScripts
            // 
            this.tsActiveScripts.Location = new System.Drawing.Point(3, 3);
            this.tsActiveScripts.Name = "tsActiveScripts";
            this.tsActiveScripts.Size = new System.Drawing.Size(942, 25);
            this.tsActiveScripts.TabIndex = 0;
            this.tsActiveScripts.Text = "toolStrip5";
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
            // nimain
            // 
            this.nimain.ContextMenuStrip = this.cmnuNotify;
            this.nimain.Icon = ((System.Drawing.Icon)(resources.GetObject("nimain.Icon")));
            this.nimain.Text = "PoshSec Framework";
            this.nimain.DoubleClick += new System.EventHandler(this.nimain_DoubleClick);
            // 
            // cmnuNotify
            // 
            this.cmnuNotify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuRestore,
            this.cmnuExit});
            this.cmnuNotify.Name = "cmnuNotify";
            this.cmnuNotify.Size = new System.Drawing.Size(114, 48);
            // 
            // cmnuRestore
            // 
            this.cmnuRestore.Name = "cmnuRestore";
            this.cmnuRestore.Size = new System.Drawing.Size(113, 22);
            this.cmnuRestore.Text = "Restore";
            this.cmnuRestore.Click += new System.EventHandler(this.cmnuRestore_Click);
            // 
            // cmnuExit
            // 
            this.cmnuExit.Name = "cmnuExit";
            this.cmnuExit.Size = new System.Drawing.Size(113, 22);
            this.cmnuExit.Text = "Exit";
            this.cmnuExit.Click += new System.EventHandler(this.cmnuExit_Click);
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
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
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
            this.tsScripts.ResumeLayout(false);
            this.tsScripts.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.cmnuCommands.ResumeLayout(false);
            this.tsModules.ResumeLayout(false);
            this.tsModules.PerformLayout();
            this.tsNetworks.ResumeLayout(false);
            this.tsNetworks.PerformLayout();
            this.pnlSystems.Panel1.ResumeLayout(false);
            this.pnlSystems.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlSystems)).EndInit();
            this.pnlSystems.ResumeLayout(false);
            this.tcMain.ResumeLayout(false);
            this.tbpSystems.ResumeLayout(false);
            this.tbpSystems.PerformLayout();
            this.tsSystems.ResumeLayout(false);
            this.tsSystems.PerformLayout();
            this.tbpPowerShell.ResumeLayout(false);
            this.cmnuPSFConsole.ResumeLayout(false);
            this.tbpSchedScripts.ResumeLayout(false);
            this.cmnuScheduleCommands.ResumeLayout(false);
            this.tcSystem.ResumeLayout(false);
            this.tbpAlerts.ResumeLayout(false);
            this.tbpAlerts.PerformLayout();
            this.cmnuAlerts.ResumeLayout(false);
            this.tsAlerts.ResumeLayout(false);
            this.tsAlerts.PerformLayout();
            this.tbpScripts.ResumeLayout(false);
            this.tbpScripts.PerformLayout();
            this.cmnuActiveScripts.ResumeLayout(false);
            this.cmnuHosts.ResumeLayout(false);
            this.cmnuNotify.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripStatusLabel lblsbSpacer;
        private System.Windows.Forms.ToolStripProgressBar pbStatus;
        private System.Windows.Forms.TabPage tbpScripts;
        private System.Windows.Forms.ContextMenuStrip cmnuHosts;
        private System.Windows.Forms.ToolStripMenuItem powerShellToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowsUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem waucheckps1ToolStripMenuItem;
        private System.Windows.Forms.TreeView tvwNetworks;
        private System.Windows.Forms.ToolStrip tsNetworks;
        private System.Windows.Forms.ListView lvwAlerts;
        private System.Windows.Forms.ColumnHeader chSeverity;
        private System.Windows.Forms.ColumnHeader chMessage;
        private System.Windows.Forms.ColumnHeader chScript;
        private System.Windows.Forms.ToolStrip tsAlerts;
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
        private System.Windows.Forms.ToolStrip tsModules;
        private System.Windows.Forms.ToolStripComboBox moduleFilterComboBox;
        private System.Windows.Forms.ToolStripButton btnRefreshLibrary;
        private System.Windows.Forms.ToolStripButton btnShowAliases;
        private System.Windows.Forms.ToolStripButton btnShowFunctions;
        private System.Windows.Forms.ToolStripButton btnShowCmdlets;
        private System.Windows.Forms.ToolStripButton btnAlert_MarkResolved;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnAlert_Information;
        private System.Windows.Forms.ToolStripButton btnAlert_Error;
        private System.Windows.Forms.ToolStripButton btnAlert_Warning;
        private System.Windows.Forms.ToolStripButton btnAlert_Severe;
        private System.Windows.Forms.ToolStripButton btnAlert_Critical;
        private System.Windows.Forms.ToolStripLabel tslblDisplay;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView lvwScripts;
        private System.Windows.Forms.ColumnHeader chScriptName;
        private System.Windows.Forms.ToolStrip tsScripts;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TabPage tbpSchedScripts;
        private System.Windows.Forms.ListView lvwActiveScripts;
        private System.Windows.Forms.ToolStrip tsActiveScripts;
        private System.Windows.Forms.ColumnHeader chActScrScriptName;
        private System.Windows.Forms.ColumnHeader chActScrStatus;
        private System.Windows.Forms.ContextMenuStrip cmnuActiveScripts;
        private System.Windows.Forms.ToolStripMenuItem cmbtnCancelScript;
        //private System.Windows.Forms.RichTextBox txtPShellOutput;
        private RichTextBoxCaret txtPShellOutput;
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
        private System.Windows.Forms.ToolStripButton btnSchedScript;
        private System.Windows.Forms.ColumnHeader chSchLastRun;
        private System.Windows.Forms.ColumnHeader chSchMessage;
        private System.Windows.Forms.ContextMenuStrip cmnuScheduleCommands;
        private System.Windows.Forms.ToolStripMenuItem mnuDeleteScheduleItem;
        private System.Windows.Forms.ToolStripButton btnScan;
        private System.Windows.Forms.ColumnHeader chDescription;
        private System.Windows.Forms.ToolStrip tsSystems;
        private System.Windows.Forms.ToolStripButton btnAddNetwork;
        private System.Windows.Forms.ToolStripButton btnExportSystems;
        private System.Windows.Forms.ToolStripButton btnOptions;
        private System.Windows.Forms.ToolStripButton btnLaunchCmd;
        private System.Windows.Forms.ToolStripButton btnLaunchPShellCmd;
        private System.Windows.Forms.ToolStripLabel tslSystemCount;
        private System.Windows.Forms.ContextMenuStrip cmnuPSFConsole;
        private System.Windows.Forms.ToolStripMenuItem cmbtnCopy;
        private System.Windows.Forms.ToolStripMenuItem cmbtnPaste;
        private System.Windows.Forms.ContextMenuStrip cmnuAlerts;
        private System.Windows.Forms.ToolStripMenuItem cmbtnCopyMessage;
        private System.Windows.Forms.ToolStripMenuItem cmbtnCopyAlert;
        private System.Windows.Forms.ColumnHeader chActScrProgress;
        private System.Windows.Forms.ToolStripButton btnRemoveNetwork;
        private System.Windows.Forms.ToolStripButton btnAddSystem;
        private System.Windows.Forms.ToolStripButton btnRemoveSystem;
        private System.Windows.Forms.ToolStripSeparator tsSeparator;
        private System.Windows.Forms.ToolStripMenuItem mnuScheduleItemRunNow;
        private System.Windows.Forms.NotifyIcon nimain;
        private System.Windows.Forms.ContextMenuStrip cmnuNotify;
        private System.Windows.Forms.ToolStripMenuItem cmnuRestore;
        private System.Windows.Forms.ToolStripMenuItem cmnuExit;
    }
}

