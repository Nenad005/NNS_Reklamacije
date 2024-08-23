


namespace NNS_Reklamacije.Converters
{
    public class BoolToVisibility : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool noText = (bool)values[0];
            bool isFocused = (bool)values[1];

            if (!noText || isFocused) return Visibility.Collapsed;
            return Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
