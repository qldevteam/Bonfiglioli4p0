using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bonfiglioli4p0.ViewModel.Commands
{
    public class vcsOpenMySettings : ICommand
    {
        private Bonfiglioli4p0MainMVVM _viewModel;

        public vcsOpenMySettings(Bonfiglioli4p0MainMVVM viewModel)
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
            _viewModel.vcmdOpenMySettings();
        }
    }
}