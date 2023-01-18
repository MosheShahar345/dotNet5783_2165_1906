using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using PL.Admin;
using PL.Client;
using BO;
using PL.Order_Tracking;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    BlApi.IBl? bl = BlApi.Factory.Get();

    /// <summary>
    /// constructor for the main window
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// button for admin section
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AdminButton_OnClick(object sender, RoutedEventArgs e)
    {
        new AdminWindow().Show();
        Close();
    }

    /// <summary>
    /// button for client section
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ClientButton_OnClick(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this)!.Content = new ProductItemPage();
    }

    /// <summary>
    /// button for order-tracking section
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OrderTrackingButton_OnClick(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this)!.Content = new OrderTrackingPage();
    }
}