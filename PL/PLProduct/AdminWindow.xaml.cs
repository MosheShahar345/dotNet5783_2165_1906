using System.Windows;
using System.Windows.Controls;

namespace PL.PLProduct;

/// <summary>
/// Interaction logic for ProductListWindow.xaml
/// </summary>
public partial class AdminWindow : Window
{
    BlApi.IBl? bl = BlApi.Factory.Get();

    public AdminWindow()
    {
        InitializeComponent();
    }

    private void TabControl_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (Product.IsSelected)
            AdminFrame.Content = new Admin.ProductList();
        if (Order.IsSelected)
            AdminFrame.Content = new Admin.OrderList();
    }
}
