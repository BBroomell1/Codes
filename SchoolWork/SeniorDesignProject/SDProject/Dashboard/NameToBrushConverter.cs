using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Dashboard
{
    class NameToBrushConverter : IMultiValueConverter
    {
        // changing the color of individual cells
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value[0].GetType() == typeof(string))
            {
                // if the cell value is P and the column header is 'outstat' OR if cell value is NP OR RP, then color is dark green
                if ((((string)value[1] == QueryLib.Outstat) && ((string)value[0] == "P")) || (string)value[0] == "NP" || (string)value[0] == "RP")
                {
                    // color is a dark green: "#03C04A"
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#03C04A")); ;
                }
                // if cell value is F OR NF OR RF, then the cell color is changed to a dark red
                else if ((((string)value[1] == QueryLib.Outstat) && ((string)value[0] == "F")) || ((string)value[0] == "NF") || ((string)value[0] == "RF"))
                {
                    // color is dark red: "#FF5555"
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF5555")); ;
                }
                // if (column header is InStat and cell value is N) OR (column header is Instat and cell value is N)
                // Note: repeated same thing two ways to account for differences in capitalization of word "Instat"
                else if ((((string)value[1] == QueryLib.Instat) && ((string)value[0] == "N")) || (((string)value[1] == QueryLib.Instat) && ((string)value[0] == "N")))
                {
                    // if true, assign color value "#91E6F0"
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#91E6F0")); ;
                }
                // if instat is R, assign cell color to be "#DAAFF4"
                else if ((((string)value[1] == QueryLib.Instat) && ((string)value[0] == "R")) || (((string)value[1] == QueryLib.Instat) && ((string)value[0] == "R")))
                {
                    return (SolidColorBrush)(new BrushConverter().ConvertFrom("#DAAFF4")); ;
                }
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        
    }
}
