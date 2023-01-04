using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using WebLibrary;


namespace Parser_dns_shop.Model
{
    class ProductUpdater
    {
        static string pathToEdge = Properties.Settings.Default.PathToEdge;
        static int frequency = int.Parse(Properties.Settings.Default.Frequency);
        Timer timer;
        Queue<string> queue = new Queue<string>();
        public Parser parser = new Parser(pathToEdge);
        ProductData data;
        public ProductUpdater(ProductData data)
        {
            this.data = data;
            timer = new Timer(frequency);
            timer.Elapsed += TimerTick;
            timer.Start();
            
        }
        public async Task CreateProductAsync(string link)
        {
            InstanceChecker.Wait();
            parser.CreateDriver(pathToEdge);
            int price = await parser.GetPriceAsync(link);
            data.AddProduct(link, price);
            InstanceChecker.Release();
        }
        private async void TimerTick(object sender, ElapsedEventArgs e)
        {
            await TimerTickHandler();
        }
        private async Task TimerTickHandler()
        {
            timer.Stop();
            InstanceChecker.Wait();
            parser.CreateDriver(pathToEdge);
            if (data.products.Count == 0)
            {
                InstanceChecker.Release();
                parser.DisposeDriver();
                timer.Start();
                return;
            }

            foreach (var t in data.products)
                queue.Enqueue(t.Key);
            foreach (var x in queue)
            {
                int price = await parser.GetPriceAsync(x);
                if (data.products[x] != price)
                {
                    MessageBox.Show($"Изменения в цене {x}: {data.products[x]}->{price}");
                    data.SetPrice(x, price);
                }
            }
            queue.Clear();
            InstanceChecker.Release();
            timer.Start();
            parser.DisposeDriver();
        }
    }
}
