using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace poshsecframework.Network
{
    class ScanIP
    {
        private String ipaddr = "";
        psframework.Network.NetworkBrowser scnr = null;
        int idx = 0;

        public event EventHandler<ScanEventArgs> ScanIPComplete;

        public void Scan()
        {
            String host = "N/A";
            bool isup = false;
            if (ipaddr != "")
            {
                scnr = new psframework.Network.NetworkBrowser();
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
