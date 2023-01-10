using PL.PLProduct;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL.Order_Tracking
{
    /// <summary>
    /// Interaction logic for OrderTrackingPage.xaml
    /// </summary>
    public partial class OrderTrackingPage : Page
    {
        public OrderTrackingPage()
        {
            InitializeComponent();
        }
        private void EnterButton_OnClick(object sender, RoutedEventArgs e)
        {

        }
        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Window.GetWindow(this)!.Close();
        }

    }
   
}
