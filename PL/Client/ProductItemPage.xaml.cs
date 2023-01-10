using BO;
using PL.PLProduct;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
public partial class ProductItemPage : Page
{
    BlApi.IBl? bl = BlApi.Factory.Get();

    public Cart cart = new Cart();
    public ObservableCollection<BO.ProductItem?> ProductItems { get; set; }
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
        try
        {
            var id = ((BO.ProductItem)ProductItemListView.SelectedItem).ID;
            bl?.Cart.AddPToCart(cart, id);
        }
        catch (BO.NotExistsException) { MessageBox.Show("Not exists!"); }
        catch (BO.NotEnoughInStockException) { MessageBox.Show("Not enough in stock!"); }
    }

    private void DeleteFromCart_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            var id = ((BO.ProductItem)ProductItemListView.SelectedItem).ID;
            cart.Items?.Remove((BO.OrderItem)ProductItemListView.SelectedItem);
        }
        catch (BO.NotExistsException) { MessageBox.Show("Not exists!"); }
        catch (BO.NotEnoughInStockException) { MessageBox.Show("Not enough in stock!"); }
    }
}
