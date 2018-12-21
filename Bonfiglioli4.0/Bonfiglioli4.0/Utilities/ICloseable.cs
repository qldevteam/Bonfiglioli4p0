//https://www.codeproject.com/Articles/413517/Closing-Windows-and-Applications-with-WPF-and-MVVM

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qFluid.Utilities
{
    interface ICloseable
    {
        event EventHandler<EventArgs> RequestClose;
    }
}
