using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Diagnostics;
using System.Windows.Controls;
//using System.Collections.Generic;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Media;
using Bonfiglioli4p0.Utilities;

namespace qFluid.Utilities
{
    public static class FormSerialisor
    {
        /*
         * Drop this class into your project, and add the following line at the top of any class/form that wishes to use it...
           using FormSerialisation;
           To use the code, simply call FormSerialisor.Serialise(FormOrControlToBeSerialised, FullPathToXMLFile)
         * 
         * For more details, see http://www.codeproject.com/KB/dialog/SavingTheStateOfAForm.aspx
         * 
         * Last updated 13th June '10 to account for the odd behaviour of the two Panel controls in a SplitContainer (see the article)
         */
        static int ddd = 0;
        public static void Serialise(Window c, string XmlFileName)
        {
            XmlTextWriter xmlSerialisedForm = new XmlTextWriter(XmlFileName, System.Text.Encoding.Default);
            xmlSerialisedForm.Formatting = Formatting.Indented;
            xmlSerialisedForm.WriteStartDocument();
            xmlSerialisedForm.WriteStartElement("ChildForm");

            //// enumerate all controls on the form, and serialise them as appropriate
            //Window window = this;
            Window window = c;
            foreach (Image img in window.FindChildren<Image>())
            {
                Console.WriteLine("Image source: " + img.Source);
                AddChildControls(xmlSerialisedForm, c);

                xmlSerialisedForm.WriteEndElement(); // ChildForm
                xmlSerialisedForm.WriteEndDocument();
                xmlSerialisedForm.Flush();
                xmlSerialisedForm.Close();

            }


            if (ddd >= 5)
            {
                xmlSerialisedForm.WriteEndElement(); // ChildForm
                xmlSerialisedForm.WriteEndDocument();
                xmlSerialisedForm.Flush();
                xmlSerialisedForm.Close();
            }
            ddd += 1;
        }


        public static void Deserialise(Control c, string XmlFileName)
        {
            if (File.Exists(XmlFileName))
            {
                XmlDocument xmlSerialisedForm = new XmlDocument();
                xmlSerialisedForm.Load(XmlFileName);
                XmlNode topLevel = xmlSerialisedForm.ChildNodes[1];
            }
        }

        private static void AddChildControls(XmlTextWriter xmlSerialisedForm, Control c)
        {
            Control childCtrl = c;
            //foreach (Control childCtrl in c.Controls)
            //{
            //if (!(childCtrl is Label))
            if ((childCtrl is Label))
            {
                // serialise this control
                xmlSerialisedForm.WriteStartElement("Control");
                xmlSerialisedForm.WriteAttributeString("Type", childCtrl.GetType().ToString());
                xmlSerialisedForm.WriteAttributeString("Name", childCtrl.Name);
                if (childCtrl is TextBox)
                {
                    xmlSerialisedForm.WriteElementString("Text", ((TextBox)childCtrl).Text);
                }
                else if (childCtrl is ComboBox)
                {
                    xmlSerialisedForm.WriteElementString("Text", ((ComboBox)childCtrl).Text);
                    xmlSerialisedForm.WriteElementString("SelectedIndex", ((ComboBox)childCtrl).SelectedIndex.ToString());
                }
                else if (childCtrl is ListBox)
                {
                    // need to account for multiply selected items
                    ListBox lst = (ListBox)childCtrl;
                    if (lst.SelectedIndex == -1)
                    {
                        xmlSerialisedForm.WriteElementString("SelectedIndex", "-1");
                    }
                    else
                    {
                        for (int i = 0; i < lst.SelectedItems.Count; i++)
                        {
                            xmlSerialisedForm.WriteElementString("SelectedIndex", (lst.SelectedItems[i].ToString()));
                        }
                    }
                }
                else if (childCtrl is CheckBox)
                {
                    xmlSerialisedForm.WriteElementString("Checked", ((CheckBox)childCtrl).IsChecked.ToString());
                }
                // this next line was taken from http://stackoverflow.com/questions/391888/how-to-get-the-real-value-of-the-visible-property
                // which dicusses the problem of child controls claiming to have Visible=false even when they haven't, based on the parent
                // having Visible=true
                //bool visible = (bool)typeof(Control).GetMethod("GetState", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(childCtrl, new object[] { 2 });
                //xmlSerialisedForm.WriteElementString("Visible", visible.ToString());

                xmlSerialisedForm.WriteEndElement(); // Control
            }
        }
        //}


    }
}
