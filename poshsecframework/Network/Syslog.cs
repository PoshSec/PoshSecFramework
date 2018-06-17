/* This class was rewritten using syslog.cs, written by Michiel Fortuin, as
 * a template. While Michiel's code is not directly in this class, I wanted to
 * recognize Michiel for giving me the base template to write this class.
 * 
 * Michiel Fortuin's syslog.cs blog post: http://blog.micfort.org/2011/06/syslog-c.html
 * 
 * */
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using PoshSec.Framework.Strings;

namespace PoshSec.Framework.Network
{
    class Syslog
    {
        private UdpClient slclient = null;
        private IPEndPoint server;

        private enum SyslogLevel
        {
            Information=6,
            Error=3,
            Warning=4,
            Severe=2,
            Critical=1
        }

        public Syslog(IPEndPoint Server)
        {
            server = Server;
            slclient = new UdpClient();
        }

        public void Close()
        {
            slclient.Close();
        }

        public void SendMessage(PShell.psmethods.PSAlert.AlertType AlertLevel, String Source, String Message)
        {
            int level = (int)GetLevel(AlertLevel) + (20 * 8); //Local4.(AlertLevel)
            string priority = String.Format(StringValue.PriorityFormat, level);
            string timestamp = DateTime.Now.ToString(StringValue.SyslogTimeFormat);
            string hostname = Dns.GetHostName();
            string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            List<byte> msg = new List<byte>();
            string msgstring = String.Format(StringValue.SyslogFormat, priority, timestamp, hostname, StringValue.psftitle.Replace(" ", ""), Source, Message);
            msg.AddRange(Encoding.ASCII.GetBytes(msgstring));
            SendMessage(msg.ToArray());
        }

        private void SendMessage(byte[] data)
        {
            try
            {
                slclient.Send(data, data.Length, server);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private SyslogLevel GetLevel(PShell.psmethods.PSAlert.AlertType level)
        {
            SyslogLevel rtn = SyslogLevel.Information;
            bool found = false;
            int idx = 0;
            int[] sidx = { 1, 2, 3, 4, 6 };
            do
            {
                SyslogLevel lvl = (SyslogLevel)sidx[idx];
                if (lvl.ToString() == level.ToString())
                {
                    found = true;
                    rtn = lvl;
                }
                idx++;
            } while (!found && idx < sidx.Length);
            return rtn;
        }
    }
}
