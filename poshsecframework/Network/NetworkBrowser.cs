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
using psframework.Structures;

namespace psframework.Network
{
    class NetworkBrowser
    {
        #region Private Variables
        frmMain frm = null;
        String ipconfig = "";
        String arp = "";
        ArrayList systems = new ArrayList();
        Collection<Thread> thds = new Collection<Thread>();
        bool waiting = false;
        #endregion

        #region Public Events
        public event EventHandler<poshsecframework.Network.ScanEventArgs> ScanComplete;
        #endregion

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
        public ArrayList ScanActiveDirectory(String domain)
        {
            ClearArpTable();
            ArrayList systems = new ArrayList();
            DirectoryEntry hostPC = new DirectoryEntry();
            hostPC.Path = "WinNT://" + domain;

            foreach (DirectoryEntry netPC in hostPC.Children)
            {
                if (netPC.Name != "Schema" && netPC.SchemaClassName == "Computer")
                {
                    Ping(netPC.Name, 1, 100);
                    systems.Add(netPC);                    
                }
            }
            BuildArpTable();
            return systems;
        }

        public void ScanbyIP()
        {            
            ClearArpTable();
            String[] localIPs = GetIP(Dns.GetHostName()).Split(',');
            String localIP = localIPs[0];
            bool cancelled = false;
            if (localIPs.Length > 1)
            {
                poshsecframework.Interface.frmScan frm = new poshsecframework.Interface.frmScan();
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
                    frm.SetProgress(0, 255);
                    int ip = 1;
                    waiting = false;
                    do
                    {
                        String host = ipparts[0] + "." + ipparts[1] + "." + ipparts[2] + "." + ip.ToString();
                        frm.SetStatus("Scanning " + host + ", please wait...");
                        frm.SetProgress(ip, 255);

                        poshsecframework.Network.ScanIP scn = new poshsecframework.Network.ScanIP();
                        scn.IPAddress = host;
                        scn.Index = ip;
                        scn.ScanIPComplete += scn_ScanIPComplete;
                        System.Threading.Thread thd = new System.Threading.Thread(scn.Scan);
                        thds.Add(thd);
                        thd.Start();
                        ip++;
                    } while(ip < 255 && !frm.CancelIPScan);
                    frm.SetStatus("Waiting for hostname responses, please wait...");
                    do
                    {
                        System.Threading.Thread.Sleep(100);
                    } while (ThreadsActive());
                    systems.Sort();
                    BuildArpTable();

                    frm.HideProgress();
                    frm.SetStatus("Ready");

                    OnScanComplete(new poshsecframework.Network.ScanEventArgs(systems));
                }
            }            
        }

        void scn_ScanIPComplete(object sender, poshsecframework.Network.ScanEventArgs e)
        {
            if (e.IsUp)
            {
                systems.Add(e.Index.ToString("000") + "|" + e.IPAddress + "|" + e.Hostname);
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

        #region Ping
        public bool Ping(string host, int attempts, int timeout)
        {
            bool rsp = false;
            try
            {
                System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
                System.Net.NetworkInformation.PingReply pingReply;

                for (int atmpt = 0; atmpt < attempts; atmpt++)
                {
                    try
                    {
                        pingReply = ping.Send(host, timeout);
                        if (pingReply != null && pingReply.Status == System.Net.NetworkInformation.IPStatus.Success)
                        {
                            rsp = true;
                        }
                    }
                    catch
                    {
                        rsp = false;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return rsp;
        }
        #endregion

        #region GetIP
        public String GetIP(string host)
        {
            String ipadr = "";
            System.Net.IPHostEntry ipentry = null;
            try
            {
                ipentry = System.Net.Dns.GetHostEntry(host);
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
                ipadr = "0.0.0.0 (unknown host)";
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
                    int ipidx = arp.IndexOf(ip, 0);
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
                rtn = "00-00-00-00-00-00";
            }
            return rtn;
        }

        public String GetMyMac(String ipaddr)
        {
            String rtn = "00-00-00-00-00-00";
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
                rtn = "00-00-00-00-00-00";
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
                host = "N/A";
            }
            return host;
        }
        #endregion

        #endregion

        #region Private Methods

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

        #region ScanComplete
        private void OnScanComplete(poshsecframework.Network.ScanEventArgs e)
        {
            EventHandler<poshsecframework.Network.ScanEventArgs> handler = ScanComplete;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion

        #endregion

        #region Public Properties
        public frmMain ParentForm
        {
            set { frm = value; }
        }
        #endregion
    }
}
