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
                new Prisoner("Уиллям Фрод",TypeOfCrimes.Theft),
                new Prisoner("Джа Джа Бинкс", TypeOfCrimes.Rape),
                new Prisoner("Литий Лит Натриев", TypeOfCrimes.Murder),
                new Prisoner("Джек Николс", TypeOfCrimes.AntiGoverment),
                new Prisoner("Бильбо Кейтр", TypeOfCrimes.Theft),
                new Prisoner("Владимир Глоб", TypeOfCrimes.AntiGoverment)
            };

            TypeOfCrimes typeForAmnesty = TypeOfCrimes.AntiGoverment;

            Console.WriteLine("Список заключенных до амнистии: \n");

            foreach (Prisoner prisoner in prisoners)
            {
                Console.WriteLine($"{prisoner.NameLastName} - {prisoner.TypeOfCrime}");
            }

            prisoners = prisoners.Where(prisoner => prisoner.TypeOfCrime != typeForAmnesty).ToList();

            Console.WriteLine("\nПосле амнистии: \n");

            foreach(Prisoner prisoner in prisoners)
            {
                Console.WriteLine($"{prisoner.NameLastName} - {prisoner.TypeOfCrime}");
            }
        }
    }

    enum TypeOfCrimes
    {
        Theft, Murder, Rape, AntiGoverment
    }

    class Prisoner
    { 
        public Prisoner(string nameLastName, TypeOfCrimes typeOfCrime)
        {
            NameLastName = nameLastName;
            TypeOfCrime = typeOfCrime;
        }

        public string NameLastName { get; private set; }
        public TypeOfCrimes TypeOfCrime { get; private set; }
    }
}
