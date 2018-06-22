using System;
using PoshSec.Framework.Strings;

namespace PoshSec.Framework
{
    internal class ScanIP
    {
        private int _idx;
        private string _ipaddr = "";
        private NetworkBrowser _scnr;

        public string IPAddress
        {
            set => _ipaddr = value;
        }

        public int Index
        {
            set => _idx = value;
        }

        public event EventHandler<ScanIpEventArgs> ScanIPComplete;

        public void Scan()
        {
            var host = StringValue.NAHost;
            var isup = false;
            if (_ipaddr != "")
            {
                _scnr = new NetworkBrowser();
                if (NetworkBrowser.Ping(_ipaddr, 1, 100))
                {
                    isup = true;
                    host = _scnr.GetHostname(_ipaddr);
                }

                _scnr = null;
            }

            OnScanIPComplete(new ScanIpEventArgs(_ipaddr, host, isup, _idx));
        }

        private void OnScanIPComplete(ScanIpEventArgs e)
        {
            var handler = ScanIPComplete;
            handler?.Invoke(this, e);
        }
    }
}