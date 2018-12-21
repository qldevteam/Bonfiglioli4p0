using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qFluid.ViewModel.Base
{
    public interface IRequestCloseViewModel
    {
        event EventHandler RequestClose;
    }
}
