using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace poshsecframework.Enums
{
    public static class EnumValues
    {
        public enum RunAs
        { 
            CurrentUser = 0,
            DifferentUser
        }

        public enum TimeFrequency
        { 
            Daily = 0,
            Weekly,
            Monthly
        }
    }
}
