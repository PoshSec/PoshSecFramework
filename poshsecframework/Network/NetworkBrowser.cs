using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.DirectoryServices;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using PoshSec.Framework.Interface;
using PoshSec.Framework.Strings;
using ThreadState = System.Threading.ThreadState;

namespace PoshSec.Framework
{
    internal class NetworkBrowser
    {
        private frmMain _frmMain;
        private string _ipconfig = "";
        private string _arp = "";
        private readonly Collection<Thread> _threads = new Collection<Thread>();
        private string _domain = "";
        private bool _shstatus = true;

        public event EventHandler<NetworkScanCompleteEventArgs> ScanComplete;
        public event EventHandler<EventArgs> ScanCancelled;
        public event EventHandler<ScanStatusEventArgs> ScanUpdate;


        public void ScanActiveDirectory()
        {
            Systems.Clear();
            if (!string.IsNullOrEmpty(_domain))
            {
                ClearArpTable();
                var hostPC = new DirectoryEntry();
                hostPC.Path = "LDAP://" + _domain;
                SearchResultCollection srslts = null;

                using (var srch = new DirectorySearcher(hostPC))
                {
                    srch.Filter = "(&(objectClass=computer))";
                    srch.SearchScope = SearchScope.Subtree;
                    srch.PropertiesToLoad.Add("description");
                    try
                    {
                        srslts = srch.FindAll();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }

                if (srslts != null && srslts.Count > 0)
                {
                    var hostcnt = 0;
                    foreach (SearchResult srslt in srslts)
                    {
                        hostcnt++;
                        var netPC = srslt.GetDirectoryEntry();
                        var scnmsg = "Scanning " + netPC.Name.Replace("CN=", "") + ", please wait...";
                        OnScanUpdate(new ScanStatusEventArgs(scnmsg, hostcnt, srslts.Count));
                        if (netPC.Name.Replace("CN=", "") != "Schema" && netPC.SchemaClassName == "computer")
                        {
                            Ping(netPC.Name.Replace("CN=", ""), 1, 100);
                            Systems.Add(netPC);
                        }
                    }
                }

                BuildArpTable();
            }

            OnNetworkScanComplete(new NetworkScanCompleteEventArgs(Systems));
        }

        public void ScanbyIP()
        {
            Systems.Clear();
            ClearArpTable();
            var localIPs = GetIp(Dns.GetHostName()).Split(',');
            var localIP = localIPs[0];
            var cancelled = false;
            if (localIPs.Length > 1)
            {
                var frm = new frmScan();
                frm.IPs = localIPs;
                frm.StartPosition = FormStartPosition.CenterScreen;
                if (frm.ShowDialog() == DialogResult.OK)
                    localIP = frm.SelectedIP;
                else
                    cancelled = true;
                frm.Dispose();
                frm = null;
            }

            if (!string.IsNullOrEmpty(localIP) && !cancelled)
            {
                var ipparts = localIP.Split('.');
                if (ipparts.Length == 4)
                {
                    if (_shstatus) _frmMain.SetProgress(0, 255);
                    var ip = 1;
                    var cancel = false;
                    do
                    {
                        var host = ipparts[0] + "." + ipparts[1] + "." + ipparts[2] + "." + ip;
                        if (_shstatus) _frmMain.SetStatus("Scanning " + host + ", please wait...");
                        if (_shstatus) _frmMain.SetProgress(ip, 255);

                        var scn = new ScanIP
                        {
                            IPAddress = host,
                            Index = ip
                        };
                        scn.ScanIPComplete += scn_ScanIPComplete;
                        var thd = new Thread(scn.Scan);
                        _threads.Add(thd);
                        thd.Start();
                        ip++;
                        if (_shstatus) cancel = _frmMain.CancelIPScan;
                    } while (ip < 255 && !cancel);

                    if (_shstatus) _frmMain.SetStatus(StringValue.WaitingForHostResp);
                    do
                    {
                        Thread.Sleep(100);
                    } while (ThreadsActive());

                    Systems.Sort();
                    BuildArpTable();

                    if (_shstatus) _frmMain.HideProgress();
                    if (_shstatus) _frmMain.SetStatus(StringValue.Ready);

                    OnNetworkScanComplete(new NetworkScanCompleteEventArgs(Systems));
                }
            }
            else
            {
                OnScanCancelled(new EventArgs());
            }
        }

        private void scn_ScanIPComplete(object sender, ScanIpEventArgs e)
        {
            if (e.IsUp) Systems.Add(e.Index.ToString("000") + "|" + e.IpAddress + "|" + e.Hostname);
        }

        private bool ThreadsActive()
        {
            var rtn = false;
            foreach (var thd in _threads)
                if (thd.ThreadState != ThreadState.Stopped)
                    rtn = true;
            return rtn;
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

        public static string GetIp(string host)
        {
            var ipadr = "";
            try
            {
                var ipentry = Dns.GetHostEntry(host.Replace("CN=", ""));
                var addrs = ipentry.AddressList;
                foreach (var addr in addrs)
                    //Limit to IPv4 for now
                    if (addr.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipadr += addr + ",";
                        Application.DoEvents();
                    }

                ipadr = ipadr.Substring(0, ipadr.Length - 1);
            }
            catch
            {
                ipadr = StringValue.UnknownHost;
            }

            return ipadr;
        }

        public string GetMac(string ipaddr)
        {
            var rtn = "";
            if (!string.IsNullOrEmpty(_arp))
            {
                var ips = ipaddr.Split(',');
                foreach (var ip in ips)
                {
                    var ipidx = _arp.IndexOf(ip + " ", 0);
                    if (ipidx > -1)
                    {
                        var mac = _arp.Substring(ipidx, 39).Replace(ip, "").Trim();
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

        public string GetMyMac(string ipaddr)
        {
            var rtn = StringValue.BlankMAC;
            try
            {
                if (_ipconfig == "")
                {
                    var psi = new ProcessStartInfo("ipconfig");
                    psi.UseShellExecute = false;
                    psi.CreateNoWindow = true;
                    psi.RedirectStandardOutput = true;
                    psi.Arguments = "/all";
                    var prc = new Process();
                    prc.StartInfo = psi;
                    prc.Start();
                    _ipconfig = prc.StandardOutput.ReadToEnd();
                    prc.WaitForExit();
                    prc = null;
                }

                if (_ipconfig != null && _ipconfig != "")
                {
                    var ipidx = _ipconfig.IndexOf(ipaddr, 0);
                    if (ipidx > -1)
                    {
                        var paidx = _ipconfig.ToLower().LastIndexOf("physical address", ipidx);
                        if (paidx > -1)
                            rtn = _ipconfig.Substring(paidx, 53).Replace("Physical Address. . . . . . . . . : ", "");
                    }
                }
            }
            catch (Exception)
            {
                rtn = StringValue.BlankMAC;
            }

            return rtn;
        }

        public static string GetHostname(string ip)
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
                var psi = new ProcessStartInfo("arp");
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                psi.Arguments = "-d";
                var prc = new Process();
                prc.StartInfo = psi;
                prc.Start();
                prc.WaitForExit();
                prc = null;
            }
            catch (Exception)
            {
                //do nothing
            }
        }

        private void BuildArpTable()
        {
            try
            {
                var psi = new ProcessStartInfo("arp");
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                psi.RedirectStandardOutput = true;
                psi.Arguments = "-a";
                var prc = new Process();
                prc.StartInfo = psi;
                prc.Start();
                _arp = prc.StandardOutput.ReadToEnd();
                prc.WaitForExit();
                prc = null;
            }
            catch (Exception)
            {
                //do nothing
            }
        }

        private void OnScanUpdate(ScanStatusEventArgs e)
        {
            var handler = ScanUpdate;
            handler?.Invoke(this, e);
        }

        private void OnNetworkScanComplete(NetworkScanCompleteEventArgs e)
        {
            var handler = ScanComplete;
            handler?.Invoke(this, e);
        }

        private void OnScanCancelled(EventArgs e)
        {
            var handler = ScanCancelled;
            handler?.Invoke(this, e);
        }

        public frmMain ParentForm
        {
            set => _frmMain = value;
        }

        public bool ShowStatus
        {
            set => _shstatus = value;
        }

        public string Domain
        {
            set => _domain = value;
        }

        public ArrayList Systems { get; } = new ArrayList();
    }
}