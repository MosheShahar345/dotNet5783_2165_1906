using PL.Admin;
using PL.Client;
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

namespace PL.Order_Tracking;

/// <summary>
/// Interaction logic for OrderItemPage.xaml
/// </summary>
public partial class OrderItemPage : Page
{
    private BlApi.IBl? bl = BlApi.Factory.Get();
    public ObservableCollection<BO.OrderItem?> OrderItems { get; set; }
    
    public BO.Order order = new BO.Order();
    
    public int OrderId;
    public OrderItemPage(int id)
    {        
        OrderItems = new ObservableCollection<BO.OrderItem?>(bl.Order.GetOrder(id).Items!);
        order = bl?.Order.GetOrder(id)!;
        InitializeComponent();
    }

    private void OrderDetailsButton_OnClick(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this)!.Content = new OrderDetailsPage(order);
        OrderDetails.Visibility = Visibility.Hidden;
    }

    private void BackButton_OnClick(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this)!.Content = new OrderTrackingPage();
    }

    private void Frame_Navigated(object sender, NavigationEventArgs e)
    {

    }
}
