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
using PL.PLProduct;
using PL.Client;
namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AdminButton_OnClick(object sender, RoutedEventArgs e)
        {
            new AdminWindow().Show();
            Close();
        }


        private void ClientButton_OnClick(object sender, RoutedEventArgs e)
        {
            //IconTie.Visibility = Visibility.Hidden;
            ProductItemFrame.Content = new ProductItemPage();
        }

        private void OrderTrackingButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void LogInButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

    }
}
