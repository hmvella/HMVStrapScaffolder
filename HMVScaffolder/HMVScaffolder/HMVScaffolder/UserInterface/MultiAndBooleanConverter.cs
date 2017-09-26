using System;
using System.Globalization;
using System.Windows.Data;

namespace HMVScaffolder.Mvc
{
    public class MultiAndBooleanConverter : IMultiValueConverter
    {
        public MultiAndBooleanConverter()
        {
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = true;
            for (int i = 0; i < (int)values.Length; i++)
            {
                if (!(values[i] is bool))
                {
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Values must be boolean!. Value {0} had a type of {1}", i, values[i].GetType().ToString()));
                }
                flag &= (bool)values[i];
            }
            return flag;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}