using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6_12_AutoServise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Garage garage = new Garage();

            garage.Service();
        }
    }

    enum DetailsName { FuelPump, DetectorICE, SteeringRack, Catalyst, DiskBrakes }

    class AllDetails
    {
        private List<DetailsName> _kinds = new List<DetailsName>() 
            { DetailsName.FuelPump, DetailsName.DetectorICE, DetailsName.SteeringRack, DetailsName.Catalyst, DetailsName.DiskBrakes };

        public AllDetails() { }

       public DetailsName GiveOneRandomKind() // мб рандом передать в метод
        {
            Random random = new Random();

            return _kinds[random.Next(0, _kinds.Count)];
        }

        public List<DetailsName> GiveAllKinds()
        {
            List<DetailsName> allKinds = new List<DetailsName>();

            for (int i = 0; i < _kinds.Count; i++)
            {
                allKinds.Add(_kinds[i]);
            }

            return allKinds;
        }
    }

    class Garage
    {
        private const int _fineForLoseClient = 500;

        private Dictionary<DetailsName, int> _priceList = new Dictionary<DetailsName, int>()
        {
            { DetailsName.FuelPump, 1000 },
            { DetailsName.Catalyst, 3000},
            { DetailsName.DiskBrakes, 1500},
            { DetailsName.SteeringRack, 5000 },
            { DetailsName.DetectorICE, 2000}
        };

        private Queue<Car> _clients;
        private Warehouse _detailsBase;
        private int _balanse;

        public Garage()
        {
            int countQueue = 5;

            _detailsBase = new Warehouse();

            Fill(countQueue);

            _balanse = 1000;
        }

        public int Balance 
        {
            get
            {
                if (_balanse < 0)
                {
                    Console.WriteLine(" Вы влезли в кредит.");
                }

                return _balanse;
            } 
            
            private set { } 
        }

        public void Service()
        {
            for (int i = 0; i < _clients.Count; i++)
            {
                int indexCliemt = i + 1;

                Console.WriteLine($"На очереди {indexCliemt}-й из {_clients.Count} клиент.\nНажмите любую клавишу, чтобы его обслужить.");

                Console.ReadKey();
                Console.Clear();

                ServiceOneClient();

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void ServiceOneClient()
        {
            Car client = _clients.Dequeue();
            Detail detail = _detailsBase.GiveByApplication(client.GiveKindOfProblem());

            if (detail != null)
            {
                client.ChangeDetail(detail);

                if (client.IsStartSuccsesfully())
                {
                    int priсeForWork = _priceList[detail.Name];
                    int priсeForDetail = detail.Price;

                    int total = priсeForWork + priсeForDetail;

                    Console.WriteLine($"Дело сделано. С вас {total} рублей за всё.");

                    Balance += total;
                }
                else
                {
                    AllDetails allDetails = new AllDetails();
                    Detail reasonOfFail;

                    int moralLoss;

                    do
                    {
                        reasonOfFail = new Detail(allDetails.GiveOneRandomKind());
                    }
                    while (reasonOfFail.Name != detail.Name);

                    moralLoss = reasonOfFail.Price;

                    Balance -= moralLoss;

                    Console.WriteLine($"Причиной поломки оказалось {reasonOfFail.Name}. {moralLoss} было выплачено клиенту в качестве морального ущерба.");
                }

            }
            else
            {
                Balance -= _fineForLoseClient;

                Console.WriteLine($"На складе не оказалось нужной детали. Был выплачен штраф клиенту - {_fineForLoseClient} рублей.");
            }
        }

        private void Fill(int size)
        {
            _clients = new Queue<Car>();

            for (int i = 0; i < size; i++)
            {
                _clients.Enqueue(new Car());
            }
        }
    }

    class Car
    {
        private Detail _detail;

        public Car()
        {
            AllDetails allDetails = new AllDetails();

            _detail = new Detail (allDetails.GiveOneRandomKind(), false);
        }

        public bool IsStartSuccsesfully() 
        {
            Random random = new Random();

            int minPercentStart = 1;
            int maxPercentStart = 100;

            int percentForFailStart = 15;

            int percentStart = random.Next(minPercentStart, maxPercentStart + 1);

            if (percentStart <= percentForFailStart)
            {              
                Console.WriteLine("Автомобиль не запустился.");

                return false;
            }
            else
            {
                Console.WriteLine("Автомобиль работает!");

                return true;
            }        
        }

        public DetailsName GiveKindOfProblem()
        {
            return _detail.Name;
        }

        public void ChangeDetail(Detail detail)
        {
            _detail = detail;
        }






    }

    class Detail
    {
        private Dictionary<DetailsName, int> _price = new Dictionary<DetailsName, int>()
        {
            { DetailsName.FuelPump, 300 },
            { DetailsName.Catalyst, 2500},
            { DetailsName.DiskBrakes, 1200},
            { DetailsName.SteeringRack, 8000},
            { DetailsName.DetectorICE, 1500}
        };

        public Detail(DetailsName name)
        {
            IsWorkable = true;
            Name = name;
            Price = GivePrice(Name);
        }

        public Detail(DetailsName name, bool isWorkable)
        {
            IsWorkable = isWorkable;
            Name = name;
            Price = GivePrice(Name);
        }

        public bool IsWorkable { get; private set; }
        public int Price { get; private set; }
        public DetailsName Name { get; private set; }

        private int GivePrice(DetailsName name)
        {
            return _price[name];
        }
    }

    class Warehouse
    {
        private Dictionary <DetailsName,List<Detail>> _details;

        public Warehouse()
        {
            Random random = new Random();
            AllDetails allDetails = new AllDetails();

            List<DetailsName> allKindDetails = allDetails.GiveAllKinds();

            int maxCountDetail = 3;
            int minCountDetail = 0;

            int countDetail;

            _details = new Dictionary<DetailsName, List<Detail>>();

            for (int i = 0; i < allKindDetails.Count; i++)
            {
                _details.Add(allKindDetails[i], new List<Detail>());

                countDetail = random.Next(minCountDetail, maxCountDetail + 1);

                for (int j = 0; i < countDetail; j++)
                {
                    _details[allKindDetails[i]].Add(new Detail(allKindDetails[i]));
                }
            }
        }

        public Detail GiveByApplication(DetailsName nameDetail)
        {
            Detail detail = null;

            if (_details[nameDetail].Count > 0)
            {
                detail = _details[nameDetail][0];

                _details[nameDetail].RemoveAt(0);
            }
            else
            {
                Console.WriteLine($"На складе нет {nameDetail}!");
            }

            return detail;
        }
    }
}
