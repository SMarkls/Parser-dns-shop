using Parser_dns_shop.Model;

namespace Parser_dns_shop.ViewModel
{
    class DataViewModel
    {
        ProductData data;
        public ProductUpdater updater;
        public DataViewModel(ProductData data)
        {
            this.data = data;
            updater = new ProductUpdater(data);
        }
        public void AddProduct(string link)
        {
            updater.CreateProduct(link);
        }

        public void DelProduct(string link)
        {
            updater.DelProduct(link);
        }

        public bool ProductsContains(string link) => data.products.ContainsKey(link);

    }
}
