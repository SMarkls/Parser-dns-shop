using Parser_dns_shop.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Parser_dns_shop.ViewModel
{
    class DataViewModel
    {
        public ProductData Data { get; set; }
        public string ProductList
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var p in Data.products)
                    sb.Append(p.Key + ":\t" + p.Value + "Р\n");
                return sb.ToString();
            }
        }
        public DataViewModel()
        {

        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
