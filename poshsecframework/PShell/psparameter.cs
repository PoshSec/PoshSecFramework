using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace poshsecframework.PShell
{
    public class psparameter
    {
        #region " Public Properties "

        #region " Parameter "
        [Category("Parameters"),
        Browsable(true),
        ReadOnly(false),
        Bindable(false),
        DesignOnly(false)]
        public String Name { get; set; }
        public String Description { get; set; }
        public String Category { get; set; }
        public Object Value { get; set; }
        public Object DefaultValue { get; set; }
        Type type;

        public Type Type
        {
            get { return type; }
            set
            {
                type = value;
            }
        }
        #endregion

        #endregion
    }
}
