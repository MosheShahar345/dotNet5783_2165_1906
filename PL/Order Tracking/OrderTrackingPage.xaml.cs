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

namespace PL.Order_Tracking
{
    /// <summary>
    /// Interaction logic for OrderTrackingPage.xaml
    /// </summary>
    public partial class OrderTrackingPage : Page, INotifyPropertyChanged
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public OrderTrackingPage()
        {
            TrackingResult = "";
            InitializeComponent();
        }

        private string trackingResult;

        public string TrackingResult
        {
            get => trackingResult;
            set
            {
                trackingResult = value;
                OnPropertyChanged(nameof(TrackingResult));
            }
        }
        private void EnterButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                int Id;
                int temp;
                int.TryParse(IdBox.Text, out temp);
                Id = temp;
                if (IdBox.Text == "")
                    throw new BO.FieldIsEmptyException("Id field is empty");
                var oroder = bl?.Order.GetOrder(Id);
                TrackingResult = $"{oroder?.ID.ToString()}" +
                                               $"\n{oroder?.OrderDate.ToString()}" +
                                               $"\n{oroder?.Status.ToString()}";
                TrackingResultTextBlock.Visibility= Visibility.Hidden;
                //ViewOrderItemButton.Visibility= Visibility.Visible;
                


            }
            catch (BO.NotExistsException) { TrackingResult = "Id was not found \n try again"; }
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            (Window.GetWindow(this)!).Close(); 
        }
    }
}
