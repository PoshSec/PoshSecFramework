using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Xml;
using System.Text;
using System.Windows.Forms;

namespace poshsecframework.Utility
{
    class ExportObject
    {
        public void XML(Collection<PSObject> CustomObject, String filename)
        {
            try
            {
                StreamWriter outfile = File.CreateText(filename);
                XmlTextWriter xmlwtr = new XmlTextWriter(outfile);
                xmlwtr.Formatting = Formatting.Indented;
                xmlwtr.WriteStartDocument();
                xmlwtr.WriteStartElement("Collection");
                foreach(PSObject pobj in CustomObject)
                {
                    xmlwtr.WriteStartElement("PSObject");
                    foreach (PSNoteProperty prop in pobj.Properties)
                    {
                        if (prop != null)
                        {
                            xmlwtr.WriteStartElement("PSNoteProperty");
                            xmlwtr.WriteAttributeString("Name", prop.Name);
                            xmlwtr.WriteAttributeString("Value", (prop.Value ?? "").ToString());
                            xmlwtr.WriteEndElement();
                        }
                    }
                    xmlwtr.WriteEndElement();
                }

                xmlwtr.WriteEndElement();
                xmlwtr.WriteEndDocument();
                xmlwtr.Flush();
                xmlwtr.Close();
                xmlwtr = null;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    MessageBox.Show("Unable to export object." + Environment.NewLine + e.Message + Environment.NewLine + e.InnerException.Message);
                }
                else
                {
                    MessageBox.Show("Unable to export object." + Environment.NewLine + e.Message);
                }
            }
        }

        public void CSV(Collection<PSObject> CustomObject, String filename)
        {

        }

        public void TXT(Collection<PSObject> CustomObject, String filename)
        {

        }
    }
}
