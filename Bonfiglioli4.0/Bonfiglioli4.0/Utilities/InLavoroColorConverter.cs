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
    public class InLavoroColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string colVal;

            string val = value?.ToString();
            switch (val)
            {
                case "ROB1":
                    colVal = "Blue";
                    break;
                case "VER1":
                    colVal = "DarkViolet";
                    break;
                default:
                    colVal = "LightGray";
                    break;
            }
            return colVal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}
