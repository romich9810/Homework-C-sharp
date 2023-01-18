using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6_7_TrainPlan
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Station station = new Station();

            station.WorkStation();
        }

        abstract class Van
        {
            public int Capacity { get; protected set; }
            public int PriceForPlace { get; protected set; }
            public  int CountOfPassengers { get; protected set; }
        }

        class Compartment: Van
        {
            public Compartment()
            {
                Capacity = 40;
                PriceForPlace = 2000;
            }
        }

        class RS: Van
        {
            public RS()
            {
                Capacity = 70;
                PriceForPlace = 1200;
            }
        }

        class Destination
        {
            private Train _train;
            
            public string StartCity { get; } = "Новокузнецк";
            public string FinishCity { get; private set; }
            public int Passengers { get; }

            public int Cash { get; private set; }

            public Destination(string finishCIty)
            {
                Random random = new Random();

                int minimalPassengers = 100;
                int maximalPassengers = 1000;

                int passengers = random.Next(minimalPassengers, maximalPassengers + 1);

                this._train = new Train(passengers);
                Passengers = passengers;

                Cash = _train.GetMoneyFromPassengers();

                FinishCity = finishCIty;
            }

            public int GetCountOFVans()
            {
                return _train.GetCountOfVans();
            }

            public int GetCountOfPassengersInRS()
            {
                return _train.CountOfPeoplesInRS;
            }

            public int GetCountOfPassengersInCompartments()
            {
                return _train.CountOfPeopleInCompartments;
            }
        }

        class Station
        {
            private List<Destination> _destinations = new List<Destination>();

            public int CashFromAllDestination { get; set; } = 0;

            public void ShowInfo()
            {
                if (_destinations.Count == 0)
                {
                    Console.WriteLine("Направлений пока нет.\n\n");
                }
                else
                {
                    for (int i = 0; i < _destinations.Count; i++)
                    {
                        int number = i + 1;
                        Console.WriteLine($"Номер направления: {number} направление - \"{_destinations[i].StartCity}-{_destinations[i].FinishCity}\" " +
                            $"сборы - {_destinations[i].Cash} рублей, к-во вагонов - {_destinations[i].GetCountOFVans()} кол-во пасажиров в купе - {_destinations[i].GetCountOfPassengersInCompartments()}" +
                            $", кол-во пассажиров в плацкарте - {_destinations[i].GetCountOfPassengersInRS()}.\n\n");
                    }
                }
            }

            public void AddDestination()
            {
                string finishDestination;

                Console.WriteLine("Введите конечный город: ");
                finishDestination = Console.ReadLine();

                _destinations.Add(new Destination(finishDestination));

                Console.WriteLine("Маршрут добавлен.");
            }

            public void LetsGoDestination()
            {
                bool isDestinationGoSuccesfully;

                Console.WriteLine("Введите номер направления");
                isDestinationGoSuccesfully = int.TryParse(Console.ReadLine(), out int numberDestinationForUser);

                if (isDestinationGoSuccesfully & _destinations.Count >= numberDestinationForUser)
                {
                    int numberDestinationReal = numberDestinationForUser - 1;

                    CashFromAllDestination += _destinations[numberDestinationReal].Cash;
                    _destinations.RemoveAt(numberDestinationReal);
                    Console.WriteLine("Поезд отправлен по направлению.");
                }
                else
                {
                    Console.WriteLine("Неверный номер направления!");
                }
            }

            public void  ShowAllCashStation()
            {
                foreach(Destination destination in _destinations)
                {
                    CashFromAllDestination += destination.Cash;
                }

                Console.Write(CashFromAllDestination + "руб");
            }

            public void WorkStation()
            {
                const string AddDestinationCommand = "добавить направление";
                const string LetsGoDestinationCommand = "отправить направление";
                const string ExitCommand = "выход";

                const string AddDestinationCommandNumber = "1";
                const string LetsGoDestinationCommandNumber = "2";
                const string ExitCommandNumber = "3";

                bool isNeedWork = true;

                string userComamnd;

                while (isNeedWork)
                {
                    Console.SetCursorPosition(30, 0);
                    Console.Write($"Выручка станции: {CashFromAllDestination}");
                    Console.SetCursorPosition(0, 0);

                    Console.WriteLine($"Добро пожаловать на станцию!\n");
                    ShowInfo();

                    Console.WriteLine($"{AddDestinationCommandNumber} - {AddDestinationCommand}\n{LetsGoDestinationCommandNumber} - {LetsGoDestinationCommand}\n" +
                        $"{ExitCommandNumber} - {ExitCommand}");

                    userComamnd = Console.ReadLine();

                    switch (userComamnd)
                    {
                        case AddDestinationCommandNumber:
                            AddDestination();
                            break;

                            case LetsGoDestinationCommandNumber:
                            LetsGoDestination();
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
            }
        }

        class Train
        {
            private List<Van> _vagTrain = new List<Van>();

            public int CountOfPeopleInCompartments { get;  }
            public int CountOfPeoplesInRS { get; }

            public Train(int countOfPeoples)
            {
                Compartment compartment = new Compartment();
                RS rs = new RS();

                int countOfPartsPeople = 7;
                int countOfPartsPeopleForCompartments = 3;
                int countOfPartsPeopleForRS = 4;

                int partOfPeopleForCompartments = (countOfPeoples * countOfPartsPeopleForCompartments) / countOfPartsPeople;
                CountOfPeopleInCompartments = partOfPeopleForCompartments;

                int partOfPeopelForRS = (countOfPeoples * countOfPartsPeopleForRS) / countOfPartsPeople;
                CountOfPeoplesInRS = partOfPeopelForRS;

                int countOfVansCompartments;
                int countOfVansRS;

                if (partOfPeopleForCompartments % compartment.Capacity == 0)
                {
                    countOfVansCompartments = partOfPeopleForCompartments / compartment.Capacity;
                }
                else
                {
                    countOfVansCompartments = partOfPeopleForCompartments / compartment.Capacity + 1;
                }

                if (partOfPeopelForRS % rs.Capacity == 0)
                {
                    countOfVansRS = partOfPeopelForRS / rs.Capacity;
                }
                else
                {
                    countOfVansRS = partOfPeopelForRS / rs.Capacity + 1;
                }

                for (int i = 0; i < countOfVansCompartments; i++)
                {
                    _vagTrain.Add(new Compartment());
                }

                for (int i = 0; i < countOfVansRS; i++)
                {
                    _vagTrain.Add(new RS());
                }
            }

            public int GetMoneyFromPassengers()
            {
                Compartment compartment = new Compartment();
                RS rs = new RS();

                int cash = (compartment.PriceForPlace * CountOfPeopleInCompartments) + (rs.PriceForPlace * CountOfPeoplesInRS);

                return cash;
            }

            public int GetCountOfVans()
            {
                return _vagTrain.Count();
            }
        }
    }
}
