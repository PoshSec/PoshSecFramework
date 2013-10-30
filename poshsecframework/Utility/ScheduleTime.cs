using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using poshsecframework.Enums;

namespace poshsecframework.Utility
{
    public class ScheduleTime
    {
        public DateTime StartTime { get; set; }
        public poshsecframework.Enums.EnumValues.TimeFrequency Frequency { get; set; }
    }
}
