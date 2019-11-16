using DataSample.BusinessLayer.Services;
using DataSample.Common.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DataSample.Client.Views
{
    public partial class MainWindow : Window
    {
        private readonly IProductsService productsService;
        private readonly IServiceProvider serviceProvider;

        public MainWindow(IServiceProvider serviceProvider, IProductsService productsService)
        {
            InitializeComponent();

            this.serviceProvider = serviceProvider;
            this.productsService = productsService;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await SearchAsync();
        }

        private async void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            await SearchAsync();
        }

        private async Task SearchAsync()
        {
            try
            {
                Cursor = Cursors.Wait;
                var products = await productsService.GetAsync(SearchTextBox.Text.Trim(), 0, 50);
                ProductsDataGrid.ItemsSource = products;
            }
            catch
            {
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }
        }

        private async void ProductsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((sender as DataGrid).SelectedItem is Product product)
            {
                var editWindow = serviceProvider.GetRequiredService<EditProductWindow>();
                editWindow.Product = product;
                var saved = editWindow.ShowDialog();

                if (saved.GetValueOrDefault())
                {
                    // If data has been changed, refresh the grid.
                    await SearchAsync();
                }
            }
        }
    }
}
