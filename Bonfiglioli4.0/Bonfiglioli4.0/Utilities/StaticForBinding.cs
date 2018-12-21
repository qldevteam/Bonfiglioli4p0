using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qFluid.Utilities
{
    /// <summary>
    /// Classe singleton necessaria per binding perchè non si può usare la statica con il binding
    /// </summary>
    public  class StaticForBinding : System.ComponentModel.INotifyPropertyChanged
    {
        private static StaticForBinding instance = null;

        private StaticForBinding()
        {
        }

        public static StaticForBinding Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StaticForBinding();
                }
                return instance;
            }
        }


         string _LabelMainBar;
        public  string LabelMainBar
        {
            get
            {
                return _LabelMainBar;
            }
            set
            {
                _LabelMainBar = value;
                OnPropertyChanged("LabelMainBar");
            }
        }


        static int _MainFirstRowHeight;
      
        public  int MainFirstRowHeight
        {
            get
            {
                return _MainFirstRowHeight;
            }
            set
            {
                _MainFirstRowHeight = value;
                //OnPropertyChanged("MainFirstRowHeight");
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
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

    }
}
