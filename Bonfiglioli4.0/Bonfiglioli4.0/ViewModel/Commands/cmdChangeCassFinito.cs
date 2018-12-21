using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bonfiglioli4p0.ViewModel.Commands
{
    public class cmdChangeCassFinito : ICommand
    {
        private Bonfiglioli4p0MWVM _viewModel;

        public cmdChangeCassFinito(Bonfiglioli4p0MWVM viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        public bool CanExecute(object parameter)
        {
            return true;
        }


        public void Execute(object parameter)
        {
            //    if (App.Usr.bUAdmin)
            //    {
            //        int yui = ((qFluid.Entity.StatiLavCluster)parameter).StatiLavClusterID;
            //        _viewModel.vCmdChangeCassFinito(yui);
            //    }
        }
    }
}