namespace poshsecframework.Interface
{
    partial class frmSchedule
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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.gbFrequency = new System.Windows.Forms.GroupBox();
            this.optMonthly = new System.Windows.Forms.RadioButton();
            this.optWeekly = new System.Windows.Forms.RadioButton();
            this.optDaily = new System.Windows.Forms.RadioButton();
            this.optOnce = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.datStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.datStartTime = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.tcSettings = new System.Windows.Forms.TabControl();
            this.tbDaily = new System.Windows.Forms.TabPage();
            this.tbWeekly = new System.Windows.Forms.TabPage();
            this.gbDays = new System.Windows.Forms.GroupBox();
            this.lstDays = new System.Windows.Forms.CheckedListBox();
            this.tbMonthly = new System.Windows.Forms.TabPage();
            this.gbDates = new System.Windows.Forms.GroupBox();
            this.lstDates = new System.Windows.Forms.CheckedListBox();
            this.gbMonths = new System.Windows.Forms.GroupBox();
            this.lstMonths = new System.Windows.Forms.CheckedListBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.gbFrequency.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tcSettings.SuspendLayout();
            this.tbWeekly.SuspendLayout();
            this.gbDays.SuspendLayout();
            this.tbMonthly.SuspendLayout();
            this.gbDates.SuspendLayout();
            this.gbMonths.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 308);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(521, 35);
            this.panel1.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(361, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(442, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(521, 26);
            this.panel2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(521, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Select the Frequency, Time, Weeks, Months, and Days for this script.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbFrequency
            // 
            this.gbFrequency.Controls.Add(this.optMonthly);
            this.gbFrequency.Controls.Add(this.optWeekly);
            this.gbFrequency.Controls.Add(this.optDaily);
            this.gbFrequency.Controls.Add(this.optOnce);
            this.gbFrequency.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbFrequency.Location = new System.Drawing.Point(0, 26);
            this.gbFrequency.Name = "gbFrequency";
            this.gbFrequency.Size = new System.Drawing.Size(103, 282);
            this.gbFrequency.TabIndex = 2;
            this.gbFrequency.TabStop = false;
            this.gbFrequency.Text = "Frequency";
            // 
            // optMonthly
            // 
            this.optMonthly.AutoSize = true;
            this.optMonthly.Dock = System.Windows.Forms.DockStyle.Top;
            this.optMonthly.Location = new System.Drawing.Point(3, 68);
            this.optMonthly.Name = "optMonthly";
            this.optMonthly.Size = new System.Drawing.Size(97, 17);
            this.optMonthly.TabIndex = 2;
            this.optMonthly.TabStop = true;
            this.optMonthly.Text = "Monthly";
            this.optMonthly.UseVisualStyleBackColor = true;
            this.optMonthly.CheckedChanged += new System.EventHandler(this.optMonthly_CheckedChanged);
            // 
            // optWeekly
            // 
            this.optWeekly.AutoSize = true;
            this.optWeekly.Dock = System.Windows.Forms.DockStyle.Top;
            this.optWeekly.Location = new System.Drawing.Point(3, 51);
            this.optWeekly.Name = "optWeekly";
            this.optWeekly.Size = new System.Drawing.Size(97, 17);
            this.optWeekly.TabIndex = 1;
            this.optWeekly.TabStop = true;
            this.optWeekly.Text = "Weekly";
            this.optWeekly.UseVisualStyleBackColor = true;
            this.optWeekly.CheckedChanged += new System.EventHandler(this.optWeekly_CheckedChanged);
            // 
            // optDaily
            // 
            this.optDaily.AutoSize = true;
            this.optDaily.Dock = System.Windows.Forms.DockStyle.Top;
            this.optDaily.Location = new System.Drawing.Point(3, 34);
            this.optDaily.Name = "optDaily";
            this.optDaily.Size = new System.Drawing.Size(97, 17);
            this.optDaily.TabIndex = 0;
            this.optDaily.Text = "Daily";
            this.optDaily.UseVisualStyleBackColor = true;
            this.optDaily.CheckedChanged += new System.EventHandler(this.optDaily_CheckedChanged);
            // 
            // optOnce
            // 
            this.optOnce.AutoSize = true;
            this.optOnce.Checked = true;
            this.optOnce.Dock = System.Windows.Forms.DockStyle.Top;
            this.optOnce.Location = new System.Drawing.Point(3, 17);
            this.optOnce.Name = "optOnce";
            this.optOnce.Size = new System.Drawing.Size(97, 17);
            this.optOnce.TabIndex = 3;
            this.optOnce.TabStop = true;
            this.optOnce.Text = "Once";
            this.optOnce.UseVisualStyleBackColor = true;
            this.optOnce.CheckedChanged += new System.EventHandler(this.optOnce_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.datStartDate);
            this.panel3.Controls.Add(this.lblDate);
            this.panel3.Controls.Add(this.datStartTime);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(103, 26);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(418, 23);
            this.panel3.TabIndex = 4;
            // 
            // datStartDate
            // 
            this.datStartDate.CustomFormat = "MM/dd/yyyy";
            this.datStartDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.datStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datStartDate.Location = new System.Drawing.Point(232, 0);
            this.datStartDate.Name = "datStartDate";
            this.datStartDate.Size = new System.Drawing.Size(110, 21);
            this.datStartDate.TabIndex = 3;
            this.datStartDate.ValueChanged += new System.EventHandler(this.datStartDate_ValueChanged);
            // 
            // lblDate
            // 
            this.lblDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblDate.Location = new System.Drawing.Point(160, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(72, 23);
            this.lblDate.TabIndex = 2;
            this.lblDate.Text = "Start Date:";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // datStartTime
            // 
            this.datStartTime.CustomFormat = "hh:mm tt";
            this.datStartTime.Dock = System.Windows.Forms.DockStyle.Left;
            this.datStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datStartTime.Location = new System.Drawing.Point(72, 0);
            this.datStartTime.Name = "datStartTime";
            this.datStartTime.ShowUpDown = true;
            this.datStartTime.Size = new System.Drawing.Size(88, 21);
            this.datStartTime.TabIndex = 1;
            this.datStartTime.ValueChanged += new System.EventHandler(this.datStartTime_ValueChanged);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Start Time:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tcSettings
            // 
            this.tcSettings.Controls.Add(this.tbDaily);
            this.tcSettings.Controls.Add(this.tbWeekly);
            this.tcSettings.Controls.Add(this.tbMonthly);
            this.tcSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSettings.ItemSize = new System.Drawing.Size(0, 1);
            this.tcSettings.Location = new System.Drawing.Point(103, 49);
            this.tcSettings.Name = "tcSettings";
            this.tcSettings.Padding = new System.Drawing.Point(0, 0);
            this.tcSettings.SelectedIndex = 0;
            this.tcSettings.Size = new System.Drawing.Size(418, 259);
            this.tcSettings.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcSettings.TabIndex = 6;
            // 
            // tbDaily
            // 
            this.tbDaily.Location = new System.Drawing.Point(4, 5);
            this.tbDaily.Name = "tbDaily";
            this.tbDaily.Padding = new System.Windows.Forms.Padding(3);
            this.tbDaily.Size = new System.Drawing.Size(410, 250);
            this.tbDaily.TabIndex = 2;
            this.tbDaily.Text = "Daily";
            this.tbDaily.UseVisualStyleBackColor = true;
            // 
            // tbWeekly
            // 
            this.tbWeekly.Controls.Add(this.gbDays);
            this.tbWeekly.Location = new System.Drawing.Point(4, 5);
            this.tbWeekly.Name = "tbWeekly";
            this.tbWeekly.Padding = new System.Windows.Forms.Padding(3);
            this.tbWeekly.Size = new System.Drawing.Size(410, 250);
            this.tbWeekly.TabIndex = 0;
            this.tbWeekly.Text = "Weekly";
            this.tbWeekly.UseVisualStyleBackColor = true;
            // 
            // gbDays
            // 
            this.gbDays.Controls.Add(this.lstDays);
            this.gbDays.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbDays.Location = new System.Drawing.Point(3, 3);
            this.gbDays.Name = "gbDays";
            this.gbDays.Size = new System.Drawing.Size(404, 78);
            this.gbDays.TabIndex = 7;
            this.gbDays.TabStop = false;
            this.gbDays.Text = "Days of the Week";
            // 
            // lstDays
            // 
            this.lstDays.CheckOnClick = true;
            this.lstDays.ColumnWidth = 100;
            this.lstDays.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDays.FormattingEnabled = true;
            this.lstDays.Items.AddRange(new object[] {
            "Sunday",
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday"});
            this.lstDays.Location = new System.Drawing.Point(3, 17);
            this.lstDays.MultiColumn = true;
            this.lstDays.Name = "lstDays";
            this.lstDays.Size = new System.Drawing.Size(398, 58);
            this.lstDays.TabIndex = 0;
            // 
            // tbMonthly
            // 
            this.tbMonthly.Controls.Add(this.gbDates);
            this.tbMonthly.Controls.Add(this.gbMonths);
            this.tbMonthly.Location = new System.Drawing.Point(4, 5);
            this.tbMonthly.Name = "tbMonthly";
            this.tbMonthly.Padding = new System.Windows.Forms.Padding(3);
            this.tbMonthly.Size = new System.Drawing.Size(410, 250);
            this.tbMonthly.TabIndex = 1;
            this.tbMonthly.Text = "Montly";
            this.tbMonthly.UseVisualStyleBackColor = true;
            // 
            // gbDates
            // 
            this.gbDates.Controls.Add(this.lstDates);
            this.gbDates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDates.Location = new System.Drawing.Point(3, 81);
            this.gbDates.Name = "gbDates";
            this.gbDates.Size = new System.Drawing.Size(404, 166);
            this.gbDates.TabIndex = 9;
            this.gbDates.TabStop = false;
            this.gbDates.Text = "Dates";
            // 
            // lstDates
            // 
            this.lstDates.CheckOnClick = true;
            this.lstDates.ColumnWidth = 60;
            this.lstDates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDates.FormattingEnabled = true;
            this.lstDates.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "Last Day"});
            this.lstDates.Location = new System.Drawing.Point(3, 17);
            this.lstDates.MultiColumn = true;
            this.lstDates.Name = "lstDates";
            this.lstDates.Size = new System.Drawing.Size(398, 146);
            this.lstDates.TabIndex = 2;
            // 
            // gbMonths
            // 
            this.gbMonths.Controls.Add(this.lstMonths);
            this.gbMonths.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbMonths.Location = new System.Drawing.Point(3, 3);
            this.gbMonths.Name = "gbMonths";
            this.gbMonths.Size = new System.Drawing.Size(404, 78);
            this.gbMonths.TabIndex = 8;
            this.gbMonths.TabStop = false;
            this.gbMonths.Text = "Months";
            // 
            // lstMonths
            // 
            this.lstMonths.CheckOnClick = true;
            this.lstMonths.ColumnWidth = 97;
            this.lstMonths.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstMonths.FormattingEnabled = true;
            this.lstMonths.Items.AddRange(new object[] {
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"});
            this.lstMonths.Location = new System.Drawing.Point(3, 17);
            this.lstMonths.MultiColumn = true;
            this.lstMonths.Name = "lstMonths";
            this.lstMonths.Size = new System.Drawing.Size(398, 58);
            this.lstMonths.TabIndex = 0;
            // 
            // frmSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 343);
            this.ControlBox = false;
            this.Controls.Add(this.tcSettings);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.gbFrequency);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSchedule";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Schedule Script";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.gbFrequency.ResumeLayout(false);
            this.gbFrequency.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.tcSettings.ResumeLayout(false);
            this.tbWeekly.ResumeLayout(false);
            this.gbDays.ResumeLayout(false);
            this.tbMonthly.ResumeLayout(false);
            this.gbDates.ResumeLayout(false);
            this.gbMonths.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox gbFrequency;
        private System.Windows.Forms.RadioButton optDaily;
        private System.Windows.Forms.RadioButton optMonthly;
        private System.Windows.Forms.RadioButton optWeekly;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DateTimePicker datStartTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tcSettings;
        private System.Windows.Forms.TabPage tbWeekly;
        private System.Windows.Forms.GroupBox gbDays;
        private System.Windows.Forms.TabPage tbMonthly;
        private System.Windows.Forms.TabPage tbDaily;
        private System.Windows.Forms.CheckedListBox lstDays;
        private System.Windows.Forms.GroupBox gbDates;
        private System.Windows.Forms.CheckedListBox lstDates;
        private System.Windows.Forms.GroupBox gbMonths;
        private System.Windows.Forms.CheckedListBox lstMonths;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton optOnce;
        private System.Windows.Forms.DateTimePicker datStartDate;
        private System.Windows.Forms.Label lblDate;
    }
}