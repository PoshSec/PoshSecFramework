using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Xml;
using System.Xml.Serialization;

namespace poshsecframework.Utility
{
    [XmlRoot("Schedule", Namespace = "poshsecframework.Utility", IsNullable = false)]
    public class Schedule
    {
        #region Private Variables
        private int interval = 1000;
        private Timer tmr = new Timer();
        List<ScheduleItem> schedule = new List<ScheduleItem>();
        bool ischecking = false;
        private Exception lastexception;
        #endregion

        #region Public Events
        [XmlIgnore]
        public EventHandler<ScheduleEventArgs> ItemUpdated;
        [XmlIgnore]
        public EventHandler<ScheduleEventArgs> ScriptInvoked;
        #endregion

        #region Public Methods
        public Schedule()
        {
            tmr.Elapsed += tmr_Elapsed;
        }

        public Schedule(int Interval)
        {
            interval = Interval;
            tmr.Elapsed += tmr_Elapsed;
        }

        public void Pause()
        {
            tmr.Enabled = false;
        }

        public void Resume()
        {
            tmr.Enabled = true;
        }

        public bool Save()
        {
            bool rtn = false;
            String schfile = poshsecframework.Properties.Settings.Default.ScheduleFile;
            FileInfo finfo = new FileInfo(schfile);
            try
            {
                if (!Directory.Exists(finfo.DirectoryName))
                {
                    Directory.CreateDirectory(finfo.DirectoryName);
                }
                using (TextWriter txtwtr = new StreamWriter(poshsecframework.Properties.Settings.Default.ScheduleFile))
                {
                    System.Xml.Serialization.XmlSerializer xmlser = new System.Xml.Serialization.XmlSerializer(typeof(Utility.Schedule));
                    xmlser.Serialize(txtwtr, this);
                    rtn = true;
                }
            }
            catch (Exception e)
            {
                lastexception = e;
                rtn = false;    
            }
            return rtn;
        }

        public bool Load()
        {
            tmr.Enabled = false;
            bool rtn = false;
            String schfile = poshsecframework.Properties.Settings.Default.ScheduleFile;
            if (File.Exists(schfile))
            {
                try
                {
                    using (TextReader txtrdr = new StreamReader(schfile))
                    {
                        System.Xml.Serialization.XmlSerializer xmlser = new System.Xml.Serialization.XmlSerializer(typeof(Utility.Schedule));
                        Schedule tmp = xmlser.Deserialize(txtrdr) as poshsecframework.Utility.Schedule;
                        this.ScheduleItems = tmp.ScheduleItems;
                        rtn = true;
                    }
                }
                catch (Exception e)
                {
                    lastexception = e;
                    rtn = false;
                }
            }
            tmr.Enabled = rtn;
            return rtn;
        }

        public void Remove(int Index)
        {
            if (this.ScheduleItems != null && this.ScheduleItems.Count > 0)
            {
                int idx = -1;
                bool found = false;
                ScheduleItem itm = null;
                do
                {
                    idx++;
                    if (this.schedule[idx].Index == Index)
                    {
                        found = true;
                        itm = this.schedule[idx];
                    }
                } while (idx < this.ScheduleItems.Count - 1 && !found);
                if (found)
                {
                    this.ScheduleItems.Remove(itm);
                }
            }
        }
        #endregion

        #region Private Methods
        private bool IsScheduledTime(ScheduleItem sched)
        {
            bool rtn = false;
            switch (sched.ScheduledTime.Frequency)
            { 
                case Enums.EnumValues.TimeFrequency.Daily:
                    //Only compare the time ignoring the seconds.
                    rtn = isittime(sched);
                    break;
                case Enums.EnumValues.TimeFrequency.Weekly:
                    //Check Day(s) then check time.
                    int didx = -1;
                    int today = (int)DateTime.Now.DayOfWeek;                    
                    if (sched.ScheduledTime.DaysofWeek.Count > 0)
                    {
                        bool found = false;
                        do
                        {
                            didx++;
                            if (sched.ScheduledTime.DaysofWeek[didx] == today)
                            {
                                found = true;
                            }
                        } while (didx < sched.ScheduledTime.DaysofWeek.Count - 1 && !found);
                        if (found)
                        {
                            rtn = isittime(sched);
                        }
                    }
                    break;
                case Enums.EnumValues.TimeFrequency.Monthly:
                    //Check Month, then dates, then time.
                    if (isthemonth(sched) && isthedate(sched))
                    {
                        rtn = isittime(sched);
                    }
                    break;
            }
            return rtn;
        }

        private bool isthemonth(ScheduleItem sched)
        {
            bool rtn = false;
            if (sched.ScheduledTime.Months.Count > 0)
            {
                int midx = -1;
                do
                {
                    midx++;
                    if (sched.ScheduledTime.Months[midx] == DateTime.Now.Month)
                    {
                        rtn = true;
                    }
                } while (midx < sched.ScheduledTime.Months.Count - 1 && !rtn);
            }
            return rtn;
        }

        private bool isthedate(ScheduleItem sched)
        {
            bool rtn = false;
            if (sched.ScheduledTime.Dates.Count > 0)
            {
                int didx = -1;
                do
                {
                    didx++;
                    if (sched.ScheduledTime.Dates[didx] == DateTime.Now.Day)
                    {
                        rtn = true;
                    }
                    else if ((sched.ScheduledTime.Dates[didx] == 32) && DateTime.Now.Day == DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month))
                    {
                        //Last day of month
                        rtn = true;
                    }
                } while (didx < sched.ScheduledTime.Dates.Count - 1 && !rtn);
            }
            return rtn;
        }

        private bool isittime(ScheduleItem sched)
        {
            bool rtn = false;
            if (DateTime.Now.ToString("HH:mm") == sched.ScheduledTime.StartTime.ToString("HH:mm"))
            {
                rtn = true;
            }
            return rtn;
        }

        private void RunScheduleScript(ScheduleItem sched)
        {
            sched.LastRunTime = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
            Save();
            OnItemUpdated(new ScheduleEventArgs(sched));
            OnScriptInvoked(new ScheduleEventArgs(sched));
        }

        private void OnItemUpdated(ScheduleEventArgs e)
        {
            EventHandler<ScheduleEventArgs> handler = ItemUpdated;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void OnScriptInvoked(ScheduleEventArgs e)
        {
            EventHandler<ScheduleEventArgs> handler = ScriptInvoked;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion

        #region Private Events
        private void tmr_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!ischecking)
            {
                ischecking = true;
                if (schedule.Count > 0)
                {
                    foreach (ScheduleItem sched in schedule)
                    {
                        if (IsScheduledTime(sched) && sched.LastRunTime != DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"))
                        {
                            RunScheduleScript(sched);
                        }
                    }
                }
                ischecking = false;
            }
        }
        #endregion

        #region Public Properties
        public List<ScheduleItem> ScheduleItems
        {
            get { return schedule; }
            set { schedule = value; }
        }

        public Exception LastException
        {
            get { return lastexception; }
        }
        #endregion
    }
}
