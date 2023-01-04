using BO;
using PL.Admin;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL.PLProduct;

/// <summary>
/// Interaction logic for ProductListWindow.xaml
/// </summary>
public partial class AdminWindow : Window
{
    BlApi.IBl? bl = BlApi.Factory.Get();

    public ObservableCollection<BO.ProductForList?> Products { get; set; }

    public ObservableCollection<BO.OrderForList?> Orders { get; set; }

    public AdminWindow()
    {
        Orders = new ObservableCollection<BO.OrderForList?>(bl.Order.GetOrderForList());
        Products = new ObservableCollection<ProductForList?>(bl.Product.GetProductForList());
        InitializeComponent();
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        StatusSelector.ItemsSource = Enum.GetValues(typeof(BO.OrderStatus));
    }

    private void ProductListView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        int Id;
        try
        {
            Id = ((BO.ProductForList)ProductListView.SelectedItem)?.ID
                 ?? throw new BO.NotExistsException();

            new AddAndUpdateWindow(Id).Show();
            (Window.GetWindow(this)!).Close();
        }
        catch (BO.NotExistsException)
        {
            new AdminWindow().Show();
            (Window.GetWindow(this)!).Close();
        }
    }

    private void CategorySelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ProductListView.ItemsSource = CategorySelector.SelectedItem.ToString() == "All" ? Products
            : bl?.Product.GetProductForList(it => it?.Category.ToString() == CategorySelector.SelectedItem.ToString());
    }

    private void AddButton_OnClick(object sender, RoutedEventArgs e)
    {
        new AddAndUpdateWindow().Show();
        (Window.GetWindow(this)!).Close();
    }

    private void BackButton_OnClick(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        (Window.GetWindow(this)!).Close();
    }

    private void UpdateShipping_OnClick(object sender, RoutedEventArgs e)
    {
        var id = ((BO.OrderForList)OrderListView.SelectedItem).ID;
        bl?.Order.UpdateOrderShipping(id);
        Orders = new ObservableCollection<BO.OrderForList?>(bl!.Order.GetOrderForList());
        
    }

    private void UpdateDelivery_OnClick(object sender, RoutedEventArgs e)
    {
        var id = ((BO.OrderForList)OrderListView.SelectedItem).ID;
        bl?.Order.UpdateOrderDelivery(id);
        Orders = new ObservableCollection<BO.OrderForList?>(bl!.Order.GetOrderForList());
        
    }

    private void TabControl_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        //if (Product.IsSelected)
        //    AdminFrame.Content = new Admin.ProductList();
        //if (Order.IsSelected)
        //    AdminFrame.Content = new Admin.OrderList();
    }

    private void StatusSSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        OrderListView.ItemsSource = StatusSelector.SelectedItem.ToString() == "All" ? Orders
            : bl?.Order.GetOrderForList(it => it?.Status.ToString() == StatusSelector.SelectedItem.ToString());
    }

    private void OrderListView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var Id = (int)((BO.OrderForList)OrderListView.SelectedItem)?.ID!;
        new OrderItemWindow(Id).Show();
        (Window.GetWindow(this)!).Close();
    }
}
