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

            string AddToDossier = "добавить досье";
            string ShowAllDossiers = "вывести все досье";
            string DeleteToDossier = "удалить досье";
            string Exit = "выход";

            Dictionary<string, string> dossiers = new Dictionary<string, string>();

            bool isWorking = true;
            string commandFromUser;

            while (isWorking)
            {
                Console.WriteLine("***Добро пожаловать в редактор досье!***\n\nВыберите команду: ");
                Console.Write($"{NumberAddDossier} - {AddToDossier}" +
                    $"\n{NumberShowAllDossiers} - {ShowAllDossiers}\n" +
                    $"{NumberDeleteDossier} - {DeleteToDossier}" +
                    $"\n{NumberExit} - {Exit}\n");

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
                        Console.Write("Завершение работы программы.");
                        break;

                    default:
                        Console.WriteLine("Неверная команда. Попробуйте ещё раз.");
                        break;
                }

                Console.Clear();
            }
        }

        static void AddDossier(Dictionary<string, string> dossiers)
        {
            const string NumberAddAgain = "1";
            string CommandAddAgain = "добавить ещё";
            string CommandBack = "любая клавиша - к главному меню";

            string command;
            bool isNeedContune = true;

            string userName;
            string userPost;

            while (isNeedContune == true)
            {
                Console.Clear();

                Console.WriteLine("Введите ФИО: ");
                userName = Console.ReadLine();
                Console.WriteLine("Введите должность: ");
                userPost = Console.ReadLine();

                if (dossiers.ContainsKey(userName) == false)
                {
                    dossiers.Add(userName, userPost);
                }
                else
                {
                    Console.WriteLine($"{userName} -  уже есть в досье!");
                    Console.ReadKey();
                }                

                Console.Clear();
                Console.WriteLine($"Данные успешно внесены.\n{CommandBack}\n{NumberAddAgain} - {CommandAddAgain}");
                command = Console.ReadLine();

                if (command != NumberAddAgain)
                {
                    isNeedContune = false;
                }
            }
        }

        static void ShowDossiers(Dictionary<string, string> dossiers)
        {
            Console.WriteLine("Все имеющиеся досье:\n");

            foreach (var dossier in dossiers)
            {
                Console.WriteLine($"{dossier.Key} -  {dossier.Value}");
            }

            Console.WriteLine("Для возвращения нажмите любую клавишу.");
            Console.ReadKey();
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
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Такого досье не найдено либо вы ввели неверный индекс.");
                Console.ReadKey();
            }
        }
    }
}
