using System;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PoshSec.Framework
{
    [JsonObject]
    public class NetworkNode : INetworkNode
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonConverter(typeof(IPAddressConverter))]
        public IPAddress IpAddress { get; set; }

        [JsonProperty("MacAddress")]
        public string MacAddress { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonIgnore]
        public string Status { get; set; }

        [JsonProperty("ClientInstalled")]
        public string ClientInstalled { get; set; }

        [JsonIgnore]
        public int Alerts { get; set; }

        [JsonProperty("LastScanned")]
        public DateTime LastScanned { get; set; }
    }
}