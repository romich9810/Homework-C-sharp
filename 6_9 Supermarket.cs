using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6_9_Supermarket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Market market = new Market();

            market.Work();
        }
    }

    enum NamesProduct
    {
        Морковка = 1, Пицца, Картошка, Водка, Капуста
    }

    class Client
    {
        private List<Product> _commercialTrolley = new List<Product>();
        private List<Product> _shoppingBag;

        public Client(List<Product> commercialTrolley) 
        {
            Random random = new Random();

            int maxMoney = 300;
            int minMoney = 50;

            int money = random.Next(minMoney, maxMoney + 1);
            Money = money;

            foreach(Product product in commercialTrolley)
            {
                _commercialTrolley.Add(product);
            }
        }

        public int Money { get; private set; }

        public List<Product> PutProductsAtBelt()
        {
            List<Product> productsForBelt = new List<Product>();

            foreach(Product product in _commercialTrolley)
            {
                productsForBelt.Add(product);
            }

            _commercialTrolley = null;

            return productsForBelt;
        }

        public bool IsMoneyEnough(List<Product> products)
        {
            int total = 0;

            foreach(Product product in products)
            {
                total += product.Price;
            }

            return Money >= total;
        }

        public void TakeProducts(List<Product> products) 
        {
            _shoppingBag = products;           
        }

        public int GiveMoneyForProducts(int total)
        {
            Money += total;

            return total;
        }

        public void LeaveProductAtCheckout(List<Product> products) 
        {
            Random random = new Random();

            int lastProduct = products.Count;

            int positionForLeave = random.Next(0, lastProduct);

            Console.WriteLine($"Клиент решил оставить {products[positionForLeave].Name} на сумму {products[positionForLeave].Price} рублей.");

            products.RemoveAt(positionForLeave);
        }
    }

    class Market
    {
        private List<Product> _allProducts = new List<Product>()
        {
            new Product(NamesProduct.Морковка, 10),
            new Product(NamesProduct.Пицца, 150),
            new Product(NamesProduct.Картошка, 30),
            new Product(NamesProduct.Водка, 100),
            new Product(NamesProduct.Капуста, 25)
        };
        
        private Queue<Client> _clienst = new Queue<Client> ();

        public Market()
        {
            int countClients = 5;
            FillQueue(countClients);
        }

        public int Cash { get; private set; } = 0;       

        public void Work()
        {
            const string ServeClientCommandNumber = "1";
            const string ExitCommandNumber = "2";

            string userCommand;

            bool isNeedWork = true;

            while (isNeedWork & _clienst.Count > 0)
            {
                int countOfClient = _clienst.Count;

                Console.SetCursorPosition(50, 0);
                Console.WriteLine($"Вырчука: {Cash}");
                Console.SetCursorPosition(0, 0);

                Console.WriteLine($"У вас в очереди {countOfClient}  клиентов.");
                Console.WriteLine($"\n{ServeClientCommandNumber} - обслужить клиента" +
                    $"\n{ExitCommandNumber} - закрыть магазин(выход из программы)");

                userCommand = Console.ReadLine();

                switch (userCommand)
                {
                    case ServeClientCommandNumber:
                        ServeClient(_clienst.Dequeue());
                        break;

                    case ExitCommandNumber:
                        isNeedWork = false;
                        break;

                    default:
                        Console.WriteLine("Неверная команда!");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }

            if (_clienst.Count > 0)
            {
                Console.WriteLine("Магазин закрылся.");
            }
            else
            {
                Console.WriteLine($"Клиенты закончились. Ваша выручка составила {Cash} рублей.");
            }
        }

        private void ServeClient(Client client)
        {
            List<Product> productBelt;

            int total = 0;

            productBelt = client.PutProductsAtBelt();

            total = GetTotalSumForProducts(productBelt);

            Console.WriteLine($"У вас {productBelt.Count} позиций на сумму {total} рублей.");

            foreach(Product product in productBelt)
            {
                Console.Write($"{product.Name} ");
            }

            Console.WriteLine();

            while (client.IsMoneyEnough(productBelt) == false)
            {              
                Console.WriteLine($"У клиента {client.Money} рублей, заместо {total}.");

                client.LeaveProductAtCheckout(productBelt);

                total = GetTotalSumForProducts(productBelt);
            }

            if (total > 0)
            {
                client.TakeProducts(productBelt);
                Cash += client.GiveMoneyForProducts(total);

                Console.WriteLine($"Клиент совершил покупку продуктов на {total} рублей.");
            }
            else
            {
                Console.WriteLine($"Клиент оставил всё и ничего не купил - не хватает денег({client.Money} рублей всего)");
            } 
        }

        private int GetTotalSumForProducts(List<Product> products)
        {
            int total = 0;

            foreach(Product product in products)
            {
                total += product.Price;
            }

            return total;
        }

        private List<Product> GiveProducts(Random random) 
        {
            List<Product> commercialTrolley = new List<Product>();

            int firstVariantProduct = 1;
            int lastVariantProduct = 5;

            int minCountProducts = 1;
            int maxCountProducts = 8;

            int countProducts = random.Next(minCountProducts, maxCountProducts);

            for ( int i = 0; i < countProducts; i++)
            {
                NamesProduct nameProduct = (NamesProduct)random.Next(firstVariantProduct, lastVariantProduct);

                foreach (Product product in _allProducts)
                {
                    if (product.Name == nameProduct)
                    {
                        commercialTrolley.Add(new Product(product.Name, product.Price));
                    }
                }
            }

            return commercialTrolley;
        }

        private void FillQueue(int countClient)
        {
            Random random = new Random();

            for (int i = 0; i < countClient; i++)
            {
                _clienst.Enqueue(new Client(GiveProducts(random)));
            }
        }
    }

    class Product
    {
        public Product (NamesProduct name, int price)
        {
            Name = name;
            Price = price;
        }

        public NamesProduct Name { get; }
        public int Price { get; }
    }
}
