using PL.Client;
using System;
using System.Collections.Generic;
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
/// Interaction logic for OrderDetailsPage.xaml
/// </summary>
public partial class OrderDetailsPage : Page, INotifyPropertyChanged
{
    private BlApi.IBl? bl = BlApi.Factory.Get();

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public BO.Order order { get; set; }

    public OrderDetailsPage(BO.Order o)
    {
        order = o;
        OnPropertyChanged(order.ToString());
        InitializeComponent();
    }

    private void BackToMainWindowButton_OnClick(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        (Window.GetWindow(this)!).Close();
    }

    private void BackButton_OnClick(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this)!.Content = new OrderItemPage(order.ID);
    }
}
