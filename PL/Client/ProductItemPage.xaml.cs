using BO;
using PL.PLProduct;
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

namespace PL.Client;

/// <summary>
/// Interaction logic for ProductItemPage.xaml
/// </summary>
public partial class ProductItemPage : Page,INotifyPropertyChanged
{
    BlApi.IBl? bl = BlApi.Factory.Get();

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

    public Cart cart = new Cart();
    //public ObservableCollection<BO.ProductItem?> ProductItems { get; set; }
    public ProductItemPage()
    {
        ProductItems = new ObservableCollection<BO.ProductItem?>(bl.Product.GetCatalog());
        InitializeComponent();
        ProductItemSelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
    }

    private void ProductItemListView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        
    }

    private void ProductItemSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ProductItemListView.ItemsSource = ProductItemSelector.SelectedItem.ToString() == "All" ? ProductItems
            : bl?.Product.GetCatalog(it => it?.Category.ToString() == ProductItemSelector.SelectedItem.ToString());
    }

    private void CartButton_OnClick(object sender, RoutedEventArgs e)
    {
        
    }

    private void BackButton_OnClick(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        Window.GetWindow(this)!.Close();
    }

    private void AddToCart_OnClick(object sender, RoutedEventArgs e)
    {
        if (ProductItemListView.SelectedItem != null)
        {
            var id = ((BO.ProductItem)ProductItemListView.SelectedItem).ID;
            bl?.Cart.AddPToCart(cart, id);
            ProductItems = new ObservableCollection<ProductItem?>(bl?.Product.GetCatalog()!);
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
            ProductItems = new ObservableCollection<ProductItem?>(bl?.Product.GetCatalog()!);
        }
        else
            MessageBox.Show("choose only from the list", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
