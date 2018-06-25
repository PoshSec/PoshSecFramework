using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PoshSec.Framework.Enums
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
            Once = 0,
            Daily,
            Weekly,
            Monthly            
        }

        public enum FilterType
        {
            XML = 1,
            CSV,
            TXT
        }
    }
}
