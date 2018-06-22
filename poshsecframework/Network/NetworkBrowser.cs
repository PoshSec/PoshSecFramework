using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using PoshSec.Framework.Interface;
using PoshSec.Framework.Strings;

namespace PoshSec.Framework
{
    class NetworkBrowser
    {
        #region Private Variables
        frmMain frm = null;
        String ipconfig = "";
        String arp = "";
        ArrayList systems = new ArrayList();
        Collection<Thread> thds = new Collection<Thread>();
        string domain = "";
        bool shstatus = true;
        #endregion

        public event EventHandler<NetworkScanCompleteEventArgs> ScanComplete;
        public event EventHandler<EventArgs> ScanCancelled;
        public event EventHandler<ScanStatusEventArgs> ScanUpdate;

        #region Initialize
        /// <summary>
        /// Creates an instance of the NetworkBrowser Class
        /// </summary>
        public NetworkBrowser()
        {
            //todo: Initialize
        }
        #endregion

        #region Public Methods

        #region Scan
        public void ScanActiveDirectory()
        {
            systems.Clear();
            if (!string.IsNullOrEmpty(domain))
            {
                ClearArpTable();
                DirectoryEntry hostPC = new DirectoryEntry();
                hostPC.Path = "LDAP://" + domain;
                SearchResultCollection srslts = null;

                using (DirectorySearcher srch = new DirectorySearcher(hostPC))
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
                    int hostcnt = 0;
                    foreach (SearchResult srslt in srslts)
                    {
                        hostcnt++;
                        DirectoryEntry netPC = srslt.GetDirectoryEntry();
                        string scnmsg = "Scanning " + netPC.Name.Replace("CN=", "") + ", please wait...";
                        OnScanUpdate(new ScanStatusEventArgs(scnmsg, hostcnt, srslts.Count));
                        if (netPC.Name.Replace("CN=", "") != "Schema" && netPC.SchemaClassName == "computer")
                        {
                            Ping(netPC.Name.Replace("CN=", ""), 1, 100);
                            systems.Add(netPC);
                        }                       
                    }
                }
                BuildArpTable();
            }            
            OnNetworkScanComplete(new NetworkScanCompleteEventArgs(systems));
        }

