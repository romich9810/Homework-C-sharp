using System;
using System.Collections.Generic;
using System.Linq;

namespace _7_3_Hospital
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Hospital hospital = new Hospital();

            hospital.Work();
        }
    }

    class Hospital
    {
        private List<Patient> _patients = new List<Patient>()
        {
            new Patient("Иванов Иван Иваныч", 54, "тахикардия"),
            new Patient("Жуков Константин Игоревич", 32, "диарея"),
            new Patient("Вилко Лилия Фёдоровна", 23,"злокачественное образование"),
            new Patient("Салихов Мефедрон Алуевич", 56, "диарея"),
            new Patient("Щунькина Елена Сергеевна", 45, "ОРЗ"),
            new Patient("Ли Вун Шень", 43, "гонорея"),
            new Patient("Хулио Петро Сантос Фердинант", 24, "диарея")
        };

        public Hospital() { }

        public void Work()
        {
            const string ShowAllPatientsCommandNumber = "1";
            const string SortByNameCommandNumber = "2";
            const string SortByAgeCommandNumber = "3";
            const string ShowByDiseaseCommandNumber = "4";
            const string ExitCommandNumber = "5";

            string showAllPatientsCommand = "показать всех пациентов";
            string sortByNameCommand = "отсортировать по имени";
            string sortByAgeCommand = "отсортировать по возрасту";
            string showByDiseaseCommand = "показать по болезни всех больных";
            string exitCommand = "выход";

            bool isNeedWork = true;

            string userCommand;

            while (isNeedWork)
            {
                Console.WriteLine("Добро пожаловать в больницу! \n");
                Console.WriteLine($"{ShowAllPatientsCommandNumber} - {showAllPatientsCommand}\n" +
                    $"{SortByNameCommandNumber} - {sortByNameCommand}\n" +
                    $"{SortByAgeCommandNumber} - {sortByAgeCommand}\n" +
                    $"{ShowByDiseaseCommandNumber} - {showByDiseaseCommand}\n" +
                    $"{ExitCommandNumber} - {exitCommand}");

                Console.WriteLine("Введите команду: ");
                userCommand = Console.ReadLine();

                switch (userCommand)
                {
                    case ShowAllPatientsCommandNumber:
                        ShowAllPatients();
                        break;

                    case SortByNameCommandNumber:
                        ShowSortedPatientsByNames();
                        break;

                    case SortByAgeCommandNumber:
                        ShowSortedPatientsByAges();
                        break;

                    case ShowByDiseaseCommandNumber:
                        ShowByDisease();
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

        private void ShowByDisease()
        {
            string disease;

            Console.WriteLine("Введите заболевание для поиска: ");
            disease = Console.ReadLine();

            List<Patient> patients = _patients.Where(patient => patient.Disease.ToLower() == disease.ToLower()).ToList();

            Console.WriteLine("Все подходящие пациенты:\n");

            ShowPatients(patients);
        }

        private void ShowSortedPatientsByAges()
        {
            _patients = _patients.OrderBy(patient => patient.Age).ToList() ;

            Console.WriteLine("После сортировки по возрасту:\n");

            ShowPatients(_patients);
        }

        private void ShowSortedPatientsByNames()
        {
            _patients = _patients.OrderBy(patient => patient.NameLastName).ToList();

            Console.WriteLine("После сортировки по возрасту:\n");

            ShowPatients(_patients);
        }

        private void ShowAllPatients()
        {
            Console.WriteLine("Все пациенты:\n");

            ShowPatients(_patients);
        }

        private void ShowPatients(List<Patient> patients)
        {
            foreach (Patient patient in patients)
            {
                Console.WriteLine($"{patient.NameLastName}, {patient.Age} - {patient.Disease}");
            }
        }
    }

    class Patient
    {
        public Patient(string nameLastName, int age, string disease)
        {          
            NameLastName = nameLastName;
            Age = age;

            Disease = disease;
        }

        public string NameLastName { get; private set; }
        public int Age { get; private set; }
        public string Disease { get; private set; }        
    }
}
