using Bonfiglioli4p0;
using Bonfiglioli4p0.Utilities;
using Bonfiglioli4p0.ViewModel;
using nsUtils;
using qFluid.Utilities;
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

namespace Bonfiglioli4p0
{
    /// <summary>
    /// Interaction logic for Bonfiglioli4p0Main.xaml
    /// </summary>
    public partial class Bonfiglioli4p0Main : Window
    {
        public static Bonfiglioli4p0Main Self;
        public static string nrPr;
        public SerializedData seda = new SerializedData();

        public Bonfiglioli4p0Main()
        {
            InitializeComponent();

            wpFields.Children.Clear();
            wpFields1.Children.Clear();
            wpFields2.Children.Clear();
            Serializer.DeSerializeXAML(wpFields.Children, AppContext.BaseDirectory + "MainGrid.xml");
            Serializer.DeSerializeXAML(wpFields1.Children, AppContext.BaseDirectory + "MainGrid1.xml");
            Serializer.DeSerializeXAML(wpFields2.Children, AppContext.BaseDirectory + "MainGrid2.xml");

            Self = this;

            seda = DeSerializeData();

            tbwH00.Text= seda.s00;
            tbwH200.Text = seda.s01;
            tbwH400.Text = seda.s02;

            var viewModel = new Bonfiglioli4p0MainMVVM();
            this.DataContext = viewModel;

            foreach (var item in wpFields.Children)
            {
                   if(item is TextBox)
                {
                    if (item.ToString()=="")
                    {

                    }
                }
            }

            //Chiusura window in mvvm
            //http://jkshay.com/closing-a-wpf-window-using-mvvm-and-minimal-code-behind/
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(() => this.Close());
        }

        //private void Window_Closed(object sender, EventArgs e)
        //{
        //    //TODO da abilitare in definitiva
        //    if (!App.IsIDE())
        //    {
        //        SerializeData();

        //        mySer();
        //    }
        //    else
        //    {
        //        SerializeData();
        //        //mySer();
        //    }

        //}

        public void mySer()
        {
            try
            {
                Serializer.SerializeToXAML(wpFields.Children, AppContext.BaseDirectory + "MainGrid.xml");
                Serializer.SerializeToXAML(wpFields1.Children, AppContext.BaseDirectory + "MainGrid1.xml");
                Serializer.SerializeToXAML(wpFields2.Children, AppContext.BaseDirectory + "MainGrid2.xml");

            }
            catch (Exception)
            {

            }
        }

        public void myDeser(int p_C, string p_file)
        {
            int rey = 0;
            string asas;

            rey = p_file.LastIndexOf("_");
            asas = p_file.Substring(rey + 1, 3);

            if (!int.TryParse(asas, out rey))
            {
                return;
            }

            try
            {
                switch (p_C)
                {
                    case 0:
                        wpFields.Children.Clear();
                        Serializer.DeSerializeXAML(wpFields.Children, p_file);

                        tbwH00.Text = rey.ToString();
                        break;
                    case 1:
                        wpFields1.Children.Clear();
                        Serializer.DeSerializeXAML(wpFields1.Children, p_file);

                        tbwH200.Text = rey.ToString();
                        break;
                    case 2:
                        wpFields2.Children.Clear();
                        Serializer.DeSerializeXAML(wpFields2.Children, p_file);

                        tbwH400.Text = rey.ToString();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {

            }
        }
        
        public void mySerMain(int p_N,string path_)
        {
            switch (p_N)
            {
                case 0:
                    Serializer.SerializeToXAML(wpFields.Children, path_);
                    break;
                case 1:
                    Serializer.SerializeToXAML(wpFields1.Children, path_);
                    break;
                case 2:
                    Serializer.SerializeToXAML(wpFields2.Children, path_);
                    break;
                default:
                    break;
            }
        }

        public void mySerMain1(string path_)
        {
            Serializer.SerializeToXAML(wpFields1.Children, path_);
        }

        public void mySerMain2(string path_)
        {
            Serializer.SerializeToXAML(wpFields2.Children, path_);
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

    [Serializable()]
    public class SerializedData
    {

        //tbwH00
        public string s00;
        //tbwH200
        public string s01;
        //tbwH400
        public string s02;
    }
}
