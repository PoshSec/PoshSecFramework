using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace poshsecframework.PShell
{
    class psfilenameeditor : System.Drawing.Design.UITypeEditor
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
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Title = "Select " + context.PropertyDescriptor.DisplayName;
                dlg.FileName = (string)value;
                dlg.Filter = "All Files (*.*)|*.*";
                dlg.CheckFileExists = false;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    value = dlg.FileName;
                }
                dlg.Dispose();
                dlg = null;
                return value;
            }            
        }
    }
}
