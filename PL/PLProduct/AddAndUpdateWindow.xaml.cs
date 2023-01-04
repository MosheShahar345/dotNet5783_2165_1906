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

namespace PL.PLProduct
{
    /// <summary>
    /// Interaction logic for AddAndUpdateWindow.xaml
    /// </summary>
    public partial class AddAndUpdateWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public AddAndUpdateWindow()
        {
            InitializeComponent();
            for (int i = 0; i < 5; i++)
                CategoryComboBox.Items.Add($"{(BO.Category)i}");
            CategoryComboBox.Text = "None";
            AddAndUpdateProductButton.Content = "Add";
        }

        public AddAndUpdateWindow(int Id)
        {
            InitializeComponent();
            for (int i = 0; i < 5; i++)
                CategoryComboBox.Items.Add($"{(BO.Category)i}");
            var product = bl.Product.GetProductForAdmin(Id);
            IdBox.Text = Id.ToString();
            CategoryComboBox.SelectedItem = product.Category.ToString();
            NameBox.Text = product.Name;
            PriceBox.Text = product.Price.ToString();
            InStockBox.Text = product.InStock.ToString();
            IdBox.IsReadOnly = true;
            AddAndUpdateProductButton.Content = "Update";
        }

        private void AddAndUpdateProductButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IdBox.Text == "")
                    throw new BO.FieldIsEmptyException("Id field is empty");
                if (CategoryComboBox.Text == "" || CategoryComboBox.Text == "None")
                    throw new BO.FieldIsEmptyException("Category field is empty");
                if (NameBox.Text == "")
                    throw new BO.FieldIsEmptyException("Name field is empty");
                if (PriceBox.Text == "" || PriceBox.Text == 0.ToString())
                    throw new BO.FieldIsEmptyException("Price field is empty");
                if (InStockBox.Text == "" || InStockBox.Text == 0.ToString())
                    throw new BO.FieldIsEmptyException("In Stock field is empty");

                BO.Product product = new BO.Product()
                {
                    ID = int.Parse(IdBox.Text),
                    Category = (BO.Category)CategoryComboBox.SelectedIndex,
                    Price = double.Parse(PriceBox.Text),
                    Name = NameBox.Text,
                    InStock = int.Parse(InStockBox.Text)
                };

                if (AddAndUpdateProductButton.Content.ToString() == "Add")
                    bl?.Product.AddProductAdmin(product);
                else
                    bl?.Product.UpdateProductAdmin(product);

                new AdminWindow().Show();
                Close();
            }
            catch (FormatException) { MessageBox.Show("invalid format"); }
            catch (BO.FieldIsEmptyException) { MessageBox.Show("Field is empty"); }
            catch (BO.InvalidPriceException) { MessageBox.Show("Price is lass than 0"); }
            catch (BO.IdIsLessThanZeroException) { MessageBox.Show("Id is lass than 0"); }
            catch (BO.InvalidNameException) { MessageBox.Show("Invalid name"); }
            catch (BO.InvalidInStockException) { MessageBox.Show("In Stock is lass than 0"); }
            catch (BO.AlreadyExistsException) { MessageBox.Show("Already exists"); }
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            new AdminWindow().Show();
            Close();
        }
    }
}
