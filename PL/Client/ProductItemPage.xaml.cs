using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PL.Admin;

namespace PL.Client;

/// <summary>
/// Interaction logic for ProductItemPage.xaml
/// </summary>
public partial class ProductItemPage : Page,INotifyPropertyChanged
{
    private BlApi.IBl? bl = BlApi.Factory.Get();

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    private ObservableCollection<BO.ProductItem?> productItems;

    public ObservableCollection<BO.ProductItem?> ProductItems
    {
        get => productItems;
        set
        {
            productItems = value;
            OnPropertyChanged(nameof(ProductItems));
        }
    }

    public BO.Cart cart = new BO.Cart();

    public ProductItemPage()
    {
        ProductItems = new ObservableCollection<BO.ProductItem?>(bl.Product.GetCatalog(cart));
        cart.Items = new ObservableCollection<BO.OrderItem?>().ToList();
        InitializeComponent();
        ProductItemSelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
    }

    private void ProductItemListView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        
    }

    private void ProductItemSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ProductItems = ProductItemSelector.SelectedItem.ToString() == "All" ? new ObservableCollection<BO.ProductItem?>(bl?.Product.GetCatalog(cart)!)
            : new ObservableCollection<BO.ProductItem?>(bl?.Product.GetCatalog(cart, it => it?.Category.ToString() == ProductItemSelector.SelectedItem.ToString())!);
    }

    private void CartButton_OnClick(object sender, RoutedEventArgs e)
    {
        Frame.Content = new CartPage(cart);
    }

    private void BackButton_OnClick(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        (Window.GetWindow(this)!).Close();
    }

    private void AddToCart_OnClick(object sender, RoutedEventArgs e)
    {
        if (ProductItemListView.SelectedItem != null)
        {
            var id = ((BO.ProductItem)ProductItemListView.SelectedItem).ID;
            bl?.Cart.AddPToCart(cart, id);
            ProductItems = new ObservableCollection<BO.ProductItem?>(bl?.Product.GetCatalog(cart)!);
        }
        else 
            MessageBox.Show("choose only from the list", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    private void DeleteFromCart_OnClick(object sender, RoutedEventArgs e)
    {
        if (ProductItemListView.SelectedItem != null)
        {
            var id = ((BO.ProductItem)ProductItemListView.SelectedItem).ID;
            cart.Items?.Remove(cart.Items?.FirstOrDefault(it => it?.ProductID == id));
            ProductItems = new ObservableCollection<BO.ProductItem?>(bl?.Product.GetCatalog(cart)!);
        }
        else
            MessageBox.Show("choose only from the list", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
