using System;
using System.Windows;
using System.Windows.Data;

namespace ServiceСenter.Model
{
    
    [ValueConversion(typeof(string), typeof(Visibility))]
    public class IsNullofVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if ((string) value != String.Empty && value != null)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    [ValueConversion(typeof(int), typeof(Visibility))]
    public class IsNullIntofVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            int res;
             if (Int32.TryParse(value.ToString(), out res))
                 if(res == 0)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    [ValueConversion(typeof(int), typeof(bool))]
    public class SelectedIndexToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            int res;
            if (Int32.TryParse(value.ToString(), out res))
                if(res >= 0)
                    return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    [ValueConversion(typeof(int), typeof(string))]
    public class IdToFabricatorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            int res;
            if (Int32.TryParse(value.ToString(), out res))
               return SQL.GetFabricator(res);
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    [ValueConversion(typeof(int), typeof(string))]
    public class IdToTypeDeviceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            int res;
            if (Int32.TryParse(value.ToString(), out res))
                return SQL.GetTypesDevices(res);
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    [ValueConversion(typeof(int), typeof(int))]
    public class IntMinusIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            int res;
            if (Int32.TryParse(value.ToString(), out res))
                return res - Int32.Parse(parameter.ToString());
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
    
}
