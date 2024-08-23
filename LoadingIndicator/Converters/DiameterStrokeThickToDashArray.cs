using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace LoadingIndicator.Converters
{
    public class DiameterStrokeThickToDashArray : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double diameter = (double)values[0];
            double thickness = (double)values[1];

            double obim = diameter * Math.PI;

            double prazno = obim * 0.25;
            double puno = obim * 0.75;

            return new DoubleCollection(new[] { puno/thickness, prazno/thickness}); 
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
