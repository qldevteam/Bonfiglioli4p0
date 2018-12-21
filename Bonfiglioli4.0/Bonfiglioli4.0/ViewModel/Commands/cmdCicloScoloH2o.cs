using qFluid.DialogControls;
using qFluid.Entity;
using qFluid.Presentation;
using qFluid.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace qFluid.ViewModel.Commands
{
    internal class cmdCicloScoloH2o : ICommand
    {
        private AnagraficaViewModel _viewModel;


        internal cmdCicloScoloH2o(AnagraficaViewModel viewModel)
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
            var yui = _viewModel.SelectedItemQeAnagrafica;

            _viewModel.vCicloScoloH2o(yui.qeAnagraficaID);
        }
    }
}

