using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNS_Reklamacije.Converters
{
    public class DodatoPre : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan prosloVreme = DateTime.Now.Subtract((DateTime)value);

            if (prosloVreme.Days > 0) return $"{prosloVreme.Days} d";
            if (prosloVreme.Hours > 0) return $"{prosloVreme.Hours} h";
            if (prosloVreme.Minutes > 0) return $"{prosloVreme.Minutes} m";
            return $"{prosloVreme.Seconds} s";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
