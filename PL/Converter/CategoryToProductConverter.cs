using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PL.Converter;

public class CategoryToProductConverter : IValueConverter
{
    BlApi.IBl? bl = BlApi.Factory.Get();

    public ObservableCollection<BO.ProductForList?> Products { get; set; }
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value.ToString() == "All")
        {
            Products = new ObservableCollection<BO.ProductForList?>(bl!.Product.GetProductForList());
            return Products;
        }
        else
        {
            Products = new ObservableCollection<BO.ProductForList?>(bl!.Product.GetProductForList(it => it?.Category.ToString() == value.ToString()));
            return Products;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

