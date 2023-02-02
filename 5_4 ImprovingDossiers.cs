using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_4_ImprovingDossiers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string NumberAddDossier = "1";
            const string NumberShowAllDossiers = "2";
            const string NumberDeleteDossier = "3";
            const string NumberExit = "4";

            string addToDossier = "добавить досье";
            string showAllDossiers = "вывести все досье";
            string deleteToDossier = "удалить досье";
            string exit = "выход";

            Dictionary<string, string> dossiers = new Dictionary<string, string>();

            bool isWorking = true;

            string commandFromUser;

            while (isWorking)
            {
                Console.WriteLine("***Добро пожаловать в редактор досье!***\n\nВыберите команду: ");
                Console.Write($"{NumberAddDossier} - {addToDossier}" +
                    $"\n{NumberShowAllDossiers} - {showAllDossiers}\n" +
                    $"{NumberDeleteDossier} - {deleteToDossier}" +
                    $"\n{NumberExit} - {exit}\n");

                commandFromUser = Console.ReadLine();

                switch (commandFromUser)
                {
                    case NumberAddDossier:
                        AddDossier(dossiers);
                        break;

                    case NumberShowAllDossiers:
                        ShowDossiers(dossiers);
                        break;

                    case NumberDeleteDossier:
                        DeleteDossier(dossiers);
                        break;

                    case NumberExit:
                        isWorking = false;
                        break;

                    default:
                        Console.WriteLine("Неверная команда. Попробуйте ещё раз.");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        static void AddDossier(Dictionary<string, string> dossiers)
        {
            string userName;
            string userPost;

            Console.Clear();

            Console.WriteLine("Введите ФИО: ");
            userName = Console.ReadLine();
            Console.WriteLine("Введите должность: ");
            userPost = Console.ReadLine();

            if (dossiers.ContainsKey(userName) == false)
            {
                dossiers.Add(userName, userPost);

                Console.WriteLine($"Данные успешно внесены.");
            }
            else
            {
                Console.WriteLine($"{userName} -  уже есть в досье!");
            }
        }

        static void ShowDossiers(Dictionary<string, string> dossiers)
        {
            Console.WriteLine("Все имеющиеся досье:\n");

            foreach (var dossier in dossiers)
            {
                Console.WriteLine($"{dossier.Key} -  {dossier.Value}");
            }
        }

        static void DeleteDossier(Dictionary<string, string> dossiers)
        {
            string nameForDeleting;

            ShowDossiers(dossiers);

            Console.WriteLine("Введите ФИО удаления досье: ");
            nameForDeleting = Console.ReadLine();

            if (dossiers.ContainsKey(nameForDeleting))
            {
                dossiers.Remove(nameForDeleting);
                Console.WriteLine("Досье успешно удалено.");
            }
            else
            {
                Console.WriteLine("Такого досье не найдено либо вы ввели неверный индекс.");
            }
        }
    }
}
