using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Parser_dns_shop.Model
{
    class ProductData : INotifyPropertyChanged
    {
        public string ProductList { get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var p in products)
                    sb.Append(p.Key + ":\t" + p.Value.ToString() + "\n");
                return sb.ToString() ?? "afaf";
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
