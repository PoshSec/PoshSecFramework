using System;

namespace PoshSec.Framework
{
    public class CurrentNetworkChangedEventArgs : EventArgs
    {
        public Network Network { get; }

        public CurrentNetworkChangedEventArgs(Network network)
        {
            Network = network;
        }
    }
}