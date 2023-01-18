using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL.Admin;

/// <summary>
/// Interaction logic for ProductListPage.xaml
/// </summary>
public partial class AdminWindow : Window, INotifyPropertyChanged
{
    BlApi.IBl? bl = BlApi.Factory.Get();

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChange(string propertyName)
    {
        PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propertyName));
    }

    private ObservableCollection<BO.ProductForList?> products;

    public ObservableCollection<BO.ProductForList?> Products
    {
        get => products;
        set
        {
            products = value;
            OnPropertyChange(nameof(Products));
        }
    }

    private ObservableCollection<BO.OrderForList?> orders;

    public ObservableCollection<BO.OrderForList?> Orders
    {
        get => orders;
        set
        {
            orders = value;
            OnPropertyChange(nameof(Orders));
        }
    }

    /// <summary>
    /// constructor
    /// </summary>
    public AdminWindow()
    {
        Orders = new ObservableCollection<BO.OrderForList?>(bl.Order.GetOrderForList());
        Products = new ObservableCollection<BO.ProductForList?>(bl.Product.GetProductForList());
        InitializeComponent();
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        StatusSelector.ItemsSource = Enum.GetValues(typeof(BO.OrderStatus));
    }

    /// <summary>
    /// on double clicking item in the list of products event  
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ProductListView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (ProductListView.SelectedItem != null)
        {
            var Id = (int)((BO.ProductForList)ProductListView.SelectedItem)?.ID!;
            new AddAndUpdateWindow(Id).Show();
            (Window.GetWindow(this)!).Close();
        }
    }

    /// <summary>
    /// filter by category
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CategorySelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Products = CategorySelector.SelectedItem.ToString() == "All" ? new ObservableCollection<BO.ProductForList?>(bl?.Product.GetProductForList()!)
            : new ObservableCollection<BO.ProductForList?>(bl?.Product.GetProductForList(it => it?.Category.ToString() == CategorySelector.SelectedItem.ToString())!);
    }

    /// <summary>
    /// event button for adding a new product
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddButton_OnClick(object sender, RoutedEventArgs e)
    {
        new AddAndUpdateWindow().Show();
        (Window.GetWindow(this)!).Close();
    }

    /// <summary>
    /// back to main window button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BackButton_OnClick(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        (Window.GetWindow(this)!).Close();
    }

    /// <summary>
    /// on right button click item one for updating order status : sent
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UpdateShipping_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            var id = ((BO.OrderForList)OrderListView.SelectedItem).ID;
            bl?.Order.UpdateOrderShipping(id);
            Orders = new ObservableCollection<BO.OrderForList?>(bl?.Order.GetOrderForList()!);
        }
        catch (BO.OrderIsAlreadyDeliveredException) { MessageBox.Show("Order is already delivered!"); }
        catch (BO.OrderIsAlreadyShippedException) { MessageBox.Show("Order is already shipped!"); }
    }

    /// <summary>
    /// on right button click item two for updating order status : delivered
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UpdateDelivery_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            var id = ((BO.OrderForList)OrderListView.SelectedItem).ID;
            bl?.Order.UpdateOrderDelivery(id);
            Orders = new ObservableCollection<BO.OrderForList?>(bl?.Order.GetOrderForList()!);
        }
        catch (BO.OrderIsAlreadyDeliveredException) { MessageBox.Show("Order is already delivered!"); }
        catch (BO.OrderHasNotShippedException) { MessageBox.Show("Order is not shipped yet!"); }
    }

    /// <summary>
    /// filter by status
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void StatusSSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Orders = StatusSelector.SelectedItem.ToString() == "All" ? new ObservableCollection<BO.OrderForList?>(bl?.Order.GetOrderForList()!) 
            : new ObservableCollection<BO.OrderForList?>(bl?.Order.GetOrderForList(it => it?.Status.ToString() == StatusSelector.SelectedItem.ToString())!);
    }

    /// <summary>
    ///  on double clicking item in the list of orders event 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OrderListView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (OrderListView.SelectedItem != null)
        {
            var Id = (int)((BO.OrderForList)OrderListView.SelectedItem)?.ID!;
            new OrderItemWindow(Id).Show();
            (Window.GetWindow(this)!).Close();
        }
    }
}