using System;

namespace PoshSec.Framework
{
    public interface INetworkNode
    {
        string IpAddress { get; set; }
        string MacAddress { get; set; }
        string Description { get; set; }
        string Status { get; set; }
        string ClientInstalled { get; set; }
        int Alerts { get; set; }
        DateTime LastScanned { get; set; }
    }
}