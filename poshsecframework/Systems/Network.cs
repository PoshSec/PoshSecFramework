using System.Collections.Generic;

namespace PoshSec.Framework
{
    public abstract class Network
    {
        public string Name { get; set; }

        public List<NetworkNode> Nodes { get; protected set; }

        protected Network()
        {
            Nodes = new List<NetworkNode>();
        }
    }
}
