using qFluid.DialogControls;
using qFluid.Entity;
using qFluid.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace qFluid.ViewModel.Commands
{
    public class SimPrelNoPz : ICommand
    {
        private readonly qFluidMWViewModel _viewModel;

        public SimPrelNoPz(qFluidMWViewModel viewModel)
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
            _viewModel.vSimPrelNoPz();
        }
    }
}

