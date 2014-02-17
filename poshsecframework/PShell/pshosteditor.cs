using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Windows.Forms;

namespace poshsecframework.PShell
{
    class pshosteditor : System.Drawing.Design.UITypeEditor
    {
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return System.Drawing.Design.UITypeEditorEditStyle.Modal;
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
                Object x = provider;
                Interface.frmNetworkBrowser nb = new Interface.frmNetworkBrowser();
                if (nb.ShowDialog() == DialogResult.OK)
                {
                    value = nb.SerializedHosts;
                }
                nb = null;               
                return value;
            }
        }
    }
}
