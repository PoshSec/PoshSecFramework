using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using poshsecframework.Enums;
using poshsecframework.PShell;

namespace poshsecframework.Utility
{
    public class ScheduleItem
    {
        String scriptname = "";
        String scriptpath = "";
        psparamtype param = new psparamtype();
        ScheduleTime schedtime = null;
        EnumValues.RunAs runas = EnumValues.RunAs.CurrentUser;

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
    }
}
