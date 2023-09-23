

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using WPF.Library.Domain;
using WPF.Library.DTO;

namespace WPF.Client
{
    public partial class MainWindow : Window
    {
        HttpClient client = new HttpClient();
        private ObservableCollection<ProductDto> _products;

        public MainWindow()
        {
            client.BaseAddress = new Uri("http://localhost:5011/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            InitializeComponent();
            LoadCategories();
            LoadSuppliers();
            _products = new ObservableCollection<ProductDto>();
            DataContext = new ViewModel { Products = _products };
            GetProducts();
        }

        private void LoadElement_Click(object sender, RoutedEventArgs e)
        {
            GetProducts();
            LoadCategories();
            LoadSuppliers();
        }

        private async void GetProducts()
        {
            try
            {
                var response = await client.GetStringAsync("Products");
                var products = JsonConvert.DeserializeObject<List<ProductDto>>(response);
                Products.Clear();
                foreach (var product in products)
                {
                    Products.Add(product);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
        }

        public ObservableCollection<ProductDto> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                RaisePropertyChanged("Products");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task SaveProduct(Product product)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("Products", product);
                lblMessage.Content = response.IsSuccessStatusCode ? "Product Saved" : "Error saving product: " + response.ReasonPhrase;
            }
            catch (Exception ex)
            {
                lblMessage.Content = "Error: " + ex.Message;
            }
        }

        private async void btnSaveProduct_Click(object sender, RoutedEventArgs e)
        {
            decimal price;
            if (!decimal.TryParse(txtPrice.Text, out price))
            {
                MessageBox.Show("Please enter a valid price.", "Invalid Price", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var product = new Product()
            {
                Name = txtProductName.Text,
                Price = price,
                Category = cmbCategory.SelectedItem as Category,
                Supplier = cmbSupplier.SelectedItem as Supplier,
            };

            if (product.Category != null)
            {
                product.CategoryId = product.Category.Id;
            }

            if (product.Supplier != null)
            {
                product.SupplierId = product.Supplier.Id;
            }

            await SaveProduct(product);
            lblMessage.Content = "Product Saved";

            ClearFormFields();
            GetProducts();
        }

        private void btnEditProduct(object sender, RoutedEventArgs e)
        {
            ProductDto _selectedProduct = ((FrameworkElement)sender).DataContext as ProductDto;
            if (_selectedProduct != null)
            {
                txtProductId.Text = _selectedProduct.Id.ToString();
                txtProductName.Text = _selectedProduct.Name;
                txtPrice.Text = _selectedProduct.Price.ToString();

                SelectCategoryById(_selectedProduct.CategoryId);
                SelectSupplierById(_selectedProduct.SupplierId);

                btnSaveProduct.Visibility = Visibility.Collapsed;
                btnUpdateProduct.Visibility = Visibility.Visible;

            }
        }

        private void SelectCategoryById(int categoryId)
        {
            cmbCategory.SelectedValue = categoryId;
        }

        private void SelectSupplierById(int supplierId)
        {
            cmbSupplier.SelectedValue = supplierId;
        }

        private async void btnDeleteProduct(object sender, RoutedEventArgs e)
        {
            ProductDto product = ((FrameworkElement)sender).DataContext as ProductDto;
            if (product != null)
            {
                DeleteProduct(product.Id);
            }
           
        }

        private async Task<List<Category>> GetCategories()
        {
            var response = await client.GetStringAsync("Categories");
            return JsonConvert.DeserializeObject<List<Category>>(response);
        }
        private async Task<List<Supplier>> GetSuppliers()
        {
            var response = await client.GetStringAsync("Suppliers");
            return JsonConvert.DeserializeObject<List<Supplier>>(response);
        }

        private async void LoadSuppliers()
        {
            var suppliers = await GetSuppliers();
            cmbSupplier.ItemsSource = suppliers;
            cmbSupplier.DisplayMemberPath = "Name";
            cmbSupplier.SelectedValuePath = "Id";
        }

        private async void LoadCategories()
        {
            var categories = await GetCategories();
            cmbCategory.ItemsSource = categories;
            cmbCategory.DisplayMemberPath = "Name";
            cmbCategory.SelectedValuePath = "Id";
        }

        private async Task UpdateProduct(Product product)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("Products/" + product.Id, product);

                lblMessage.Content = response.IsSuccessStatusCode ? "Product Updated" : "Error updating product: " + response.ReasonPhrase;
            }
            catch (Exception ex)
            {
                lblMessage.Content = "Error: " + ex.Message;
            }
        }

        private async void DeleteProduct(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("Products/" + id);

                lblMessage.Content = response.IsSuccessStatusCode ? "Product Deleted" : "Error deleting product: " + response.ReasonPhrase;
                GetProducts();
            }
            catch (Exception ex)
            {
                lblMessage.Content = "Error: " + ex.Message;
            }
        }

        private async void btnUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            decimal price;
            if (!decimal.TryParse(txtPrice.Text, out price))
            {
                MessageBox.Show("Please enter a valid price.", "Invalid Price", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var product = new Product()
            {
                Id = Convert.ToInt32(txtProductId.Text),
                Name = txtProductName.Text,
                Price = price,
                Category = cmbCategory.SelectedItem as Category,
                Supplier = cmbSupplier.SelectedItem as Supplier,
            };

            if (product.Category != null)
            {
                product.CategoryId = product.Category.Id;
            }

            if (product.Supplier != null)
            {
                product.SupplierId = product.Supplier.Id;
            }

            await UpdateProduct(product);

            btnSaveProduct.Visibility = Visibility.Visible;
            btnUpdateProduct.Visibility = Visibility.Collapsed;
            lblMessage.Content = "Product Updated";

            ClearFormFields();
            GetProducts();
        }

       

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearFormFields();
        }

        private async void btnSaveSupplier_Click(object sender, RoutedEventArgs e)
        {          
            var supplier = new Supplier()
            {
                Name = txtSupplier.Text    
            };          

            await SaveSupplier(supplier);
            lblMessage.Content = "Supplier Saved";
            ClearFormFields();
            LoadSuppliers();

        }

        private async Task SaveSupplier(Supplier supplier)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("Suppliers", supplier);
                lblMessage.Content = response.IsSuccessStatusCode ? "Supplier Saved" : "Error saving product: " + response.ReasonPhrase;
            }
            catch (Exception ex)
            {
                lblMessage.Content = "Error: " + ex.Message;
            }
        }

        private async void btnSaveCategory_Click(object sender, RoutedEventArgs e)
        {
            var category = new Category()
            {
                Name = txtCategory.Text
            };

            await SaveCategory(category);
            lblMessage.Content = "Category Saved";
            ClearFormFields();
            LoadCategories();
        }

        private async Task SaveCategory(Category category)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("Categories", category);
                lblMessage.Content = response.IsSuccessStatusCode ? "Category Saved" : "Error saving product: " + response.ReasonPhrase;
            }
            catch (Exception ex)
            {
                lblMessage.Content = "Error: " + ex.Message;
            }
        }

        private void ClearFormFields()
        {
            txtProductId.Text = "0";
            txtProductName.Text = "";
            txtPrice.Text = "";
            cmbCategory.SelectedIndex = -1;
            cmbSupplier.SelectedIndex = -1;
            txtSupplier.Text = "";
            txtCategory.Text = "";
        }
    }
}
