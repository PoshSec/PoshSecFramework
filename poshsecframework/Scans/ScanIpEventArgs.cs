using System;

namespace PoshSec.Framework
{
    internal class ScanIpEventArgs : EventArgs
    {
        public ScanIpEventArgs(string ipAddress, string hostname, bool isUp, int index)
        {
            IpAddress = ipAddress;
            Hostname = hostname;
            IsUp = isUp;
            Index = index;
        }

        public string IpAddress { get; }

        public string Hostname { get; }

        public bool IsUp { get; }

        public int Index { get; }
    }
}
