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

    public BO.Cart Cart;

    /// <summary>
    /// constructor with or without cart
    /// </summary>
    /// <param name="cart"></param>
    public ProductItemPage(BO.Cart? cart = null)
    {
        Cart = cart ?? new BO.Cart
        {
            Items = new ObservableCollection<BO.OrderItem?>().ToList()
        }; 
        ProductItems = new ObservableCollection<BO.ProductItem?>(bl.Product.GetCatalog(Cart));
        InitializeComponent();
        ProductItemSelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
    }

    /// <summary>
    /// filter by category
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ProductItemSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ProductItems = ProductItemSelector.SelectedItem.ToString() == "All" ? new ObservableCollection<BO.ProductItem?>(bl?.Product.GetCatalog(Cart)!)
            : new ObservableCollection<BO.ProductItem?>(bl?.Product.GetCatalog(Cart, it => it?.Category.ToString() == ProductItemSelector.SelectedItem.ToString())!);
    }
    
    /// <summary>
    /// to the cart button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CartButton_OnClick(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this)!.Content = new CartPage(Cart);
    }

    /// <summary>
    /// back main window button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BackButton_OnClick(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        (Window.GetWindow(this)!).Close();
    }

    /// <summary>
    /// add a product to the cart
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddToCart_OnClick(object sender, RoutedEventArgs e)
    {
        if (ProductItemListView.SelectedItem != null)
        {
            var id = ((BO.ProductItem)ProductItemListView.SelectedItem).ID;
            bl?.Cart.AddPToCart(Cart, id);
            ProductItems = new ObservableCollection<BO.ProductItem?>(bl?.Product.GetCatalog(Cart)!);
        }
        else 
            MessageBox.Show("choose only from the list", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    /// <summary>
    /// delete a product from the cart
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DeleteFromCart_OnClick(object sender, RoutedEventArgs e)
    {
        if (ProductItemListView.SelectedItem != null)
        {
            var id = ((BO.ProductItem)ProductItemListView.SelectedItem).ID;
            Cart.Items?.Remove(Cart.Items?.FirstOrDefault(it => it?.ProductID == id));
            ProductItems = new ObservableCollection<BO.ProductItem?>(bl?.Product.GetCatalog(Cart)!);
        }
        else
            MessageBox.Show("choose only from the list", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
