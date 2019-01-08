using Bonfiglioli4p0;
using Bonfiglioli4p0.Utilities;
using Bonfiglioli4p0.ViewModel;
using qFluid.Utilities;
using System;
using System.Collections.Generic;
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
    public partial class mySettings : Window
    {
        public mySettings()
        {
            InitializeComponent();

            wpFields.Children.Clear();
            spNote.Children.Clear();

            Serializer.DeSerializeXAML(wpFields.Children, AppContext.BaseDirectory + "mySettings.xml");
            Serializer.DeSerializeXAML(spNote.Children, AppContext.BaseDirectory + "mySettingsNote.xml");

            var viewModel = new mySettingsMvvm();
            this.DataContext = viewModel;

            //Chiusura window in mvvm
            //http://jkshay.com/closing-a-wpf-window-using-mvvm-and-minimal-code-behind/
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(() => this.Close());


        }

        private void Window_Closed(object sender, EventArgs e)
        {
            //TODO da abilitare in definitiva
            if (!App.IsIDE())
            {
                Serializer.SerializeToXAML(wpFields.Children, AppContext.BaseDirectory + "mySettings.xml");
                Serializer.SerializeToXAML(spNote.Children, AppContext.BaseDirectory + "mySettingsNote.xml");
            }
            else
            {
                Serializer.SerializeToXAML(wpFields.Children, AppContext.BaseDirectory + "mySettings.xml");
                Serializer.SerializeToXAML(spNote.Children, AppContext.BaseDirectory + "mySettingsNote.xml");
            }
        }

    }
}
