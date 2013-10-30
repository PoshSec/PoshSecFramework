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
        #endregion

        #region Public Methods
        public Schedule()
        {
            
        }

        public Schedule(int Interval)
        {
            interval = Interval;            
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
            catch (Exception)
            {
                rtn = false;    
            }
            return rtn;
        }

        public bool Load()
        {
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
                catch (Exception)
                { 
                    rtn = false;
                }
            }
            return rtn;
        }
        #endregion

        #region Private Methods

        #endregion

        #region Private Events
        private void tmr_Elapsed(object sender, ElapsedEventArgs e)
        {

        }
        #endregion

        #region Public Properties
        public List<ScheduleItem> ScheduleItems
        {
            get { return schedule; }
            set { schedule = value; }
        }
        #endregion
    }
}
