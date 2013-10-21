using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using poshsecframework.Strings;

namespace poshsecframework.Network
{
    class ScanIP
    {
        private String ipaddr = "";
        poshsecframework.Network.NetworkBrowser scnr = null;
        int idx = 0;

        public event EventHandler<ScanEventArgs> ScanIPComplete;

        public void Scan()
        {
            String host = StringValue.NAHost;
            bool isup = false;
            if (ipaddr != "")
            {
                scnr = new poshsecframework.Network.NetworkBrowser();
                if (scnr.Ping(ipaddr, 1, 100))
                {
                    isup = true;
                    host = scnr.GetHostname(ipaddr);
                }
                scnr = null;                
            }
            OnScanIPComplete(new ScanEventArgs(ipaddr, host, isup, idx));
        }

        private void OnScanIPComplete(ScanEventArgs e)
        {
            EventHandler<ScanEventArgs> handler = ScanIPComplete;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public String IPAddress
        {
            set { ipaddr = value; }
        }

        public int Index
        {
            set { idx = value; }
        }
    }
}
