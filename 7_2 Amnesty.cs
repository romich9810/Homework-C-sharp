using System;
using System.Collections.Generic;
using System.Linq;

namespace _7_2_Amnesty
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Prisoner> prisoners = new List<Prisoner>()
            {
                new Prisoner("Уиллям Фрод",TypesOfCrimes.Theft),
                new Prisoner("Джа Джа Бинкс", TypesOfCrimes.Rape),
                new Prisoner("Литий Лит Натриев", TypesOfCrimes.Murder),
                new Prisoner("Джек Николс", TypesOfCrimes.AntiGoverment),
                new Prisoner("Бильбо Кейтр", TypesOfCrimes.Theft),
                new Prisoner("Владимир Глоб", TypesOfCrimes.AntiGoverment)
            };

            TypesOfCrimes typeForAmnesty = TypesOfCrimes.AntiGoverment;

            Console.WriteLine("Список заключенных до амнистии: \n");

            ShowPrisoners(prisoners);

            prisoners = prisoners.Where(prisoner => prisoner.TypeOfCrime != typeForAmnesty).ToList();

            Console.WriteLine("\nПосле амнистии: \n");

            ShowPrisoners(prisoners);
        }

        static void ShowPrisoners(List<Prisoner> prisoners)
        {
            foreach (Prisoner prisoner in prisoners)
            {
                Console.WriteLine($"{prisoner.NameLastName} - {prisoner.TypeOfCrime}");
            }
        }
    }

    enum TypesOfCrimes
    {
        Theft, Murder, Rape, AntiGoverment
    }

    class Prisoner
    { 
        public Prisoner(string nameLastName, TypesOfCrimes typeOfCrime)
        {
            NameLastName = nameLastName;
            TypeOfCrime = typeOfCrime;
        }

        public string NameLastName { get; private set; }
        public TypesOfCrimes TypeOfCrime { get; private set; }
    }
}
