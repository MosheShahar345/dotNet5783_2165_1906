using BO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL.Admin
{
    /// <summary>
    /// Interaction logic for OrderList.xaml
    /// </summary>
    public partial class OrderList : Page
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public ObservableCollection<BO.OrderForList?> Orders { get; set; }

        public OrderList()
        {
            Orders = new ObservableCollection<OrderForList?>(bl.Order.GetOrderForList());
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.OrderStatus));
        }

        private void CategorySelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OrderListView.ItemsSource = CategorySelector.SelectedItem.ToString() == "All" ? Orders
                : bl?.Order.GetOrderForList(it => it?.Status.ToString() == CategorySelector.SelectedItem.ToString());
        }

        private void ProductListView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
