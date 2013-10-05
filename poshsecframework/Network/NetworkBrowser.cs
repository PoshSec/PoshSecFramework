using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Net;
using System.Text;
using System.Windows.Forms;
using psframework.Structures;

namespace psframework.Network
{
    class NetworkBrowser
    {
        #region Private Variables
        frmMain frm = null;
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
            ArrayList systems = new ArrayList();
            DirectoryEntry hostPC = new DirectoryEntry();
            hostPC.Path = "WinNT://" + domain;

            foreach (DirectoryEntry netPC in hostPC.Children)
            {
                if (netPC.Name != "Schema" && netPC.SchemaClassName == "Computer")
                {
                    systems.Add(netPC);                    
                }
            }

            return systems;
        }

        public ArrayList ScanbyIP()
        {
            ArrayList systems = new ArrayList();
            String localIP = GetIP(Dns.GetHostName());

            if (localIP != "" && localIP != null)
            {
                String[] ipparts = localIP.Split('.');
                if (ipparts != null && ipparts.Length == 4)
                {
                    frm.SetProgress(0, 255);
                    int ip = 1;
                    do
                    {
                        String host = ipparts[0] + "." + ipparts[1] + "." + ipparts[2] + "." + ip.ToString();
                        frm.SetStatus("Scanning " + host + ", please wait...");
                        frm.SetProgress(ip, 255);
                        
                        if (Ping(host, 1, 100))
                        {
                            systems.Add(host);
                            Application.DoEvents();
                        }
                        ip++;
                    } while(ip < 255 && !frm.CancelIPScan);
                }
            }

            frm.HideProgress();
            frm.SetStatus("Ready");

            return systems;
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

        #region Public Properties
        public frmMain ParentForm
        {
            set { frm = value; }
        }
        #endregion
    }
}
