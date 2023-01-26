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

    class Client
    {
        private List<Product> _commercialTrolley = new List<Product>();

        public Client(List<Product> commercialTrolley, Random random) 
        {
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

        public List<Product> GiveProductsAtCheckout()
        {
            List<Product> productsForBelt = new List<Product>();

            foreach(Product product in _commercialTrolley)
            {
                productsForBelt.Add(product);
            }

            return productsForBelt;
        }

        public bool IsMoneyEnough()
        {
            int total = 0;

            foreach(Product product in _commercialTrolley)
            {
                total += product.Price;
            }

            return Money >= total;
        }

        public int GiveMoneyForProducts(int total)
        {
            Money -= total;

            return total;
        }

        public void LeaveProductAtCheckout() 
        {
            Random random = new Random();

            int lastProduct = _commercialTrolley.Count;

            int positionForLeave = random.Next(0, lastProduct);

            Console.WriteLine($"Клиент решил оставить {_commercialTrolley[positionForLeave].Name} на сумму {_commercialTrolley[positionForLeave].Price} рублей.");

            _commercialTrolley.RemoveAt(positionForLeave);
        }
    }

    class Market
    {       
        private Queue<Client> _clienst = new Queue<Client>();

        public Market()
        {
            int countClients = 5;
            int capacityOfOneCommertialTrolley = 8;

            FillQueue(countClients, capacityOfOneCommertialTrolley);
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

            int total;

            productBelt = client.GiveProductsAtCheckout();

            total = GetTotalSumForProducts(productBelt);

            Console.WriteLine($"У вас {productBelt.Count} позиций на сумму {total} рублей.");

            foreach(Product product in productBelt)
            {
                Console.Write($"{product.Name} ");
            }

            Console.WriteLine();

            while (client.IsMoneyEnough() == false)
            {              
                Console.WriteLine($"У клиента {client.Money} рублей, заместо {total}.");

                client.LeaveProductAtCheckout();

                productBelt = client.GiveProductsAtCheckout();

                total = GetTotalSumForProducts(productBelt);
            }

            if (total > 0)
            {
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

        private List<Product> GiveProducts(Random random, int capacityOfCommertialTrolley) 
        {
            List<Product> allProducts = new List<Product>()
            {
                new Product("Морковка", 10),
                new Product("Пицца", 150),
                new Product("Картошка", 30),
                new Product("Водка", 100),
                new Product("Капуста", 25),
            };

            List<Product> commercialTrolley = new List<Product>();

            Product productForClient;

            int firstVariantProduct = 1;
            int lastVariantProduct = allProducts.Count; 

            int minCountProducts = 1;
            int maxCountProducts = capacityOfCommertialTrolley;

            int countProducts = random.Next(minCountProducts, maxCountProducts);

            for (int i = 0; i < countProducts; i++)
            {
                productForClient = allProducts[random.Next(firstVariantProduct, lastVariantProduct)];
                commercialTrolley.Add(new Product(productForClient.Name, productForClient.Price));
            }

            return commercialTrolley;
        }

        private void FillQueue(int countClient, int capacityOfCommertialTrolley)
        {
            Random random = new Random();

            for (int i = 0; i < countClient; i++)
            {
                _clienst.Enqueue(new Client(GiveProducts(random, capacityOfCommertialTrolley), random));
            }
        }
    }

    class Product
    {
        public Product (string name, int price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; }
        public int Price { get; }
    }
}
