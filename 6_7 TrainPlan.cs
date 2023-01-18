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
    }

    class Van
    {
        public int Capacity { get; } = 45;
    }
    class Train
    {
        private List<Van> _vans = new List<Van>();

        public Train(int countOfPassengers)
        {
            int countOfVan = CalculateCountOfVan(countOfPassengers);

            for (int i = 0; i < countOfVan; i++)
            {
                _vans.Add(new Van());
            }
        }

        public int GetCountOfVans()
        {
            return _vans.Count;
        }

        private int CalculateCountOfVan(int countOfPassengers)
        {
            Van van = new Van();

            if (countOfPassengers % van.Capacity == 0)
            {
                return countOfPassengers / van.Capacity;
            }
            else
            {
                return ((countOfPassengers / van.Capacity) + 1);
            }
        }
    }

    class Destination
    {
        private Train _train;

        public Destination(string finishPoint)
        {
            Random random = new Random();

            int maximalPassengers = 1000;
            int minimalPassengers = 100;

            int passengers = random.Next(minimalPassengers, maximalPassengers + 1);

            _train = new Train(passengers);

            Passengers = passengers;
            FinishPoint = finishPoint;
        }

        public string StartPoint { get; } = "Новокузнецк";
        public string FinishPoint { get; private set; }
        public int Passengers { get; }

        public int GetCountOfVans()
        {
            return _train.GetCountOfVans();
        }
    }

    class Station
    {
        private List<Destination> _destinations = new List<Destination>();

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

        private void ShowInfo()
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

                    Console.WriteLine($"Номер направления: {number} направление - \"{_destinations[i].StartPoint}-{_destinations[i].FinishPoint}\" " +
                           $"к-во вагонов - {_destinations[i].GetCountOfVans()} кол-во пасажиров - {_destinations[i].Passengers}.\n\n");
                }
            }
        }

        private void AddDestination()
        {
            string finishDestination;

            Console.WriteLine("Введите конечный город: ");
            finishDestination = Console.ReadLine();

            _destinations.Add(new Destination(finishDestination));

            Console.WriteLine("Маршрут добавлен.");
        }

        private void LetsGoDestination()
        {
            bool isDestinationGoSuccesfully;

            Console.WriteLine("Введите номер направления");
            isDestinationGoSuccesfully = int.TryParse(Console.ReadLine(), out int numberDestinationForUser);

            if (isDestinationGoSuccesfully & _destinations.Count >= numberDestinationForUser)
            {
                int numberDestinationReal = numberDestinationForUser - 1;

                _destinations.RemoveAt(numberDestinationReal);
                Console.WriteLine("Поезд отправлен по направлению.");
            }
            else
            {
                Console.WriteLine("Неверный номер направления!");
            }
        }
    }
}
