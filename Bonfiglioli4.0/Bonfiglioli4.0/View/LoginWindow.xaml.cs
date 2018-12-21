//using Uniconta.Common;
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
//using Bonfiglioli4p0.DialogControls;
//using Bonfiglioli4p0.Entity;

namespace Bonfiglioli4p0.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        //https://social.technet.microsoft.com/wiki/contents/articles/20719.wpf-displaying-and-editing-many-to-many-relational-data-in-a-datagrid.aspx
        // Here you will need to insert your own valid App id, obtained at your Uniconta partner or at Uniconta directly!
        static Guid DemoConsoleGuid = new Guid("10000000-0000-0000-0000-000000000000");
        public static bool bButtonCancel; 

        public LoginWindow()
        {
            InitializeComponent();
            this.loginCtrl.loginButton.Click += loginButton_Click;
            this.loginCtrl.CancelButton.Click += CancelButton_Click;

        }

        public void myShow()
        {
            this.ShowDialog();
        }

        void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;

            bButtonCancel = true;

            this.Hide();
        }

        void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string password = loginCtrl.Password;
            string userName = loginCtrl.UserName;


            //using (ModelQFluid _cntx = new ModelQFluid())
            //{
            //    //string sdfwsdz = _cntx.Utentis.Where(x => x.Usr == userName).Select(x => x.psw).FirstOrDefault();
            //    App.Usr = _cntx.Utentis.Where(x => x.Usr == userName).FirstOrDefault();
            //    if (App.Usr == null)
            //    {
            //        MessageBox.Show("Utente non riconosciuto \r\nL'applicazione verrà chiusa", "User");
            //        try
            //        {
            //            App.Current.Shutdown();

            //        }
            //        catch (Exception ee)
            //        {
            //            string ss = ee.Message;
            //            throw;
            //        }
            //    }

            //    try
            //    {
            //        string st = _cntx.Utentis.Where(x => x.Usr == userName).Select(x => x.psw).FirstOrDefault().ToString();
            //        if (password != st)
            //        {
            //            Xceed.Wpf.Toolkit.MessageBox.Show("Password errata \r\nL'applicazione verrà chiusa", "Password");
            //            App.Current.Shutdown();
            //        }

            //    }
            //    catch (Exception ee)
            //    {
            //        string ss = ee.Message;
            //        throw;
            //    }
            //    this.Hide();

            //    App.CurUsr = userName;

            //    if(_cntx.Utentis.Where(x => x.Usr != App.CurUsr).Select(x => x.bUAdmin).FirstOrDefault() == null)
            //            Xceed.Wpf.Toolkit.MessageBox.Show("Nome utente non trovato !", "Utenti");

            //    if (_cntx.Utentis.Where(x => x.bUUser == true).Select(x => x.bUAdmin).FirstOrDefault() == null)
            //        Xceed.Wpf.Toolkit.MessageBox.Show("Nessun 'user' abilitato !", "Utenti");

            //    if (_cntx.Utentis.Where(x => x.bUUser == true).Select(x => x.bUAdmin).ToList().Count() > 1)
            //        Xceed.Wpf.Toolkit.MessageBox.Show("Abilitazione doppia di utente : 'user' !", "Utenti");

            //    if (_cntx.Utentis.Where(x => x.bUAdmin == true).Select(x => x.bUAdmin).FirstOrDefault() == null)
            //        Xceed.Wpf.Toolkit.MessageBox.Show("Nessun 'admin' abilitato !", "Utenti");

            //    if (_cntx.Utentis.Where(x => x.bUAdmin == true).Select(x => x.bUAdmin).ToList().Count() > 1)
            //        Xceed.Wpf.Toolkit.MessageBox.Show("Abilitazione doppia di utente : 'admin' !", "Utenti");

            //    if (_cntx.Utentis.Where(x => x.bUPower == true).Select(x => x.bUAdmin).FirstOrDefault() == null)
            //        Xceed.Wpf.Toolkit.MessageBox.Show("Nessun 'PowerUser' abilitato !", "Utenti");

            //    if (_cntx.Utentis.Where(x => x.bUPower == true).Select(x => x.bUAdmin).ToList().Count() > 1)
            //        Xceed.Wpf.Toolkit.MessageBox.Show("Abilitazione doppia di utente : 'Power User' !", "Utenti");


            //    bool jhhj = _cntx.Utentis.Where(x => x.Usr != userName).Select(x => x.bUAdmin).FirstOrDefault();
            //    while (_cntx.Utentis.Where(x => x.Usr != userName).Select(x => x.bUAdmin).FirstOrDefault())
            //    {
            //        System.Threading.Thread.Sleep(30);
            //    }

            //    return;
            //}
        }

        //private async Task<ErrorCodes> SetLogin(string username, string password)
        //{
        //    if (!ValidateUserCredentials(username, password))
        //        return ErrorCodes.NoSucces;

        //    try
        //    {
        //        var ses = DemoInitializer.GetSession();
        //        return await ses.LoginAsync(username, password, Uniconta.Common.User.LoginType.API, DemoConsoleGuid, Uniconta.ClientTools.Localization.InititalLanguageCode);
        //    }
        //    catch
        //    {
        //        MessageBox.Show("System Exception. Application Will Close.", "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //        this.Close();
        //        return ErrorCodes.Exception;
        //    }
        //}

        private bool ValidateUserCredentials(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                return true;

            return false;
        }
    }
}
