using System.Collections.Concurrent;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PoshSec.Framework
{
    [JsonObject]
    public abstract class Network
    {
        public static Network Empty { get; }

        static Network()
        {
            Empty = new EmptyNetwork();
        }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Nodes")]
        public ConcurrentBag<INetworkNode> Nodes { get; protected set; }

        protected Network()
        {
            Nodes = new ConcurrentBag<INetworkNode>();
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
