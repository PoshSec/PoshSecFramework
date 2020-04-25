using System;
using System.Net;
using System.Windows.Forms;
using PoshSec.Framework.Strings;

namespace PoshSec.Framework
{
    public class SystemsListViewItem : ListViewItem, INetworkNode
    {
        private SystemsListViewItem() 
        {
            ImageIndex = 2;            
            SubItems.Add(new ListViewSubItem(this,string.Empty){Name = "ip"}); 
            SubItems.Add(new ListViewSubItem(this,string.Empty){Name = "mac"});
            SubItems.Add(new ListViewSubItem(this,string.Empty){Name = "description"});
            SubItems.Add(new ListViewSubItem(this,string.Empty){Name = "status"});
            SubItems.Add(new ListViewSubItem(this,string.Empty){Name = "installed"});
            SubItems.Add(new ListViewSubItem(this,string.Empty){Name = "alerts"});
            SubItems.Add(new ListViewSubItem(this,string.Empty){Name = "lastscanned"});
        }

        public SystemsListViewItem(INetworkNode node) : this()
        {
            Name = node.Name;
            Text = node.Name;
            IpAddress = node.IpAddress;
            MacAddress = node.MacAddress;
            Description = node.Description;
            Status = node.Status;
            ClientInstalled = node.ClientInstalled;
            Alerts = node.Alerts;
            LastScanned = node.LastScanned;
        }

        public IPAddress IpAddress
        {
            get => IPAddress.TryParse(SubItems["ip"].Text, out var ipAddress) ? ipAddress : null;
            set => SubItems["ip"].Text = value.ToString();
        }

        public string MacAddress
        {
            get => SubItems["mac"].Text;
            set => SubItems["mac"].Text = value;
        }

        public string Description
        {
            get => SubItems["description"].Text;
            set => SubItems["description"].Text = value;
        }

        public string Status
        {
            get => SubItems["status"].Text;
            set => SubItems["status"].Text = value;
        }

        public string ClientInstalled
        {
            get => SubItems["installed"].Text;
            set => SubItems["installed"].Text = value;
        }

        public int Alerts
        {
            get => int.Parse(SubItems["alerts"].Text);
            set => SubItems["alerts"].Text = value.ToString();
        }

        public DateTime LastScanned
        {
            get => DateTime.Parse(SubItems["lastscanned"].Text);
            set => SubItems["lastscanned"].Text = value.ToString(StringValue.TimeFormat);
        }
    }
}