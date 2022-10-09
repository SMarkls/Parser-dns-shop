using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Parser_dns_shop.Model
{
    class ProductData : INotifyPropertyChanged
    {
        public string ProductList { get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var p in products)
                    sb.Append(p.Key + ":\t" + p.Value.ToString() + "Р\n");
                return sb.ToString() ?? "";
            }
        }
        public Dictionary<string, int> products;
        public ProductData()
        {
            products = new Dictionary<string, int>();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
