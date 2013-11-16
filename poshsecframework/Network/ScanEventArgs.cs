using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace poshsecframework.Network
{
    class ScanEventArgs : EventArgs
    {
        #region Private Variables
        ArrayList systems = null;
        String ipaddr = "";
        String hostname = "";
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
    }
}
