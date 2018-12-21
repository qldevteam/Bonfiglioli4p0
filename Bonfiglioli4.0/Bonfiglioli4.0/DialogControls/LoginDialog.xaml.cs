using Bonfiglioli4._0;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bonfiglioli4p0.DialogControls
{
    /// <summary>
    /// Interaction logic for LoginDialog.xaml
    /// </summary>
    public partial class LoginDialog : UserControl
    {
        public string UserName
        {
            get { return txtUserName.Text; }
        }

        public string Password
        {
            get { return txtPassword.Password; }
        }

        public LoginDialog()
        {
            InitializeComponent();

            if (App.strPCName.ToUpper() == "D-QFLUIDO01")
            {
                txtUserName.Text = "admin";
                txtPassword.Password = "admin";
            }
            else
            {
                if (App.IsIDE())
                {
                    txtUserName.Text = "admin";
                    txtPassword.Password = "admin1";
                }
                else
                {

                txtUserName.Text = "poweruser";
                txtPassword.Password = "";
                }
            }
        }
    }
}
