namespace PoshSec.Framework.FirstTimeSetup
{
    partial class frmFirstTime
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFirstTime));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDont = new System.Windows.Forms.Button();
            this.btnContinue = new System.Windows.Forms.Button();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lvwSteps = new System.Windows.Forms.ListView();
            this.chStep = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chResult = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.pnlFix = new System.Windows.Forms.Panel();
            this.btnFix = new System.Windows.Forms.Button();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlFix.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDont);
            this.panel1.Controls.Add(this.btnContinue);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 313);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(722, 35);
            this.panel1.TabIndex = 2;
            // 
            // btnDont
            // 
            this.btnDont.Location = new System.Drawing.Point(6, 6);
            this.btnDont.Name = "btnDont";
            this.btnDont.Size = new System.Drawing.Size(180, 23);
            this.btnDont.TabIndex = 4;
            this.btnDont.Text = "&Don\'t do anything. I\'ll do it myself.";
            this.btnDont.UseVisualStyleBackColor = true;
            this.btnDont.Click += new System.EventHandler(this.btnDont_Click);
            // 
            // btnContinue
            // 
            this.btnContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnContinue.Location = new System.Drawing.Point(644, 6);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(75, 23);
            this.btnContinue.TabIndex = 3;
            this.btnContinue.Text = "&Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            // 
            // lblInstructions
            // 
            this.lblInstructions.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblInstructions.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblInstructions.Location = new System.Drawing.Point(0, 0);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(722, 57);
            this.lblInstructions.TabIndex = 3;
            this.lblInstructions.Text = "Please select the steps you would like the PoshSec Framework to perform for you t" +
    "o configure your system.";
            this.lblInstructions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lvwSteps);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 57);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(276, 256);
            this.panel2.TabIndex = 4;
            // 
            // lvwSteps
            // 
            this.lvwSteps.CheckBoxes = true;
            this.lvwSteps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chStep,
            this.chResult});
            this.lvwSteps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwSteps.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lvwSteps.FullRowSelect = true;
            this.lvwSteps.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwSteps.Location = new System.Drawing.Point(0, 0);
            this.lvwSteps.Name = "lvwSteps";
            this.lvwSteps.ShowGroups = false;
            this.lvwSteps.Size = new System.Drawing.Size(276, 256);
            this.lvwSteps.TabIndex = 0;
            this.lvwSteps.UseCompatibleStateImageBehavior = false;
            this.lvwSteps.View = System.Windows.Forms.View.Details;
            this.lvwSteps.SelectedIndexChanged += new System.EventHandler(this.lvwSteps_SelectedIndexChanged);
            // 
            // chStep
            // 
            this.chStep.Text = "Step";
            this.chStep.Width = 170;
            // 
            // chResult
            // 
            this.chResult.Text = "Result";
            this.chResult.Width = 90;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtDescription);
            this.panel3.Controls.Add(this.pnlFix);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(276, 57);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(446, 256);
            this.panel3.TabIndex = 5;
            // 
            // txtDescription
            // 
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtDescription.Location = new System.Drawing.Point(0, 0);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(446, 224);
            this.txtDescription.TabIndex = 2;
            this.txtDescription.Text = "Select a step to the left to read the description.";
            // 
            // pnlFix
            // 
            this.pnlFix.Controls.Add(this.btnFix);
            this.pnlFix.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFix.Location = new System.Drawing.Point(0, 224);
            this.pnlFix.Name = "pnlFix";
            this.pnlFix.Size = new System.Drawing.Size(446, 32);
            this.pnlFix.TabIndex = 0;
            this.pnlFix.Visible = false;
            // 
            // btnFix
            // 
            this.btnFix.Location = new System.Drawing.Point(5, 4);
            this.btnFix.Name = "btnFix";
            this.btnFix.Size = new System.Drawing.Size(75, 23);
            this.btnFix.TabIndex = 4;
            this.btnFix.Text = "&Fix";
            this.btnFix.UseVisualStyleBackColor = true;
            this.btnFix.Click += new System.EventHandler(this.btnFix_Click);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "dialog-accept-2.png");
            this.imgList.Images.SetKeyName(1, "dialog-cancel-6.png");
            this.imgList.Images.SetKeyName(2, "dialog-disable.png");
            this.imgList.Images.SetKeyName(3, "system-run-3.png");
            // 
            // frmFirstTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 348);
            this.ControlBox = false;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblInstructions);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFirstTime";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PoshSec Framework First Time Setup";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlFix.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnDont;
        private System.Windows.Forms.ListView lvwSteps;
        private System.Windows.Forms.ColumnHeader chStep;
        private System.Windows.Forms.ColumnHeader chResult;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Panel pnlFix;
        private System.Windows.Forms.Button btnFix;
    }
}