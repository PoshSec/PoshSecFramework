using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PoshSec.Framework.Utility
{
    public class ScheduleEventArgs : EventArgs
    {
        private ScheduleItem sched;

        public ScheduleEventArgs(ScheduleItem schedule)
        {
            sched = schedule;
        }

        public ScheduleItem Schedule 
        {
            get { return sched; } 
        }
    }
}
