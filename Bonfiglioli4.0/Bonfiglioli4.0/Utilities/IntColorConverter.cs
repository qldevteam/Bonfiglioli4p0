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
    public class IntColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //var numValue = (int)value;
            //return numValue < 350 ? Brushes.Green : Brushes.Red;
            var numValue = (bool)value;
            return numValue ? "#5BD700" : "#FF6A00";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
        //    public class InLavoroColorConverter : IValueConverter
        //    {
        //        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        //        {
        //            string colVal;

        //            string val = value?.ToString();
        //            switch (val)
        //            {
        //                case "ROB1":
        //                    colVal = "Blue";
        //                    break;
        //                case "VER1":
        //                    colVal = "DarkViolet";
        //                    break;
        //                default:
        //                    colVal = "LightGray";
        //                    break;
        //            }
        //            return colVal;
        //        }

        //        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
        //    }

        //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        //    {
        //        var sValue = (bool)value;
        //        return sValue ? "Bold" : "Light";
        //    }

        //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
        //}

}
