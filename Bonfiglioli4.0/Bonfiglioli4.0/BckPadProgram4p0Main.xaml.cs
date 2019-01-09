using PadProgram4p0.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PadProgram4p0
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public static PadProgram4p0Main Self;
        public static string nrPr;
        public SerializedData seda = new SerializedData();

        public Window2()
        {
            InitializeComponent();
        }


        private void tbwHeader_LostFocus(object sender, RoutedEventArgs e)
        {
            seda.s00 = tbwH00.Text;
            seda.s01 = tbwH200.Text;
            seda.s02 = tbwH400.Text;

            SerializeData();
        }

        private SerializedData DeSerializeData()
        {

            //SerializedData mp = null;

            //string ss = Environment.GetFolderPath(Environment.CurrentDirectory);

            Stream stream = File.Open(Environment.CurrentDirectory + "\\SerialData.osl", FileMode.Open);
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

            SerializedData mp = (SerializedData)bFormatter.Deserialize(stream);
            stream.Close();

            return mp;
        }

        private void SerializeData()
        {
            //string ss = Environment.GetFolderPath(Environment.CurrentDirectory);

            System.IO.Stream stream = File.Open(Environment.CurrentDirectory + "\\SerialData.osl", FileMode.Create);
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

            bFormatter.Serialize(stream, seda);
            stream.Close();
        }

        private void window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
