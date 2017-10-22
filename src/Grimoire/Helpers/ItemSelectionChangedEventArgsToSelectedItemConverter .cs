using Syncfusion.ListView.XForms;
using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

public class ItemSelectionChangedEventArgsToSelectedItemConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var eventArgs = value as ItemSelectionChangedEventArgs;
        return eventArgs.AddedItems.FirstOrDefault();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
