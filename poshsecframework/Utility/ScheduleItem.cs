using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PoshSec.Framework.Enums;
using PoshSec.Framework.PShell;

namespace PoshSec.Framework.Utility
{
    public class ScheduleItem
    {
        private String scriptname = "";
        private String scriptpath = "";
        private psparamtype param = new psparamtype();
        private ScheduleTime schedtime = null;
        private EnumValues.RunAs runas = EnumValues.RunAs.CurrentUser;
        private String lastrun = "Never";
        private String msg = "";
        private int idx;

        public String ScriptName
        {
            get { return scriptname; }
            set { scriptname = value; }
        }

        public String ScriptPath
        {
            get { return scriptpath; }
            set { scriptpath = value; }
        }

        public psparamtype Parameters
        {
            get { return param; }
            set { param = value; }
        }

        public ScheduleTime ScheduledTime
        {
            get { return schedtime; }
            set { schedtime = value; }
        }

        public EnumValues.RunAs RunAs
        {
            get { return runas; }
            set { runas = value; }
        }

        public String LastRunTime
        {
            get { return lastrun; }
            set { lastrun = value; }
        }

        public String Message
        {
            get { return msg; }
            set { msg = value; }
        }

        public int Index
        {
            get { return idx; }
            set { idx = value; }
        }
    }
}
