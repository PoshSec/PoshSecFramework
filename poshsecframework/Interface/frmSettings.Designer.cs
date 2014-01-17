namespace poshsecframework.Interface
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbpGeneral = new System.Windows.Forms.TabPage();
            this.panel6 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbFirstTime = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbScriptSetting = new System.Windows.Forms.GroupBox();
            this.cmbScriptDefAction = new System.Windows.Forms.ComboBox();
            this.lblScriptDefAction = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.txtPSExecPath = new System.Windows.Forms.TextBox();
            this.btnBrowsePSExec = new System.Windows.Forms.Button();
            this.lblPSExecPath = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.txtSchFile = new System.Windows.Forms.TextBox();
            this.btnBrowseSchFile = new System.Windows.Forms.Button();
            this.lblSchFile = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtFrameworkFile = new System.Windows.Forms.TextBox();
            this.btnBrowseFramework = new System.Windows.Forms.Button();
            this.lblFrameworkDirectory = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtModuleDirectory = new System.Windows.Forms.TextBox();
            this.btnBrowseModule = new System.Windows.Forms.Button();
            this.lblModuleDirectory = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtScriptDirectory = new System.Windows.Forms.TextBox();
            this.btnBrowseScript = new System.Windows.Forms.Button();
            this.lblScriptDirectory = new System.Windows.Forms.Label();
            this.tcSettings = new System.Windows.Forms.TabControl();
            this.tbpModules = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.chModName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chRepository = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chBranch = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbModules = new System.Windows.Forms.ToolStrip();
            this.btnAddModule = new System.Windows.Forms.ToolStripButton();
            this.pbStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.lblStatus = new System.Windows.Forms.ToolStripLabel();
            this.panel1.SuspendLayout();
            this.tbpGeneral.SuspendLayout();
            this.panel6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbScriptSetting.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tcSettings.SuspendLayout();
            this.tbpModules.SuspendLayout();
            this.tbModules.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 249);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(460, 34);
            this.panel1.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(297, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(378, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tbpGeneral
            // 
            this.tbpGeneral.Controls.Add(this.panel6);
            this.tbpGeneral.Controls.Add(this.panel5);
            this.tbpGeneral.Controls.Add(this.panel7);
            this.tbpGeneral.Controls.Add(this.panel8);
            this.tbpGeneral.Controls.Add(this.panel3);
            this.tbpGeneral.Controls.Add(this.panel4);
            this.tbpGeneral.Controls.Add(this.panel2);
            this.tbpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tbpGeneral.Name = "tbpGeneral";
            this.tbpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tbpGeneral.Size = new System.Drawing.Size(452, 223);
            this.tbpGeneral.TabIndex = 0;
            this.tbpGeneral.Text = "General";
            this.tbpGeneral.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.groupBox1);
            this.panel6.Controls.Add(this.gbScriptSetting);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(3, 152);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(446, 70);
            this.panel6.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbFirstTime);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(200, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(123, 70);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "First Time Utility";
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
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Show First Time Utility:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbScriptSetting
            // 
            this.gbScriptSetting.Controls.Add(this.cmbScriptDefAction);
            this.gbScriptSetting.Controls.Add(this.lblScriptDefAction);
            this.gbScriptSetting.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbScriptSetting.Location = new System.Drawing.Point(0, 0);
            this.gbScriptSetting.Name = "gbScriptSetting";
            this.gbScriptSetting.Size = new System.Drawing.Size(200, 70);
            this.gbScriptSetting.TabIndex = 0;
            this.gbScriptSetting.TabStop = false;
            this.gbScriptSetting.Text = "Scripts";
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
            this.cmbScriptDefAction.Size = new System.Drawing.Size(194, 21);
            this.cmbScriptDefAction.TabIndex = 7;
            // 
            // lblScriptDefAction
            // 
            this.lblScriptDefAction.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblScriptDefAction.Location = new System.Drawing.Point(3, 17);
            this.lblScriptDefAction.Name = "lblScriptDefAction";
            this.lblScriptDefAction.Size = new System.Drawing.Size(194, 23);
            this.lblScriptDefAction.TabIndex = 0;
            this.lblScriptDefAction.Text = "Double Click Default Action:";
            this.lblScriptDefAction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(3, 133);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(446, 19);
            this.panel5.TabIndex = 6;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.txtPSExecPath);
            this.panel7.Controls.Add(this.btnBrowsePSExec);
            this.panel7.Controls.Add(this.lblPSExecPath);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(3, 107);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(446, 26);
            this.panel7.TabIndex = 5;
            // 
            // txtPSExecPath
            // 
            this.txtPSExecPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPSExecPath.Location = new System.Drawing.Point(111, 0);
            this.txtPSExecPath.Name = "txtPSExecPath";
            this.txtPSExecPath.Size = new System.Drawing.Size(309, 21);
            this.txtPSExecPath.TabIndex = 6;
            // 
            // btnBrowsePSExec
            // 
            this.btnBrowsePSExec.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBrowsePSExec.Image = global::poshsecframework.Properties.Resources.documentopenfolder;
            this.btnBrowsePSExec.Location = new System.Drawing.Point(420, 0);
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
            this.panel8.Location = new System.Drawing.Point(3, 81);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(446, 26);
            this.panel8.TabIndex = 8;
            // 
            // txtSchFile
            // 
            this.txtSchFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSchFile.Location = new System.Drawing.Point(111, 0);
            this.txtSchFile.Name = "txtSchFile";
            this.txtSchFile.Size = new System.Drawing.Size(309, 21);
            this.txtSchFile.TabIndex = 5;
            // 
            // btnBrowseSchFile
            // 
            this.btnBrowseSchFile.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBrowseSchFile.Image = global::poshsecframework.Properties.Resources.documentopenfolder;
            this.btnBrowseSchFile.Location = new System.Drawing.Point(420, 0);
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
            // panel3
            // 
            this.panel3.Controls.Add(this.txtFrameworkFile);
            this.panel3.Controls.Add(this.btnBrowseFramework);
            this.panel3.Controls.Add(this.lblFrameworkDirectory);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 55);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(446, 26);
            this.panel3.TabIndex = 1;
            // 
            // txtFrameworkFile
            // 
            this.txtFrameworkFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFrameworkFile.Location = new System.Drawing.Point(111, 0);
            this.txtFrameworkFile.Name = "txtFrameworkFile";
            this.txtFrameworkFile.Size = new System.Drawing.Size(309, 21);
            this.txtFrameworkFile.TabIndex = 4;
            // 
            // btnBrowseFramework
            // 
            this.btnBrowseFramework.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBrowseFramework.Image = global::poshsecframework.Properties.Resources.documentopenfolder;
            this.btnBrowseFramework.Location = new System.Drawing.Point(420, 0);
            this.btnBrowseFramework.Name = "btnBrowseFramework";
            this.btnBrowseFramework.Size = new System.Drawing.Size(26, 26);
            this.btnBrowseFramework.TabIndex = 1;
            this.btnBrowseFramework.UseVisualStyleBackColor = true;
            this.btnBrowseFramework.Click += new System.EventHandler(this.btnBrowseFramework_Click);
            // 
            // lblFrameworkDirectory
            // 
            this.lblFrameworkDirectory.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblFrameworkDirectory.Location = new System.Drawing.Point(0, 0);
            this.lblFrameworkDirectory.Name = "lblFrameworkDirectory";
            this.lblFrameworkDirectory.Size = new System.Drawing.Size(111, 26);
            this.lblFrameworkDirectory.TabIndex = 0;
            this.lblFrameworkDirectory.Text = "Framework File:";
            this.lblFrameworkDirectory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtModuleDirectory);
            this.panel4.Controls.Add(this.btnBrowseModule);
            this.panel4.Controls.Add(this.lblModuleDirectory);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 29);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(446, 26);
            this.panel4.TabIndex = 2;
            // 
            // txtModuleDirectory
            // 
            this.txtModuleDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtModuleDirectory.Location = new System.Drawing.Point(111, 0);
            this.txtModuleDirectory.Name = "txtModuleDirectory";
            this.txtModuleDirectory.Size = new System.Drawing.Size(309, 21);
            this.txtModuleDirectory.TabIndex = 3;
            // 
            // btnBrowseModule
            // 
            this.btnBrowseModule.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBrowseModule.Image = global::poshsecframework.Properties.Resources.documentopenfolder;
            this.btnBrowseModule.Location = new System.Drawing.Point(420, 0);
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
            this.panel2.Size = new System.Drawing.Size(446, 26);
            this.panel2.TabIndex = 0;
            // 
            // txtScriptDirectory
            // 
            this.txtScriptDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtScriptDirectory.Location = new System.Drawing.Point(111, 0);
            this.txtScriptDirectory.Name = "txtScriptDirectory";
            this.txtScriptDirectory.Size = new System.Drawing.Size(309, 21);
            this.txtScriptDirectory.TabIndex = 2;
            // 
            // btnBrowseScript
            // 
            this.btnBrowseScript.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBrowseScript.Image = global::poshsecframework.Properties.Resources.documentopenfolder;
            this.btnBrowseScript.Location = new System.Drawing.Point(420, 0);
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
            this.tcSettings.Controls.Add(this.tbpModules);
            this.tcSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSettings.Location = new System.Drawing.Point(0, 0);
            this.tcSettings.Multiline = true;
            this.tcSettings.Name = "tcSettings";
            this.tcSettings.SelectedIndex = 0;
            this.tcSettings.Size = new System.Drawing.Size(460, 249);
            this.tcSettings.TabIndex = 1;
            // 
            // tbpModules
            // 
            this.tbpModules.Controls.Add(this.listView1);
            this.tbpModules.Controls.Add(this.tbModules);
            this.tbpModules.Location = new System.Drawing.Point(4, 22);
            this.tbpModules.Name = "tbpModules";
            this.tbpModules.Padding = new System.Windows.Forms.Padding(3);
            this.tbpModules.Size = new System.Drawing.Size(452, 223);
            this.tbpModules.TabIndex = 1;
            this.tbpModules.Text = "Modules";
            this.tbpModules.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chModName,
            this.chRepository,
            this.chBranch});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(3, 28);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(446, 192);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // chModName
            // 
            this.chModName.Text = "Module Name";
            this.chModName.Width = 140;
            // 
            // chRepository
            // 
            this.chRepository.Text = "Repository";
            this.chRepository.Width = 160;
            // 
            // chBranch
            // 
            this.chBranch.Text = "Branch";
            this.chBranch.Width = 120;
            // 
            // tbModules
            // 
            this.tbModules.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddModule,
            this.pbStatus,
            this.lblStatus});
            this.tbModules.Location = new System.Drawing.Point(3, 3);
            this.tbModules.Name = "tbModules";
            this.tbModules.Size = new System.Drawing.Size(446, 25);
            this.tbModules.TabIndex = 0;
            // 
            // btnAddModule
            // 
            this.btnAddModule.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddModule.Image = ((System.Drawing.Image)(resources.GetObject("btnAddModule.Image")));
            this.btnAddModule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddModule.Name = "btnAddModule";
            this.btnAddModule.Size = new System.Drawing.Size(23, 22);
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
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(460, 283);
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
            this.groupBox1.ResumeLayout(false);
            this.gbScriptSetting.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tcSettings.ResumeLayout(false);
            this.tbpModules.ResumeLayout(false);
            this.tbpModules.PerformLayout();
            this.tbModules.ResumeLayout(false);
            this.tbModules.PerformLayout();
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
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtFrameworkFile;
        private System.Windows.Forms.Button btnBrowseFramework;
        private System.Windows.Forms.Label lblFrameworkDirectory;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.GroupBox gbScriptSetting;
        private System.Windows.Forms.ComboBox cmbScriptDefAction;
        private System.Windows.Forms.Label lblScriptDefAction;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TextBox txtPSExecPath;
        private System.Windows.Forms.Button btnBrowsePSExec;
        private System.Windows.Forms.Label lblPSExecPath;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.TextBox txtSchFile;
        private System.Windows.Forms.Button btnBrowseSchFile;
        private System.Windows.Forms.Label lblSchFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbFirstTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tbpModules;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader chModName;
        private System.Windows.Forms.ToolStrip tbModules;
        private System.Windows.Forms.ColumnHeader chRepository;
        private System.Windows.Forms.ColumnHeader chBranch;
        private System.Windows.Forms.ToolStripButton btnAddModule;
        private System.Windows.Forms.ToolStripProgressBar pbStatus;
        private System.Windows.Forms.ToolStripLabel lblStatus;
    }
}