using System.Collections.Generic;
using Parser_dns_shop.ViewModel;

namespace Parser_dns_shop.Model
{
    class ProductData
    {
        private readonly DataViewModel viewModel;
        public Dictionary<string, int> products;
        public ProductData(DataViewModel viewModel)
        {
            this.viewModel = viewModel;
            products = new Dictionary<string, int>();
            products.Add("AFAFAF", 3000);
        }

        public void AddProduct(string link, int price)
        {
            products.Add(link, price);
            viewModel.OnPropertyChanged("ProductList");
        }

        public void RemoveProduct(string link)
        {
            products.Remove(link);
            viewModel.OnPropertyChanged("ProductList");
        }
        public void SetPrice(string link, int price)
        {
            products[link] = price;
            viewModel.OnPropertyChanged("ProductList");
        }

        public bool ProductsContains(string link) => products.ContainsKey(link);
    }
}
