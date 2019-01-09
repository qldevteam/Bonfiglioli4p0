using PadProgram4p0.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadProgram4p0.ViewModel.Base
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Protected Methods

        /// <summary>
        /// Fires the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the changed property.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        #endregion

    }
}
