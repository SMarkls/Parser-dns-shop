using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parser_dns_shop.Model;


namespace Parser_dns_shop.ViewModel
{
    class DataViewModel
    {
        ProductData data;
        public DataViewModel(ProductData data)
        {
            this.data = data;
        }
        public async Task AddProduct(string link)
        {

        }

        public bool ProductsContains(string link) => !data.products.ContainsKey(link);

    }
}
