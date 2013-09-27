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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbpGeneral = new System.Windows.Forms.TabPage();
            this.panel6 = new System.Windows.Forms.Panel();
            this.gbScriptSetting = new System.Windows.Forms.GroupBox();
            this.cmbScriptDefAction = new System.Windows.Forms.ComboBox();
            this.lblScriptDefAction = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtModuleDirectory = new System.Windows.Forms.TextBox();
            this.btnBrowseModule = new System.Windows.Forms.Button();
            this.lblModuleDirectory = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtFrameworkFile = new System.Windows.Forms.TextBox();
            this.btnBrowseFramework = new System.Windows.Forms.Button();
            this.lblFrameworkDirectory = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtScriptDirectory = new System.Windows.Forms.TextBox();
            this.btnBrowseScript = new System.Windows.Forms.Button();
            this.lblScriptDirectory = new System.Windows.Forms.Label();
            this.tcSettings = new System.Windows.Forms.TabControl();
            this.panel1.SuspendLayout();
            this.tbpGeneral.SuspendLayout();
            this.panel6.SuspendLayout();
            this.gbScriptSetting.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tcSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 210);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(460, 34);
            this.panel1.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(297, 6);
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
            this.btnCancel.Location = new System.Drawing.Point(378, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tbpGeneral
            // 
            this.tbpGeneral.Controls.Add(this.panel6);
            this.tbpGeneral.Controls.Add(this.panel5);
            this.tbpGeneral.Controls.Add(this.panel4);
            this.tbpGeneral.Controls.Add(this.panel3);
            this.tbpGeneral.Controls.Add(this.panel2);
            this.tbpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tbpGeneral.Name = "tbpGeneral";
            this.tbpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tbpGeneral.Size = new System.Drawing.Size(452, 184);
            this.tbpGeneral.TabIndex = 0;
            this.tbpGeneral.Text = "General";
            this.tbpGeneral.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.gbScriptSetting);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(3, 100);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(446, 79);
            this.panel6.TabIndex = 4;
            // 
            // gbScriptSetting
            // 
            this.gbScriptSetting.Controls.Add(this.cmbScriptDefAction);
            this.gbScriptSetting.Controls.Add(this.lblScriptDefAction);
            this.gbScriptSetting.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbScriptSetting.Location = new System.Drawing.Point(0, 0);
            this.gbScriptSetting.Name = "gbScriptSetting";
            this.gbScriptSetting.Size = new System.Drawing.Size(200, 79);
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
            this.cmbScriptDefAction.TabIndex = 1;
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
            this.panel5.Location = new System.Drawing.Point(3, 81);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(446, 19);
            this.panel5.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtModuleDirectory);
            this.panel4.Controls.Add(this.btnBrowseModule);
            this.panel4.Controls.Add(this.lblModuleDirectory);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 55);
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
            this.txtModuleDirectory.TabIndex = 2;
            // 
            // btnBrowseModule
            // 
            this.btnBrowseModule.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBrowseModule.Image = global::poshsecframework.Properties.Resources.document_open_folder;
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
            // panel3
            // 
            this.panel3.Controls.Add(this.txtFrameworkFile);
            this.panel3.Controls.Add(this.btnBrowseFramework);
            this.panel3.Controls.Add(this.lblFrameworkDirectory);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 29);
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
            this.txtFrameworkFile.TabIndex = 2;
            // 
            // btnBrowseFramework
            // 
            this.btnBrowseFramework.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnBrowseFramework.Image = global::poshsecframework.Properties.Resources.document_open_folder;
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
            this.btnBrowseScript.Image = global::poshsecframework.Properties.Resources.document_open_folder;
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
            this.tcSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSettings.Location = new System.Drawing.Point(0, 0);
            this.tcSettings.Name = "tcSettings";
            this.tcSettings.SelectedIndex = 0;
            this.tcSettings.Size = new System.Drawing.Size(460, 210);
            this.tcSettings.TabIndex = 1;
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(460, 244);
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
            this.gbScriptSetting.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tcSettings.ResumeLayout(false);
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
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.GroupBox gbScriptSetting;
        private System.Windows.Forms.ComboBox cmbScriptDefAction;
        private System.Windows.Forms.Label lblScriptDefAction;
        private System.Windows.Forms.Panel panel5;
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
    }
}