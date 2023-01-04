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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BO;

namespace PL.Admin
{
    /// <summary>
    /// Interaction logic for ProductList.xaml
    /// </summary>
    public partial class ProductList : Page
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public ObservableCollection<BO.ProductForList?> Products { get; set; }

        public ProductList()
        {
            Products = new ObservableCollection<ProductForList?>(bl.Product.GetProductForList());
            InitializeComponent();
            //ProductListView.ItemsSource = bl.Product.GetProductForList();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void ProductListView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int Id;
            try
            {
                Id = ((BO.ProductForList)ProductListView.SelectedItem)?.ID
                     ?? throw new BO.NotExistsException();

                new AddAndUpdateWindow(Id).Show(); 
                (Window.GetWindow(this)!).Close();
            }
            catch (BO.NotExistsException)
            {
                new AdminWindow().Show();
                (Window.GetWindow(this)!).Close();
            }
        }

        private void CategorySelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductListView.ItemsSource = CategorySelector.SelectedItem.ToString() == "All" ? Products
                : bl?.Product.GetProductForList(it => it?.Category.ToString() == CategorySelector.SelectedItem.ToString());
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            new AddAndUpdateWindow().Show();
            (Window.GetWindow(this)!).Close();
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            (Window.GetWindow(this)!).Close();
        }
    }
}
