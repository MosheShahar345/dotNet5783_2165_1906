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
using BO;

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
            Orders = new ObservableCollection<BO.OrderForList?>(bl.Order.GetOrderForList());
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
            var Id = (int)((BO.OrderForList)OrderListView.SelectedItem)?.ID!;
            new OrderItemWindow(Id).Show();
            (Window.GetWindow(this)!).Close();
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            (Window.GetWindow(this)!).Close();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            (Window.GetWindow(this)!).Close();
        }

        private void UpdateShipping_OnClick(object sender, RoutedEventArgs e)
        {
            var id = ((BO.OrderForList)OrderListView.SelectedItem).ID;
            bl?.Order.UpdateOrderShipping(id);
            Orders = new ObservableCollection<BO.OrderForList?>(bl!.Order.GetOrderForList());
            this.NavigationService!.Refresh();
        }

        private void UpdateDelivery_OnClick(object sender, RoutedEventArgs e)
        {
            var id = ((BO.OrderForList)OrderListView.SelectedItem).ID;
            bl?.Order.UpdateOrderDelivery(id);
            Orders = new ObservableCollection<BO.OrderForList?>(bl!.Order.GetOrderForList());
            this.NavigationService!.Refresh();
        }
    }
}
