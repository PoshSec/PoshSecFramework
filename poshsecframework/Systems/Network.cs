using System.Collections.Concurrent;
using System.Collections.Generic;

namespace PoshSec.Framework
{
    public abstract class Network
    {
        public static Network Empty { get; }

        static Network()
        {
            Empty = new EmptyNetwork();
        }

        public string Name { get; set; }

        public ConcurrentBag<NetworkNode> Nodes { get; protected set; }

        protected Network()
        {
            Nodes = new ConcurrentBag<NetworkNode>();
        }

        private class EmptyNetwork : Network
        {
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
