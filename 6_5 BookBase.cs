using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6_5_BookBase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataBase bookBase = new DataBase();
            bookBase.Work();
        }

        class Book
        {
            public Book(string name, string author, int yearOfRelease)
            {
                Name = name;
                Author = author;
                YearOfRelease = yearOfRelease;
            }

            public string Name { get; }
            public string Author { get; }
            public int YearOfRelease { get; }
        }

        class DataBase
        {
            private List<Book> _listBooks = new List<Book>();
            private int _yearNow = 2023;

            public DataBase()
            {
                _listBooks = new List<Book>() {new Book("Война и мир", "Толстой Л.Н.", 1865), new Book("Капитанская дочка","Пушкин А. С.", 1836), new Book("1984", "Оруэлл Д.", 1949) };
            }

            public void Work()
            {
                const string ShowAllBooksNumber = "1";
                const string AddBookNumber = "2";
                const string DeleteBookNumber = "3";
                const string FindBookByParametr = "4";
                const string ExitCommandNumber = "5";

                string ShowAllBooksCommand = "показать все книги в базе";
                string AddBookCommand = "добавить книгу в базу";
                string DeleteBookCommand = "удалить книгу из базы";
                string FindByParametrsCommand = "найти книгу по параметрам";
                string ExitCommand = "выход";

                bool isNeedWorkElse = true;

                string userCommand;

                while (isNeedWorkElse)
                {
                    Console.WriteLine($"Добро пожаловать в базу книг!" +
                        $"\n\n{ShowAllBooksNumber} - {ShowAllBooksCommand}" +
                        $"\n{AddBookNumber} - {AddBookCommand}" +
                        $"\n{DeleteBookNumber} - {DeleteBookCommand}" +
                        $"\n{FindBookByParametr} - {FindByParametrsCommand}" +

                        $"\n{ExitCommandNumber} - {ExitCommand}");
                    userCommand = Console.ReadLine();

                    switch (userCommand)
                    {
                        case ShowAllBooksNumber:
                            ShowBooks();
                            break;

                        case AddBookNumber:
                            AddBook();
                            break;

                        case DeleteBookNumber:
                            DeleteBook();
                            break;

                        case FindBookByParametr:

                            const string FindBookByYearNumber = "1";
                            const string FindBookByAuthorCommandNumber = "2";
                            const string FindBookByNameOfBookCommandNumber = "3";
                            const string GoPreviousMenuCommandNumber = "4";

                            string FindBookByYearCommand = "найти книгу по году издания";
                            string FindBookByAuthorCommand = "найти книгу по автору";
                            string FindBookByNameOfBookCommand = "Найти книгу по названию";
                            string GoPreviousMenuCommand = "вернуться назад в главное меню";

                            Console.Clear();

                            Console.WriteLine($"Выберите параметра для поиска: " +
                                $"\n{FindBookByYearNumber} - {FindBookByYearCommand}" +
                                $"\n{FindBookByAuthorCommandNumber} - {FindBookByAuthorCommand}" +
                                $"\n{FindBookByNameOfBookCommandNumber} - {FindBookByNameOfBookCommand}" +
                                $"\n{GoPreviousMenuCommandNumber} - {GoPreviousMenuCommand}");

                            userCommand = Console.ReadLine();

                            switch (userCommand)
                            {
                                case FindBookByYearNumber:
                                    FindBookByYear();
                                    break;

                                case FindBookByAuthorCommandNumber:
                                    FindBookByAuthor();
                                    break;

                                case FindBookByNameOfBookCommandNumber:
                                    FindBookByItsName();
                                    break;

                                case GoPreviousMenuCommandNumber:
                                    Console.WriteLine("Нажмите любую клавишу.");
                                    break;

                                default:
                                    Console.WriteLine("Неверная команда!");
                                    Console.ReadKey();
                                    break;
                            }

                            break;

                        case ExitCommandNumber:
                            isNeedWorkElse = false;
                            break;

                        default:
                            Console.WriteLine("Неверная команда!");
                            Console.ReadKey();
                            break;
                    }

                    Console.ReadKey();
                    Console.Clear();
                }
            }

            private void AddBook()
            {
                string nameOfBook;
                string authorOfBook;
                bool isYearOfBookCorrect;

                Console.WriteLine("Введите название книги: ");
                nameOfBook = Console.ReadLine();

                Console.WriteLine("Введите автора книги: ");
                authorOfBook = Console.ReadLine();

                Console.WriteLine("Введите год издания: ");
                isYearOfBookCorrect = int.TryParse(Console.ReadLine(), out int yearOfBook);

                if (isYearOfBookCorrect & ((yearOfBook > 0) && (yearOfBook <= _yearNow)))
                {
                    _listBooks.Add(new Book(nameOfBook, authorOfBook, yearOfBook));
                    Console.WriteLine($"Книга {nameOfBook} добавлена в базу.");
                }
                else
                {
                    Console.WriteLine("Некорректный год книги!");
                }
            }

            private void DeleteBook()
            {
                string nameOfBookForDelete;
                bool isDeleteCorrect = false;

                Console.WriteLine("Введите название книги для удаления: ");
                nameOfBookForDelete = Console.ReadLine();

                for (int i = 0; i < _listBooks.Count; i++)
                {
                    if (_listBooks[i].Name.ToLower() == nameOfBookForDelete.ToLower())
                    {
                        _listBooks.RemoveAt(i);
                        Console.WriteLine("Книга удалена");
                        isDeleteCorrect = true;
                    }
                }

                if (isDeleteCorrect == false)
                {
                    Console.WriteLine("Книга не найдена! Возможна ошибка в вводе названия для  удаления.");
                }
            }

            private void ShowBooks()
            {
                Console.WriteLine("Все имеющиеся книги:\n ");

                foreach (Book book in _listBooks)
                {
                    Console.WriteLine($"Название:{book.Name}, автор:  {book.Author}, год издания: {book.YearOfRelease}");
                }

                Console.WriteLine();
            }

            private void FindBookByYear()
            {
                List<Book> foundedBooks = new List<Book>();

                bool isYearWritedCorreclty;

                Console.WriteLine("Введите год произведения для поиска:");
                isYearWritedCorreclty = int.TryParse(Console.ReadLine(), out int yearOfBook);

                if (isYearWritedCorreclty && yearOfBook > 0)
                {
                    foreach (Book book in _listBooks)
                    {
                        if (book.YearOfRelease == yearOfBook)
                        {
                            foundedBooks.Add(book);
                        }
                    }

                    ShowFoundedBooks(foundedBooks);
                }
                else
                {
                    Console.WriteLine("Неверный год издания!");
                }
            }

            private void FindBookByAuthor()
            {
                List<Book> foundedBooks = new List<Book>();

                string nameOrAuthor;

                Console.WriteLine("Введите Фамилию И.О. автора:");
                nameOrAuthor = Console.ReadLine();

                foreach (Book book in _listBooks)
                {
                    if (book.Author.ToLower() == nameOrAuthor.ToLower())
                    {
                        foundedBooks.Add(book);
                    }
                }

                ShowFoundedBooks(foundedBooks);
            }

            private void FindBookByItsName()
            {
                List<Book> foundedBooks = new List<Book>();

                string nameOrBook;

                Console.WriteLine("Введите название произведения:");
                nameOrBook = Console.ReadLine();

                foreach (Book book in _listBooks)
                {
                    if (book.Name.ToLower() == nameOrBook.ToLower())
                    {
                        foundedBooks.Add(book);
                    }
                }

                ShowFoundedBooks(foundedBooks);
            }

            private void ShowFoundedBooks(List<Book> listBooks)
            {
                Console.WriteLine("Все книги, что подходят по параметрам:\n ");

                foreach (Book book in listBooks)
                {
                    Console.WriteLine($"Название:{book.Name}, автор:  {book.Author}, год издания: {book.YearOfRelease}");
                }

                Console.WriteLine();
            }
        }
    }
}
