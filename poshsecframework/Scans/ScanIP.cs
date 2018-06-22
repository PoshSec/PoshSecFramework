using System;
using PoshSec.Framework.Strings;

namespace PoshSec.Framework
{
    internal class ScanIP
    {
        private int idx;
        private string ipaddr = "";
        private NetworkBrowser scnr;

        public string IPAddress
        {
            set => ipaddr = value;
        }

        public int Index
        {
            set => idx = value;
        }

        public event EventHandler<ScanIpEventArgs> ScanIPComplete;

        public void Scan()
        {
            var host = StringValue.NAHost;
            var isup = false;
            if (ipaddr != "")
            {
                scnr = new NetworkBrowser();
                if (scnr.Ping(ipaddr, 1, 100))
                {
                    isup = true;
                    host = scnr.GetHostname(ipaddr);
                }

                scnr = null;
            }

            OnScanIPComplete(new ScanIpEventArgs(ipaddr, host, isup, idx));
        }

        private void OnScanIPComplete(ScanIpEventArgs e)
        {
            var handler = ScanIPComplete;
            handler?.Invoke(this, e);
        }
    }
}