using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace poshsecframework.PShell
{
    [TypeConverter(typeof(psparamtype.psparamobject))]
    public class psparamtype
    {
        [Category("Standard")]
        private readonly List<psparameter> psparams = new List<psparameter>();

        [Browsable(false)]
        public List<psparameter> Properties
        {
            get { return psparams; }
        }

        private Dictionary<string,object> values = new Dictionary<string,object>();

        public object this[String name]
        {
            get 
            { 
                object val; 
                values.TryGetValue(name, out val); 
                return val; 
            }
            set
            {
                values.Remove(name);
            }
        }

        private class psparamobject : ExpandableObjectConverter
        {
            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                var props = base.GetProperties(context, value, attributes);
                psparamtype parm = value as psparamtype;
                List<psparameter> psprops = null;
                int propcnt = props.Count;
                if (parm != null)
                {
                    psprops = parm.Properties;
                    propcnt += psprops.Count;
                }
                PropertyDescriptor[] psdescs = new PropertyDescriptor[propcnt];
                props.CopyTo(psdescs, 0);

                if (psprops != null)
                {
                    int idx = props.Count;
                    foreach (psparameter psparam in psprops)
                    {
                        psdescs[idx++] = new psparamdescriptor(psparam);
                    }
                }
                return new PropertyDescriptorCollection(psdescs);
            }
        }

        private class psparamdescriptor : PropertyDescriptor
        {
            private readonly psparameter psparam;

            public psparamdescriptor(psparameter psparam) : base(psparam.Name, null)
            {
                this.psparam = psparam;
            }

            public override string Category 
            {
                get { return psparam.Category; } 
            }

            public override string Description 
            { 
                get { return psparam.Description; } 
            }

            public override string Name 
            { 
                get { return psparam.Name; } 
            }

            public override bool ShouldSerializeValue(object component) 
            { 
                return ((psparamtype)component)[psparam.Name] != null; 
            }

            public override void ResetValue(object component) 
            { 
                ((psparamtype)component)[psparam.Name] = null; 
            }

            public override bool IsReadOnly 
            { 
                get { return false; } 
            }
            
            public override Type PropertyType 
            { 
                get { return psparam.Type; } 
            }
            
            public override bool CanResetValue(object component) 
            { 
                return true; 
            }
            
            public override Type ComponentType 
            { 
                get { return typeof(psparamtype); }
            }
            
            public override void SetValue(object component, object value) 
            {
                psparam.Value = value; 
            }
            
            public override object GetValue(object component) 
            {
                return psparam.Value ?? psparam.DefaultValue ; 
            }
        }
    }
}
