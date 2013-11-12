using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using poshsecframework.Utility;

namespace poshsecframework.Interface
{
    public partial class frmSchedule : Form
    {
        #region Private Variables
        private ScheduleTime schedtime = new ScheduleTime();
        #endregion

        #region Public Methods
        public frmSchedule()
        {
            InitializeComponent();
            schedtime.Frequency = Enums.EnumValues.TimeFrequency.Daily;
            schedtime.StartTime = datStartTime.Value;
        }
        #endregion
        
        #region Private Events
        private void optDaily_CheckedChanged(object sender, EventArgs e)
        {
            tcSettings.SelectedTab = tbDaily;
            schedtime.Frequency = Enums.EnumValues.TimeFrequency.Daily;
        }

        private void optWeekly_CheckedChanged(object sender, EventArgs e)
        {
            tcSettings.SelectedTab = tbWeekly;
            schedtime.Frequency = Enums.EnumValues.TimeFrequency.Weekly;
        }

        private void optMonthly_CheckedChanged(object sender, EventArgs e)
        {
            tcSettings.SelectedTab = tbMonthly;
            schedtime.Frequency = Enums.EnumValues.TimeFrequency.Monthly;
        }

        private void datStartTime_ValueChanged(object sender, EventArgs e)
        {
            schedtime.StartTime = datStartTime.Value;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool save = false;
            switch (schedtime.Frequency)
            {
                case Enums.EnumValues.TimeFrequency.Daily:
                    save = true;
                    break;
                case Enums.EnumValues.TimeFrequency.Weekly:
                    if (lstDays.CheckedItems.Count > 0)
                    {
                        List<int> daysofweek = new List<int>();
                        foreach (int idx in lstDays.CheckedIndices)
                        {
                            daysofweek.Add(idx);
                        }
                        schedtime.DaysofWeek = daysofweek;
                        save = true;
                    }
                    else
                    {
                        MessageBox.Show(Strings.StringValue.SelectWeekdays);
                    }
                    break;
                case Enums.EnumValues.TimeFrequency.Monthly:
                    if (lstMonths.CheckedItems.Count > 0 && lstDates.CheckedItems.Count > 0)
                    {
                        List<int> mons = new List<int>();
                        List<int> dates = new List<int>();
                        foreach (int idx in lstMonths.CheckedIndices)
                        {
                            mons.Add(idx + 1);
                        }
                        foreach (int idx in lstDates.CheckedIndices)
                        {
                            dates.Add(idx + 1);
                        }
                        schedtime.Months = mons;
                        schedtime.Dates = dates;
                        save = true;
                    }
                    else
                    {
                        MessageBox.Show(Strings.StringValue.SelectMonths);
                    }
                    break;
            }
            if (save)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
        #endregion        

        #region Public Properties
        public ScheduleTime ScheduledTime
        {
            get { return this.schedtime; }
        }
        #endregion

    }
}
