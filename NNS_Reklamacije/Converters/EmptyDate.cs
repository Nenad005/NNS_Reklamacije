


namespace NNS_Reklamacije.Converters
{
    public class EmptyDate : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is null) return values[1];
            try
            {
                DateTime datum = (DateTime)values[0];
                return datum.ToString("MMM/dd/yyyy");
            }
            catch (Exception)   
            {
            }
            return "greska";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
