using PL.Admin;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Globalization;
using System.Windows.Data;

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
        Products = new ObservableCollection<BO.ProductForList?>(bl.Product.GetProductForList());
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
            GetWindow(this)!.Close();
        }
        catch (BO.NotExistsException)
        {
            new AdminWindow().Show();
            GetWindow(this)!.Close();
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
        GetWindow(this)!.Close();
    }

    private void BackButton_OnClick(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        GetWindow(this)!.Close();
    }

    private void UpdateShipping_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            var id = ((BO.OrderForList)OrderListView.SelectedItem).ID;
            bl?.Order.UpdateOrderShipping(id);
        }
        catch (BO.OrderIsAlreadyDeliveredException) { MessageBox.Show("Order is already delivered!"); }
        catch (BO.OrderIsAlreadyShippedException) { MessageBox.Show("Order is already shipped!"); }
    }

    private void UpdateDelivery_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            var id = ((BO.OrderForList)OrderListView.SelectedItem).ID;
            bl?.Order.UpdateOrderDelivery(id);
        }
        catch (BO.OrderIsAlreadyDeliveredException) { MessageBox.Show("Order is already delivered!"); }
        catch (BO.OrderHasNotShippedException) { MessageBox.Show("Order is not shipped yet!"); }
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