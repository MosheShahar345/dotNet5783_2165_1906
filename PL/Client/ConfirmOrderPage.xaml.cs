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

namespace PL.Client;

/// <summary>
/// Interaction logic for ConfirmOrderPage.xaml
/// </summary>
public partial class ConfirmOrderPage : Page
{
    private BlApi.IBl? bl = BlApi.Factory.Get();

    public BO.Cart Cart { get; set; }

    /// <summary>
    /// constructor with an existing cart
    /// </summary>
    /// <param name="cart"></param>
    public ConfirmOrderPage(BO.Cart cart)
    {
        Cart = cart;
        InitializeComponent();
    }

    /// <summary>
    /// confirm order event - confirmation
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ConfirmOrderButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            if (TextBoxName.Text == "" || TextBoxAddress.Text == "" || TextBoxEmail.Text == "")
                throw new BO.FieldIsEmptyException();
            bl?.Cart.ConfirmationOrder(Cart);
            Cart = new BO.Cart();
            ConfirmOrderButton.Visibility = Visibility.Hidden;
            OrderConfirmedTextBlock.Visibility = Visibility.Visible;
        }
        catch (FormatException) { MessageBox.Show("Invalid format", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
        catch (BO.FieldIsEmptyException) { MessageBox.Show("Field is empty", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
        catch (BO.InvalidAddressException) { MessageBox.Show("Invalid address", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
        catch (BO.InvalidNameException) { MessageBox.Show("Invalid name", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
        catch (BO.InvalidEmailException) { MessageBox.Show("Invalid email", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
        catch (BO.AlreadyExistsException) { MessageBox.Show("Already exists", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
    }

    /// <summary>
    ///  back to product item page button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BackButton_OnClick(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this)!.Content = new ProductItemPage(Cart);
    }

    /// <summary>
    ///  back main window button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BackToMainWindowButton_OnClick(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        (Window.GetWindow(this)!).Close();
    }
}