        public void ScanbyIP()
        {
            systems.Clear();
            ClearArpTable();
            String[] localIPs = GetIP(Dns.GetHostName()).Split(',');
            String localIP = localIPs[0];
            bool cancelled = false;
            if (localIPs.Length > 1)
            {
                frmScan frm = new frmScan();
                frm.IPs = localIPs;
                frm.StartPosition = FormStartPosition.CenterScreen;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    localIP = frm.SelectedIP;
                }
                else
                {
                    cancelled = true;
                }
                frm.Dispose();
                frm = null;
            }
            if (localIP != "" && localIP != null && !cancelled)
            {
                String[] ipparts = localIP.Split('.');
                if (ipparts != null && ipparts.Length == 4)
                {
                    if (shstatus) { frm.SetProgress(0, 255); }                    
                    int ip = 1;
                    bool cancel = false;
                    do
                    {
                        String host = ipparts[0] + "." + ipparts[1] + "." + ipparts[2] + "." + ip.ToString();
                        if (shstatus) { frm.SetStatus("Scanning " + host + ", please wait...");}
                        if (shstatus) { frm.SetProgress(ip, 255);}

                        var scn = new ScanIP
                        {
                            IPAddress = host,
                            Index = ip
                        };
                        scn.ScanIPComplete += scn_ScanIPComplete;
                        System.Threading.Thread thd = new System.Threading.Thread(scn.Scan);
                        thds.Add(thd);
                        thd.Start();
                        ip++;
                        if (shstatus) { cancel = frm.CancelIPScan; }
                    } while (ip < 255 && !cancel);
                    if (shstatus) { frm.SetStatus(StringValue.WaitingForHostResp); }
                    do
                    {
                        System.Threading.Thread.Sleep(100);
                    } while (ThreadsActive());
                    systems.Sort();
                    BuildArpTable();

                    if (shstatus) { frm.HideProgress();}
                    if (shstatus) { frm.SetStatus(StringValue.Ready);}

                    OnNetworkScanComplete(new NetworkScanCompleteEventArgs(systems));
                }
            }
            else
            {
                OnScanCancelled(new EventArgs());
            }
        }

        void scn_ScanIPComplete(object sender, ScanIpEventArgs e)
        {
            if (e.IsUp)
            {
                systems.Add(e.Index.ToString("000") + "|" + e.IpAddress + "|" + e.Hostname);
            }
        }

        private bool ThreadsActive()
        {
            bool rtn = false;
            foreach (Thread thd in thds)
            {
                if (thd.ThreadState != ThreadState.Stopped)
                {
                    rtn = true;
                }
            }
            return rtn;
        }
        #endregion

        #endregion

        #region Public Functions

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

        #region GetIP
        public String GetIP(string host)
        {
            String ipadr = "";
            System.Net.IPHostEntry ipentry = null;
            try
            {
                ipentry = System.Net.Dns.GetHostEntry(host.Replace("CN=", ""));
                System.Net.IPAddress[] addrs = ipentry.AddressList;
                foreach (System.Net.IPAddress addr in addrs)
                {
                    //Limit to IPv4 for now
                    if (addr.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        ipadr += addr.ToString() + ",";
                        Application.DoEvents();
                    }                    
                }
                ipadr = ipadr.Substring(0, ipadr.Length - 1);
            }
            catch
            {
                ipadr = StringValue.UnknownHost;
            }
            return ipadr;
        }
        #endregion

        #region GetMac
        public String GetMac(String ipaddr)
        {
            String rtn = "";
            if (arp != null && arp != "")
            {
                String[] ips = ipaddr.Split(',');
                foreach (String ip in ips)
                {
                    int ipidx = arp.IndexOf(ip + " ", 0);
                    if (ipidx > -1)
                    {
                        String mac = arp.Substring(ipidx, 39).Replace(ip, "").Trim();
                        if (mac.Contains("---"))
                        {
                            mac = GetMyMac(ip);
                        }
                        rtn += mac + ",";
                    }
                    else
                    {
                        rtn += GetMyMac(ip);
                    }
                }
                if (rtn.EndsWith(","))
                {
                    rtn = rtn.Substring(0, rtn.Length - 1);
                }
            }
            if (rtn == "")
            {
                rtn = StringValue.BlankMAC;
            }
            return rtn;
        }

        public String GetMyMac(String ipaddr)
        {
            String rtn = StringValue.BlankMAC;
            try
            {
                if(ipconfig == "")
                {
                    System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("ipconfig");
                    psi.UseShellExecute = false;
                    psi.CreateNoWindow = true;
                    psi.RedirectStandardOutput = true;
                    psi.Arguments = "/all";
                    System.Diagnostics.Process prc = new System.Diagnostics.Process();
                    prc.StartInfo = psi;
                    prc.Start();
                    ipconfig = prc.StandardOutput.ReadToEnd();
                    prc.WaitForExit();
                    prc = null;
                }                
                if (ipconfig != null && ipconfig != "")
                {
                    int ipidx = ipconfig.IndexOf(ipaddr, 0);
                    if (ipidx > -1)
                    {
                        int paidx = ipconfig.ToLower().LastIndexOf("physical address", ipidx);
                        if (paidx > -1)
                        {
                            rtn = ipconfig.Substring(paidx, 53).Replace("Physical Address. . . . . . . . . : ", "");
                        }
                    }                    
                }
            }
            catch (Exception)
            {
                rtn = StringValue.BlankMAC;
            }
            return rtn;
        }
        #endregion

        #region GetHostname
        public String GetHostname(String ip)
        {
            String host = "";
            System.Net.IPHostEntry ipentry = null;
            try
            {
                ipentry = System.Net.Dns.GetHostEntry(IPAddress.Parse(ip));
                host = ipentry.HostName;
            }
            catch
            {
                host = StringValue.NAHost;
            }
            return host;
        }
        #endregion

        #endregion


        #region Arp
        private void ClearArpTable()
        {
            try
            {
                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("arp");
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                psi.Arguments = "-d";
                System.Diagnostics.Process prc = new System.Diagnostics.Process();
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
                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("arp");
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                psi.RedirectStandardOutput = true;
                psi.Arguments = "-a";
                System.Diagnostics.Process prc = new System.Diagnostics.Process();
                prc.StartInfo = psi;
                prc.Start();
                arp = prc.StandardOutput.ReadToEnd();
                prc.WaitForExit();
                prc = null;
            }
            catch (Exception)
            {
                //do nothing
            }
        }
        #endregion

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

        #region Public Properties
        public frmMain ParentForm
        {
            set { frm = value; }
        }

        public bool ShowStatus
        {
            set { shstatus = value; }
        }

        public String Domain
        {
            set { domain = value; }
        }

        public ArrayList Systems
        {
            get { return systems; }
        }
        #endregion
    }
}
