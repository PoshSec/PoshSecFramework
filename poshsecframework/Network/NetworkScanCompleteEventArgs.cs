using System;
using System.Collections;

namespace PoshSec.Framework
{
    public class NetworkScanCompleteEventArgs : EventArgs
    {
        public NetworkScanCompleteEventArgs(Network network)
        {
            Network = network;
        }

        public Network Network { get; set; }
    }
}