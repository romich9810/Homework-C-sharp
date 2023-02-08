using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_5_Stews
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FoodSet foodSet = new FoodSet();

            foodSet.ShowExpiredStews();
        }
    }

    class Stew
    {
        public Stew(string name, int issureYear, int expiryYear)
        {
            Name = name;
            IssureYears = issureYear;
            ExpiryYear = expiryYear;
        }

        public string Name { get; private set; }
        public int IssureYears { get; private set; }
        public int ExpiryYear { get; private set; }
    }

    class FoodSet
    {
        private List<Stew> _stews;
        private int _yearNow = 2023;

        public FoodSet()
        {
            Random random = new Random();

            int sizeOfSet = 7;

            _stews = new List<Stew>();

            _stews = GiveRandomStews(random, sizeOfSet);
        }

        public void ShowExpiredStews()
        {
            var expiredStews = _stews.Where(stew => stew.ExpiryYear < _yearNow).ToList();

            Console.WriteLine("Вся тушенка:\n");

            ShowStews(_stews);

            Console.WriteLine("\nВся тушёнка с истёкшим сроком годности:\n");

            ShowStews(expiredStews);
        }

        private void ShowStews(List<Stew> stews) 
        {
            foreach(Stew stew in stews)
            {
                Console.WriteLine($"{stew.Name}, от {stew.IssureYears} года до {stew.ExpiryYear} года");
            }
        }

        private List<Stew> GiveRandomStews(Random random, int size)
        {
            List<string> namesStew = new List<string>()
            {
                "Барнаульский пищевик", "Улан-Уденский мясокомбинат", "Новосибирский молочно-мясной комбинат"
            };

            int[] issureYears = new int[]
            {
                1998, 2015, 2022
            };

            List<Stew> stews = new List<Stew>();

            int shelfLife = 5;

            for (int i = 0; i < size; i++)
            {
                int issureYear = issureYears[random.Next(0, issureYears.Length)];
                int expiryYear = issureYear + shelfLife;

                stews.Add(new Stew(namesStew[random.Next(0, namesStew.Count)], issureYear, expiryYear));
            }

            return stews;
        }
    }
}
