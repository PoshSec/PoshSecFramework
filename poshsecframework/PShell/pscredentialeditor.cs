using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Management.Automation;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace poshsecframework.PShell
{
    class pscredentialeditor : System.Drawing.Design.UITypeEditor
    {
        [DllImport("credui.dll", CharSet = CharSet.Auto)]
        private static extern int CredUIPromptForWindowsCredentials(ref CREDUI_INFO notUsedHere,
            int authError,
            ref uint authPackage,
            IntPtr InAuthBuffer,
            uint InAuthBufferSize,
            out IntPtr refOutAuthBuffer,
            out uint refOutAuthBufferSize,
            ref bool fSave,
            int flags);

        [DllImport("credui.dll", CharSet = CharSet.Auto)]
        private static extern bool CredUnPackAuthenticationBuffer(int dwFlags,
            IntPtr pAuthBuffer,
            uint cbAuthBuffer,
            StringBuilder pszUserName,
            ref int pcchMaxUserName,
            StringBuilder pszDomainName,
            ref int pcchMaxDomainame,
            StringBuilder pszPassword,
            ref int pcchMaxPassword);

        [DllImport("ole32.dll")]
        public static extern void CoTaskMemFree(IntPtr ptr);

        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct CREDUI_INFO
        {
            public int cbSize;
            public IntPtr hwndParent;
            public string pszMessageText;
            public string pszCaptionText;
            public IntPtr hbmBanner;
        }

        [RefreshProperties(System.ComponentModel.RefreshProperties.All)]
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context == null || context.Instance == null)
            {
                return base.EditValue(context, provider, value);
            }
            else
            {
                value = null;
                NetworkCredential creds = GetCredential();
                if (creds != null)
                {
                    System.Security.SecureString pwd = new System.Security.SecureString();
                    foreach(var c in creds.Password) 
                    {
                        pwd.AppendChar(c);
                    }
                    PSCredential rtncrds = new PSCredential(creds.UserName, pwd);
                    creds = null;
                    pwd = null;
                    GC.Collect();
                    value = rtncrds;
                }
                return value;
            }
        }

        public static NetworkCredential GetCredential()
        {
            CREDUI_INFO credui = new CREDUI_INFO();
            credui.pszCaptionText = "Please enter the credentials.";
            credui.pszMessageText = "DisplayedMessage";
            credui.cbSize = Marshal.SizeOf(credui);
            uint authPackage = 0;
            IntPtr outCredBuffer = new IntPtr();
            uint outCredSize;
            bool save = false;
            int result = CredUIPromptForWindowsCredentials(ref credui,0,ref authPackage,IntPtr.Zero,0,out outCredBuffer,out outCredSize,ref save,0x1);

            var usernameBuf = new StringBuilder(100);
            var passwordBuf = new StringBuilder(100);
            var domainBuf = new StringBuilder(100);

            int maxUserName = 100;
            int maxDomain = 100;
            int maxPassword = 100;
            NetworkCredential netcreds = null;
            if (result == 0)
            {
                if (CredUnPackAuthenticationBuffer(0, outCredBuffer, outCredSize, usernameBuf, ref maxUserName, domainBuf, ref maxDomain, passwordBuf, ref maxPassword))
                {
                    CoTaskMemFree(outCredBuffer);
                    netcreds = new NetworkCredential(usernameBuf.ToString(), passwordBuf.ToString(), domainBuf.ToString());
                }
            }

            return netcreds;
        }
    }
}
