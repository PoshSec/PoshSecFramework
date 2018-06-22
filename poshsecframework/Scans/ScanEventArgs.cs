using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PoshSec.Framework
{
    class ScanEventArgs : EventArgs
    {
        #region Private Variables
        ArrayList systems = null;
        String ipaddr = "";
        String hostname = "";
        String status = "";
        int curidx = 0;
        int maxidx = 0;
        bool isup = false;
        int idx = 0;
        #endregion

        public ScanEventArgs(ArrayList Systems)
        {
            systems = Systems;
        }

        public ScanEventArgs(String IPAddress, String Hostname, bool IsUp, int Index)
        {
            ipaddr = IPAddress;
            hostname = Hostname;
            isup = IsUp;
            idx = Index;
        }

        public ScanEventArgs(String Status, int CurrentIndex, int MaxIndex)
        {
            status = Status;
            curidx = CurrentIndex;
            maxidx = MaxIndex;
        }

        public ArrayList Systems
        {
            get { return systems; }
        }

        public String IPAddress
        {
            get { return ipaddr; }
        }

        public String Hostname
        {
            get { return hostname; }
        }

        public Boolean IsUp
        {
            get { return isup; }
        }

        public int Index
        {
            get { return idx; }
        }

        public String Status
        {
            get { return status; }
        }

        public int CurrentIndex
        {
            get { return curidx; }
        }

        public int MaxIndex
        {
            get { return maxidx; }
        }
    }
}
