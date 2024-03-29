﻿using MaterialDesignThemes.Wpf;
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
/// Interaction logic for CartPage.xaml
/// </summary>
public partial class CartPage : Page , INotifyPropertyChanged
{
    private BlApi.IBl? bl = BlApi.Factory.Get();

    public BO.Cart Cart;

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public IEnumerable<int> LongIntegerList => Enumerable.Range(0, 100).ToList();

    private ObservableCollection<BO.OrderItem?> _orderItems = null!;

    public ObservableCollection<BO.OrderItem?> OrderItems
    {
        get => _orderItems;
        set
        {
            _orderItems = value;
            OnPropertyChanged(nameof(OrderItems));
        }
    }

    private double _totalPrice;

    public double TotalPrice
    {
        get => _totalPrice;
        set
        {
            _totalPrice = value;
            OnPropertyChanged(nameof(TotalPrice));
        }
    }

    /// <summary>
    /// constructor with an existing cart
    /// </summary>
    /// <param name="cart"></param>
    public CartPage(BO.Cart cart)
    {
        TotalPrice = cart.TotalPrice;
        OrderItems = new ObservableCollection<BO.OrderItem?>(cart.Items!);
        InitializeComponent();
        Cart = cart;
    }

    /// <summary>
    /// when an item is chosen to have its amount updated 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UpdateAmount_OnClick(object sender, RoutedEventArgs e)
    {
        AmountToUpdate.Visibility = Visibility.Visible;
    }

    /// <summary>
    /// removing an item from the cart
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RemoveFromCart_OnClick(object sender, RoutedEventArgs e)
    {
        var id = ((BO.OrderItem)OrderItemListView.SelectedItem).ProductID;
        BO.OrderItem orderItem = Cart.Items?.FirstOrDefault(x => x?.ProductID == id)!;
        Cart.Items?.Remove(orderItem);
        Cart.TotalPrice -= orderItem.TotalPrice;
        TotalPrice = Cart.TotalPrice;
        AmountToUpdate.Visibility = Visibility.Hidden;
        OrderItems = new ObservableCollection<BO.OrderItem?>(Cart.Items!); // refreshing
    }

    /// <summary>
    /// confirm order button to confirm the order
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ConfirmOrderButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (Cart.Items?.Count > 0)
        {
            Window.GetWindow(this)!.Content = new ConfirmOrderPage(Cart);
        }
        else
            MessageBox.Show("Cart is empty", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    /// <summary>
    /// back to product item page button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BackButton_OnClick(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this)!.Content = new ProductItemPage(Cart);
    }

    /// <summary>
    /// updating the amount of a chosen item 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AmountToUpdate_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var id = ((BO.OrderItem)OrderItemListView.SelectedItem).ProductID;
        var newAmount = int.Parse(AmountToUpdate.SelectedItem.ToString()!);
        try
        {
            bl?.Cart.UpdateAmount(Cart, id, newAmount);
        }
        catch (BO.NotEnoughInStockException) { MessageBox.Show("Not enough in stock", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }

        AmountToUpdate.Visibility = newAmount == 0 ? Visibility.Hidden : Visibility.Visible;
        TotalPrice = Cart.TotalPrice;
        OrderItems = new ObservableCollection<BO.OrderItem?>(Cart.Items!);
    }
}