using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.DirectoryServices;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PoshSec.Framework.Interface;
using PoshSec.Framework.Strings;
using ThreadState = System.Threading.ThreadState;

namespace PoshSec.Framework
{
    internal class NetworkBrowser
    {
        private readonly Network _network;

        private readonly List<Task> _tasks = new List<Task>();

        public bool CancelScan { get; set; }

        public event EventHandler<NetworkScanCompleteEventArgs> NetworkScanComplete;
        public event EventHandler<EventArgs> NetworkScanCancelled;
        public event EventHandler<ScanStatusEventArgs> ScanStatusUpdate;

        public NetworkBrowser(Network network)
        {
            _network = network;
        }

        public void ScanActiveDirectory()
        {
            if (_network == null) return;

            _network.Nodes.Clear();
            ClearArpTable();
            var hostPc = new DirectoryEntry { Path = "LDAP://" + _network.Name };
            SearchResultCollection searchResults = null;

            using (var srch = new DirectorySearcher(hostPc))
            {
                srch.Filter = "(&(objectClass=computer))";
                srch.SearchScope = SearchScope.Subtree;
                srch.PropertiesToLoad.Add("description");
                try
                {
                    searchResults = srch.FindAll();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }

            if (searchResults != null && searchResults.Count > 0)
            {
                var hostcnt = 0;
                foreach (SearchResult result in searchResults)
                {
                    hostcnt++;
                    var directoryEntry = result.GetDirectoryEntry();
                    var scnmsg = "Scanning " + directoryEntry.Name.Replace("CN=", "") + ", please wait...";
                    OnStatusUpdate(new ScanStatusEventArgs(scnmsg, hostcnt, searchResults.Count));
                    if (directoryEntry.Name.Replace("CN=", "") != "Schema" && directoryEntry.SchemaClassName == "computer")
                    {
                        Ping(directoryEntry.Name.Replace("CN=", ""), 1, 100);
                        _network.Nodes.AddRange(GetSystems(directoryEntry));
                    }
                }
            }

            BuildArpTable();
            OnNetworkScanComplete(new NetworkScanCompleteEventArgs(_network));
        }

        private IEnumerable<NetworkNode> GetSystems(DirectoryEntry directoryEntry)
        {
            var nodes = new List<NetworkNode>();
            var ipAddresses = GetIPAddresses(directoryEntry.Name.Replace("CN=", ""));
            foreach (var ip in ipAddresses)
            {
                OnStatusUpdate(new ScanStatusEventArgs("Adding " + directoryEntry.Name.Replace("CN=", "") + ", please wait...", 0, 255));
                var ipaddr = ip.ToString();
                var macaddr = GetMac(ipaddr);
                var isup = macaddr != StringValue.BlankMAC;
                var node = new NetworkNode
                {
                    Name = directoryEntry.Name.Replace("CN=", ""),
                    IpAddress = ipaddr,
                    MacAddress = macaddr,
                    Description = (string)directoryEntry.Properties["description"].Value ?? "",
                    Status = isup ? StringValue.Up : StringValue.Down,
                    ClientInstalled = StringValue.NotInstalled,
                    Alerts = 0,
                    LastScanned = DateTime.Now
                };
                nodes.Add(node);
            }
            return nodes;
        }

        public void ScanbyIP()
        {
            if (_network == null)
                return;
            _network.Nodes.Clear();
            ClearArpTable();
            var localIPs = GetIPAddresses(Dns.GetHostName()).ToArray();
            var localIP = localIPs.First();

            if (localIPs.Length > 1)
                localIP = PromptUserToSelectIP(localIPs);

            if (localIP == null)
            {
                OnScanCancelled(new EventArgs());
                return;
            }

            OnStatusUpdate(new ScanStatusEventArgs("", 0, 255));

            var localIpBytes = localIP.GetAddressBytes();
            for (byte index = 1; index < 255; index++)
            {
                var scanIp = new IPAddress(new[] { localIpBytes[0], localIpBytes[1], localIpBytes[2], index });

                OnStatusUpdate(new ScanStatusEventArgs("Scanning " + scanIp + ", please wait...", index, 255));

                var task = Task.Run(() =>
                {
                    var networkNode = GetNetworkNode(scanIp);
                    if (networkNode.Status == StringValue.Up)
                        _network.Nodes.Add(networkNode);
                });
                Application.DoEvents();
                _tasks.Add(task);

                if (CancelScan)
                {
                    OnScanCancelled(new EventArgs());
                    break;
                }
            }

            OnStatusUpdate(new ScanStatusEventArgs(StringValue.WaitingForHostResp, 255, 255));
            Task.WaitAll(_tasks.ToArray());

            BuildArpTable();

            OnStatusUpdate(new ScanStatusEventArgs(StringValue.Ready, 0, 255));

            OnNetworkScanComplete(new NetworkScanCompleteEventArgs(_network));
        }

        private static IPAddress PromptUserToSelectIP(IEnumerable<IPAddress> ipAddresses)
        {
            using (var frm = new frmScan
            {
                IPs = ipAddresses.Select(ip => ip.ToString()).ToArray(),
                StartPosition = FormStartPosition.CenterScreen
            })
            {
                return frm.ShowDialog() == DialogResult.OK ? IPAddress.Parse(frm.SelectedIP) : null;
            }
        }

        private NetworkNode GetNetworkNode(IPAddress ipAddress)
        {
            var host = StringValue.NAHost;
            var isup = false;
            string ipaddr = ipAddress.ToString();
            if (ipaddr != "")
            {
                if (Ping(ipaddr, 1, 100))
                {
                    isup = true;
                    host = GetHostname(ipaddr);
                }
            }
            var networkNode = new NetworkNode
            {
                IpAddress = ipaddr,
                Name = host,
                Status = isup ? StringValue.Up : StringValue.Down
            };
            return networkNode;
        }

        public static bool Ping(string host, int attempts, int timeout)
        {
            var response = false;
            try
            {
                var ping = new Ping();

                for (var attempt = 0; attempt < attempts; attempt++)
                    try
                    {
                        var pingReply = ping.Send(host, timeout);
                        if (pingReply?.Status == IPStatus.Success) response = true;
                    }
                    catch
                    {
                        response = false;
                    }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return response;
        }

        public static IEnumerable<IPAddress> GetIPAddresses(string host)
        {
            var addressList = new List<IPAddress>();
            try
            {
                var ipentry = Dns.GetHostEntry(host.Replace("CN=", ""));
                var addrs = ipentry.AddressList;
                foreach (var addr in addrs)
                    //Limit to IPv4 for now
                    if (addr.AddressFamily == AddressFamily.InterNetwork)
                    {
                        addressList.Add(addr);
                        Application.DoEvents();
                    }
            }
            catch
            {
                //ipadr = StringValue.UnknownHost;
            }

            return addressList;
        }

        public static string GetMac(string ipaddr)
        {
            var rtn = "";
            var arp = BuildArpTable();
            if (!string.IsNullOrEmpty(arp))
            {
                var ips = ipaddr.Split(',');
                foreach (var ip in ips)
                {
                    var ipidx = arp.IndexOf(ip + " ", 0);
                    if (ipidx > -1)
                    {
                        var mac = arp.Substring(ipidx, 39).Replace(ip, "").Trim();
                        if (mac.Contains("---")) mac = GetMyMac(ip);
                        rtn += mac + ",";
                    }
                    else
                    {
                        rtn += GetMyMac(ip);
                    }
                }

                if (rtn.EndsWith(",")) rtn = rtn.Substring(0, rtn.Length - 1);
            }

            if (rtn == "") rtn = StringValue.BlankMAC;
            return rtn;
        }

        public static string GetMyMac(string ipaddr)
        {
            var mac = StringValue.BlankMAC;
            try
            {
                var psi = new ProcessStartInfo("ipconfig")
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    Arguments = "/all"
                };
                var prc = new Process { StartInfo = psi };
                prc.Start();
                var ipconfig = prc.StandardOutput.ReadToEnd();
                prc.WaitForExit();

                if (!string.IsNullOrEmpty(ipconfig))
                {
                    var ipidx = ipconfig.IndexOf(ipaddr, 0, StringComparison.Ordinal);
                    if (ipidx > -1)
                    {
                        var paidx = ipconfig.ToLower().LastIndexOf("physical address", ipidx, StringComparison.Ordinal);
                        if (paidx > -1)
                            mac = ipconfig.Substring(paidx, 53).Replace("Physical Address. . . . . . . . . : ", "");
                    }
                }
            }
            catch (Exception)
            {
                mac = StringValue.BlankMAC;
            }

            return mac;
        }

        private static string GetHostname(string ip)
        {
            var host = "";
            try
            {
                var ipentry = Dns.GetHostEntry(IPAddress.Parse(ip));
                host = ipentry.HostName;
            }
            catch
            {
                host = StringValue.NAHost;
            }

            return host;
        }

        private static void ClearArpTable()
        {
            try
            {
                var psi = new ProcessStartInfo("arp")
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    Arguments = "-d"
                };
                var prc = new Process
                {
                    StartInfo = psi
                };
                prc.Start();
                prc.WaitForExit();
            }
            catch (Exception)
            {
                //do nothing
            }
        }

        private static string BuildArpTable()
        {
            try
            {
                var psi = new ProcessStartInfo("arp")
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    Arguments = "-a"
                };
                var prc = new Process
                {
                    StartInfo = psi
                };
                prc.Start();
                var arp = prc.StandardOutput.ReadToEnd();
                prc.WaitForExit();
                return arp;
            }
            catch (Exception)
            {
                //do nothing
            }

            return null;
        }

        private void OnStatusUpdate(ScanStatusEventArgs e)
        {
            var handler = ScanStatusUpdate;
            handler?.Invoke(this, e);
        }

        private void OnNetworkScanComplete(NetworkScanCompleteEventArgs e)
        {
            var handler = NetworkScanComplete;
            handler?.Invoke(this, e);
        }

        private void OnScanCancelled(EventArgs e)
        {
            var handler = NetworkScanCancelled;
            handler?.Invoke(this, e);
        }
    }
}