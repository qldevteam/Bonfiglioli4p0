using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Bonfiglioli4p0
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        static string _strPCName;
        public static string strPCName
        {
            get { return _strPCName; }

            set
            {
                _strPCName = value;
            }
        }

        public static bool IsIDE()
        {
            if (System.Diagnostics.Debugger.IsAttached)

                // Since there is a debugger attached,

                // assume we are running from the IDE
                return true;
            else

                // Assume we aren't running from the IDE
                return false;

        }
    }
}



