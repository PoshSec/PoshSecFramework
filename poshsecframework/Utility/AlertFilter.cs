using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poshsecframework.Utility
{
    internal static class AlertFilter
    {
        static bool info = true;
        static bool error = true;
        static bool warning = true;
        static bool severe = true;
        static bool critical = true;

        public static bool Informational
        {
            get { return info; }
            set { info = value; }
        }

        public static bool Error
        {
            get { return error; }
            set { error = value; }
        }

        public static bool Warning
        {
            get { return warning; }
            set { warning = value; }
        }

        public static bool Severe
        {
            get { return severe; }
            set { severe = value; }
        }

        public static bool Critical
        {
            get { return critical; }
            set { critical = value; }
        }
    }
}
