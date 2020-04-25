using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Xml;
using System.Text;
using System.Windows.Forms;
using PoshSec.Framework.Enums;

namespace PoshSec.Framework.Utility
{
    class ExportObject
    {
        public void XML(System.Object[] CustomObject, String filename)
        {
            try
            {
                if (CustomObject != null && CustomObject.Count() > 0)
                {
                    if (CustomObject[0].GetType() == typeof(PSObject))
                    {
                        StreamWriter outfile = File.CreateText(filename);
                        XmlTextWriter xmlwtr = new XmlTextWriter(outfile);
                        xmlwtr.Formatting = Formatting.Indented;
                        xmlwtr.WriteStartDocument();
                        xmlwtr.WriteStartElement("Collection");
                        foreach (PSObject pobj in CustomObject)
                        {
                            xmlwtr.WriteStartElement("PSObject");
                            foreach (PSPropertyInfo prop in pobj.Properties)
                            {
                                if (prop != null)
                                {
                                    xmlwtr.WriteStartElement("PSNoteProperty");
                                    xmlwtr.WriteAttributeString("Name", prop.Name);
                                    try
                                    {
                                        xmlwtr.WriteAttributeString("Value", (prop.Value ?? "").ToString());
                                    }
                                    catch (GetValueException)
                                    {
                                        xmlwtr.WriteAttributeString("Value", "");
                                    }                                    
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
                }                
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

        public void CSV(System.Object[] CustomObject, String filename)
        {
            ExportDelim(CustomObject, filename, ",");
        }

        public void TXT(System.Object[] CustomObject, String filename)
        {
            ExportDelim(CustomObject, filename, "\t");
        }

        private void ExportDelim(System.Object[] CustomObject, String filename, String Delimiter)
        {
            try
            {
                if (CustomObject != null && CustomObject.Count() > 0)
                {
                    if (CustomObject[0].GetType() == typeof(PSObject))
                    {
                        StreamWriter outfile = File.CreateText(filename);
                        int idx = -1;
                        foreach (PSObject pobj in CustomObject)
                        {
                            idx++;
                            if (idx == 0)
                            {
                                //Header Row
                                String header = "";
                                foreach (PSPropertyInfo prop in pobj.Properties)
                                {
                                    if (prop != null)
                                    {
                                        header += prop.Name + Delimiter;
                                    }
                                }
                                if (header.EndsWith(Delimiter))
                                {
                                    header = header.Substring(0, header.Length - 1);
                                }
                                outfile.WriteLine(header);
                                outfile.Flush();
                            }
                            String value = "";
                            foreach (PSPropertyInfo prop in pobj.Properties)
                            {
                                if (prop != null)
                                {
                                    try
                                    {
                                        value += prop.Value + Delimiter;
                                    }
                                    catch (GetValueException)
                                    {
                                        value += "" + Delimiter;
                                    }                                    
                                }
                            }
                            if (value.EndsWith(Delimiter))
                            {
                                value = value.Substring(0, value.Length - 1);
                            }
                            outfile.WriteLine(value);
                            outfile.Flush();
                        }

                        outfile.Flush();
                        outfile.Close();
                        outfile = null;
                    }                    
                }
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
    }
}
