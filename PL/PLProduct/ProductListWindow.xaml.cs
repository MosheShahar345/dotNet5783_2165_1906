﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
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
using BlApi;
using BlImplementation;

namespace PL.PLProduct;

    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
public partial class ProductListWindow : Window
{
    private IBl bl = new Bl();

    public ProductListWindow()
    {
        InitializeComponent();
        ProductListView.ItemsSource = bl.Product.GetProductForList();
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
    }

    private void ProductListView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var x = (BO.ProductForList)((ListView)sender).Items[((ListView)sender).SelectedIndex];
        new AddAndUpdateWindow(x.ID).Show();
        Close();
    }

    private void CategorySelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ProductListView.ItemsSource = CategorySelector.SelectedItem.ToString() == "All" ? bl.Product.GetProductForList() 
            : bl.Product.GetProductForList(it => it?.Category.ToString() == CategorySelector.SelectedItem.ToString());
    }

    private void AddButton_OnClick(object sender, RoutedEventArgs e)
    {
        new AddAndUpdateWindow().Show();
        Close();
    }
}
