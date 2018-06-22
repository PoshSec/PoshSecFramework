using System;
using System.Collections;

namespace PoshSec.Framework
{
    public class NetworkScanCompleteEventArgs : EventArgs
    {
        public NetworkScanCompleteEventArgs(ArrayList systems)
        {
            Systems = systems;
        }

        public ArrayList Systems { get; } = null;
    }
}