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

        public List<NetworkNode> Nodes { get; protected set; }

        protected Network()
        {
            Nodes = new List<NetworkNode>();
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
