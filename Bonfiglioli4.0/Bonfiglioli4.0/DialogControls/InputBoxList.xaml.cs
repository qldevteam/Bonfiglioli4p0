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

namespace Bonfiglioli4p0.DialogControls
{
    /// <summary>
    /// Interaction logic for InputBoxList.xaml
    /// </summary>
    public partial class InputBoxList : Window
    {
        public InputBoxList(string question, string defaultAnswer = "")
        {
            InitializeComponent();

            lblQuestion.Content = question;
            cmbAnswer.Text = defaultAnswer;
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            //txtAnswer.SelectAll();
            cmbAnswer.Focus();
        }

        public string Answer
        {
            get { return cmbAnswer.Text; }
        }

        private void btnDialogCanc_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
