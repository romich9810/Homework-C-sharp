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
                new Prisoner("Уиллям Фрод",TypeOfCrime.Theft),
                new Prisoner("Джа Джа Бинкс", TypeOfCrime.Rape),
                new Prisoner("Литий Лит Натриев", TypeOfCrime.Murder),
                new Prisoner("Джек Николс", TypeOfCrime.Anti_Goverment),
                new Prisoner("Бильбо Кейтр", TypeOfCrime.Theft),
                new Prisoner("Владимир Глоб", TypeOfCrime.Anti_Goverment)
            };

            TypeOfCrime typeForAmnesty = TypeOfCrime.Anti_Goverment;

            Console.WriteLine("Список заключенных до амнистии: \n");

            foreach (Prisoner prisoner in prisoners)
            {
                Console.WriteLine($"{prisoner.NameLastName} - {prisoner.TypeOfCrime}");
            }

            var prisonersAfterAmnesty = prisoners.Where(prisoner => prisoner.TypeOfCrime != typeForAmnesty);

            Console.WriteLine("\nПосле амнистии: \n");

            foreach(var prisoner in prisonersAfterAmnesty)
            {
                Console.WriteLine($"{prisoner.NameLastName} - {prisoner.TypeOfCrime}");
            }
        }
    }

    enum TypeOfCrime
    {
        Theft, Murder, Rape, Anti_Goverment
    }

    class Prisoner
    { 
        public Prisoner(string nameLastName, TypeOfCrime typeOfCrime)
        {
            NameLastName = nameLastName;
            TypeOfCrime = typeOfCrime;
        }

        public string NameLastName { get; private set; }
        public TypeOfCrime TypeOfCrime { get; private set; }
    }
}
