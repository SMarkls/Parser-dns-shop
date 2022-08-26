using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using WebLibrary;


namespace Parser_dns_shop.Model
{
    class ProductUpdater
    {
        Timer timer;
        public bool isAdding = false;
        public bool isTicked = false;
        Queue<string> queue = new Queue<string>();
        public Parser parser = new Parser();
        ProductData data;
        public ProductUpdater(ProductData data)
        {
            this.data = data;
            timer = new Timer(60000);
            timer.Elapsed += TimerTick;
            timer.Start();
            
        }
        public async Task CreateProduct(string link)
        {
            await Task.Run(() => { while (isTicked || isAdding) { } });
            isAdding = true;
            int price = await parser.GetPriceAsync(link);
            data.products.Add(link, price);
            data.OnPropertyChanged("ProductList");
            isAdding = false;
        }

        public void DelProduct(string link)
        {
            data.products.Remove(link);
            data.OnPropertyChanged("ProductList");
        }

        private async void TimerTick(object sender, ElapsedEventArgs e)
        {
            await Task.Run(() => { while (isAdding || isTicked) { } });
            isTicked = true;
            timer.Stop();
            if (data.products.Count == 0)
            {
                timer.Start();
                return;
            }                

            foreach (var t in data.products)
                queue.Enqueue(t.Key);
            foreach(var x in queue)
            {
                int price = await parser.GetPriceAsync(x);
                if (data.products[x] != price)
                {
                    MessageBox.Show($"Изменения в цене {x}: {data.products[x]}->{price}");
                    data.products[x] = price;
                    data.OnPropertyChanged("ProductList");
                }
            }
            queue.Clear();
            isTicked = false;
            timer.Start();
        }
    }
}
