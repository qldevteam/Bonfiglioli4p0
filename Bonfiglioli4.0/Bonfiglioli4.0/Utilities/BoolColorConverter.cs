using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace qFluid.Utilities
{
    public class BoolColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var numValue = (bool)value;
            return numValue ? "#5BD700" : "#FF6A00";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }

}
