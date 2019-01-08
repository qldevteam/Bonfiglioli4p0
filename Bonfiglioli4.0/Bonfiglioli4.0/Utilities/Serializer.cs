using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Bonfiglioli4p0.Utilities
{
    internal static class Serializer
    {
        // Serializes any UIElement object to XAML using a given filename.
        public static void SerializeToXAML(UIElementCollection elements, string filename)
        {
            // Use XamlWriter object to serialize element
            string strXAML = System.Windows.Markup.XamlWriter.Save(elements);

            // Write XAML to file. Use 'using' so objects are disposed of properly.
            using (System.IO.FileStream fs = System.IO.File.Create(filename))
            {
                using (System.IO.StreamWriter streamwriter = new System.IO.StreamWriter(fs))
                {
                    streamwriter.Write(strXAML);
                }
            }
        }

        public static void DeSerializeXAML(UIElementCollection elements, string filename)
        {
            var context = System.Windows.Markup.XamlReader.GetWpfSchemaContext();

            var settings = new System.Xaml.XamlObjectWriterSettings
            {
                RootObjectInstance = elements
            };

            using (var reader = new System.Xaml.XamlXmlReader(filename))
            using (var writer = new System.Xaml.XamlObjectWriter(context, settings))
            {
                try
                {
                    System.Xaml.XamlServices.Transform(reader, writer);
                }
                catch (Exception ee)
                {
                    string dd = ee.Message;
                }
            }
        }


    }
}
