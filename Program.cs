using System;
using System.Collections.Generic;
using System.Linq;

namespace BookManagementSystem
{
    //Book entity
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }

        public Book(string title, string author, int year)
        {
            Title = title;
            Author = author;
            PublicationYear = year;
        }

        public override string ToString()
        {
            return $"'{Title}' by {Author} [Year: {PublicationYear}] ";
        }
    }

    // BookManager 
    public class BookManager
    {
        // I am using in-memory db
        private List<Book> _books = new List<Book>();

        public void AddBook(Book book)
        {
            _books.Add(book);
        }

        public List<Book> GetAllBooks()
        {
            return _books;
        }

        public Book SearchByTitle(string title)
        {
            // Case-insensitive search
            return _books.FirstOrDefault(b =>
                b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
        }
    }


    // 3. User Interface
    class Program
    {
        private static BookManager _manager = new BookManager();

        static void Main(string[] args)
        {
            bool isRunning = true;

            Console.WriteLine("Book Management System");

            while (isRunning)
            {
                // Main Menu Component
                Console.WriteLine("\nPlease select :");
                Console.WriteLine("1. Add a New Book");
                Console.WriteLine("2. View All Books");
                Console.WriteLine("3. Search for a Book");
                Console.WriteLine("4. Exit");
                Console.Write(" Your Selection: ");

                string selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":
                        UI_AddBook();
                        break;
                    case "2":
                        UI_ListBooks();
                        break;
                    case "3":
                        UI_SearchBook();
                        break;
                    case "4":
                        isRunning = false;
                        Console.WriteLine("Exiting great application. See you  soon :)!");
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
            }
        }

        //  method  - Add Book 
        static void UI_AddBook()
        {
            Console.WriteLine("\nAdd New Book");

            // Validation 1: Ensure Title is not empty
            string title = GetValidString("Enter Title: ");

            // Validation 2: Ensure Author is not empty
            string author = GetValidString("Enter Author: ");

            // Validation 3: Ensure Year is a valid number
            int year = GetValidYearInt("Enter Publication Year: ");

            Book newBook = new Book(title, author, year);
            _manager.AddBook(newBook);

            Console.WriteLine("Book added successfully!");
        }

        // Method List Books
        static void UI_ListBooks()
        {
            Console.WriteLine("\nBook List");
            var books = _manager.GetAllBooks();

            if (books.Count == 0)
            {
                Console.WriteLine("No books found in the library.");
            }
            else
            {
                foreach (var book in books)
                {
                    Console.WriteLine(book.ToString());
                }
            }
        }

        //  Method :  Search Book 
        static void UI_SearchBook()
        {
            Console.WriteLine("\n Search Book ");
            string searchTitle = GetValidString("Enter exact title to search: ");

            var result = _manager.SearchByTitle(searchTitle);

            if (result != null)
            {
                Console.WriteLine("Book Found:");
                Console.WriteLine(result.ToString());
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }

        // 4. Validation
        static string GetValidString(string prompt)
        {
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Input cannot be empty. Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(input));
            return input;
        }

        static int GetValidYearInt(string prompt)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                // TryParse handles the exception handling logic safely
                if (int.TryParse(input, out result) && result > 0 && result <= DateTime.Now.Year + 1)
                {
                    return result;
                }

                Console.WriteLine($"Invalid input. Please enter a valid year.");
            }
        }
    }
}
