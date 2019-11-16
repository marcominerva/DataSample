using DataSample.BusinessLayer.Services;
using DataSample.Common.Models;
using System.Windows;

namespace DataSample.Client.Views
{
    public partial class EditProductWindow : Window
    {
        private readonly IProductsService productsService;

        private Product product;
        public Product Product
        {
            get => product;
            set
            {
                product = value;
                IdTextBlock.Text = product.ProductId.ToString();
                NameTextBox.Text = product.ProductName;
            }
        }

        public EditProductWindow(IProductsService productsService)
        {
            InitializeComponent();

            this.productsService = productsService;
        }

        private async void SaveProductButton_Click(object sender, RoutedEventArgs e)
        {
            product.ProductName = NameTextBox.Text;
            await productsService.SaveAsync(product);

            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
