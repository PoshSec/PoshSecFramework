﻿namespace PoshSec.Framework.Interface
{
    partial class frmNetworkBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNetworkBrowser));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cmbNetworks = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this._lvwSystems = new SystemsListView();
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMAC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imgList16 = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 266);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(458, 35);
            this.panel1.TabIndex = 1;
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
            // panel2
            // 
            this.panel2.Controls.Add(this.btnRefresh);
            this.panel2.Controls.Add(this.cmbNetworks);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(458, 23);
            this.panel2.TabIndex = 2;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::PoshSec.Framework.Properties.Resources.viewrefresh7;
            this.btnRefresh.Location = new System.Drawing.Point(212, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(25, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // cmbNetworks
            // 
            this.cmbNetworks.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmbNetworks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNetworks.FormattingEnabled = true;
            this.cmbNetworks.Location = new System.Drawing.Point(59, 0);
            this.cmbNetworks.Name = "cmbNetworks";
            this.cmbNetworks.Size = new System.Drawing.Size(147, 21);
            this.cmbNetworks.TabIndex = 1;
            this.cmbNetworks.SelectedIndexChanged += new System.EventHandler(this.cmbNetworks_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Network:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lvwSystems
            // 
            this._lvwSystems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._lvwSystems.CheckBoxes = true;
            this._lvwSystems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chIP,
            this.chMAC,
            this.chStatus});
            this._lvwSystems.Dock = System.Windows.Forms.DockStyle.Fill;
            this._lvwSystems.FullRowSelect = true;
            this._lvwSystems.HideSelection = false;
            this._lvwSystems.Location = new System.Drawing.Point(0, 23);
            this._lvwSystems.Name = "_lvwSystems";
            this._lvwSystems.Size = new System.Drawing.Size(458, 243);
            this._lvwSystems.SmallImageList = this.imgList16;
            this._lvwSystems.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this._lvwSystems.TabIndex = 3;
            this._lvwSystems.UseCompatibleStateImageBehavior = false;
            this._lvwSystems.View = System.Windows.Forms.View.Details;
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 103;
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
            // imgList16
            // 
            this.imgList16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList16.ImageStream")));
            this.imgList16.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList16.Images.SetKeyName(0, "Server.png");
            // 
            // frmNetworkBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 301);
            this.ControlBox = false;
            this.Controls.Add(this._lvwSystems);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNetworkBrowser";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Network Browser Dialog";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ComboBox cmbNetworks;
        private System.Windows.Forms.Label label1;
        private SystemsListView _lvwSystems;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chIP;
        private System.Windows.Forms.ColumnHeader chMAC;
        private System.Windows.Forms.ColumnHeader chStatus;
        private System.Windows.Forms.ImageList imgList16;
    }
}