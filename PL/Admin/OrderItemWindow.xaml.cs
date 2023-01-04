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
using System.Windows.Shapes;
using BO;

namespace PL.Admin
{
    /// <summary>
    /// Interaction logic for OrderItemWindow.xaml
    /// </summary>
    public partial class OrderItemWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public ObservableCollection<BO.OrderItem?> OrderItems { get; set; }
        public OrderItemWindow(int id)
        {
            OrderItems = new ObservableCollection<BO.OrderItem?>(bl.Order.GetOrder(id).Items!);
            InitializeComponent();
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            new AdminWindow().Show();
            Close();
        }
    }
}
