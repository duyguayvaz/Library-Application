using LibraryManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace LibraryApp_DLL
{
    public class Class1
    {
        public void Start()
        {
            Title = "LibraryApp";
            RunMainMenu();
        }

        public void RunMainMenu()
        {
            string prompt = @" 
  **************************************************************
  *     __    _ __                          ___                *
  *    / /   (_) /_  _________ ________  __/   |  ____  ____   *
  *   / /   / / __ \/ ___/ __ `/ ___/ / / / /| | / __ \/ __ \  *
  *  / /___/ / /_/ / /  / /_/ / /  / /_/ / ___ |/ /_/ / /_/ /  *
  * /_____/_/_.___/_/   \__,_/_/   \__, /_/  |_/ .___/ .___/   *
  *                               /____/      /_/   /_/        *
  *                                                            *
  **************************************************************
 Welcome to LibraryApp.
(Use the up and down arrows to move, and the escape key to come back.) ";
            string[] options = { "Start", "About", "Exit" };
            Menu mainMenu = new Menu(prompt, options);
            int selectedIndex = mainMenu.Run();


            switch (selectedIndex)
            {
                case 0:
                    StartInfo();
                    break;
                case 1:
                    AboutInfo();
                    break;
                case 2:
                    ExitInfo();
                    break;
                
            }
        }
        private void StartInfo()
        {
            string prompt = "*LIBRARY TRANSACTIONS*";
            string[] options = { "Book Transactions", "Category Transactions", "Reader Transactions", "Borrow Book Transactions", "Book Return Transactions" };
            Menu StartInfo = new Menu(prompt, options);
            int SelectedIndex = StartInfo.Run();

            switch (SelectedIndex)
            {
                case 0:
                    BookTransactions();
                    break;
                case 1:
                    CategoryTransactions();
                    break;
                case 2:
                    ReaderTransactions();
                    break;
                case 3:
                    BorrowBookTransactions();
                    break;
                case 4:
                    BookReturnTransactions();
                    break;
            }
        }
        private void BookTransactions()
        {
            string prompt = "*ADD BOOK*";
            string[] options = { "Add Book", "Delete Book", "Edit Book", "List Book", "Search Book" };
            Menu BookTransactions = new Menu(prompt, options);
            int SelectedIndex = BookTransactions.Run();

            switch (SelectedIndex)
            {
                case 0:
                    AddBook();
                    break;
                case 1:
                    DeleteBook();
                    break;
                case 2:
                    EditBook();
                    break;
                case 3:
                    ListBook();
                    break;
                case 4:
                    SearchBook();
                    break ;
            }


        }
        private void AddBook()
        {
            Clear();
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string filename = Path.Combine(path, "library.dat");

            Book book = new Book();
            Write("Please enter the information requested below");
            Write("\n<><><><><><><><><><><><><><><><><><><><><>");
            Write("\nID:");
            book.Id = Convert.ToInt32(ReadLine());
            Write("\nBook title: ");
            book.Title = "Title: " + ReadLine();
            Write("\nBook description: ");
            book.Description = "Description: " + ReadLine();
            Write("\nBook year: ");
            book.Year = "Year: " + ReadLine();
            Write("\nBook pages: ");
            book.Pages = "Pages: " + ReadLine();
            Write("\nBook abstract: ");
            book.Abstract = "Abstract: " + ReadLine();
            Write("\nBook city: ");
            book.City = "City: " + ReadLine();
            Write("\nBook edition: ");
            book.Edition = "Edition: " + ReadLine();
            Write("\nBook publisher: ");
            book.Publisher = "Publisher: " + ReadLine();
            Write("\nBook catalogID: ");
            book.CatalogId = "CatalogID: " + ReadLine();
            Write("\nBook price: ");
            book.Price = "Price: " + ReadLine();
            Write("\nBook rack: ");
            book.RackNo = "Rack: " + ReadLine();
            Write("\nBook row: ");
            book.RowNo = "Row: " + ReadLine();
            book.Status = "In library";
            Write("\nBook enter date: ");
            book.Return = "Firstly added to library at: " + ReadLine() + "date";
            book.Given = "Given: -";
            Write("\nBook url: ");
            book.Url = "Url: " + ReadLine();
            Write("\nBook author: ");
            book.Authors.Add ("Author :" + ReadLine());
            Write("\nBook tag: ");
            book.Tags.Add ("Tag: " + ReadLine());
            Write("\nBook editor: ");
            book.Editors.Add ("Editor: " + ReadLine());
            Write("\nBook category: ");
            book.Categories.Add("Category: " + ReadLine());

           
            byte[] bookBytes = Book.BookToByteArrayBlock(book);
            FileUtility.AppendBlock(bookBytes, filename);
            WriteLine("\n*** Book added ***" +
            "\n *** Press any key to return ***");
            ReadKey();

            BookTransactions();
        }
        private void DeleteBook()
        {
            if (File.Exists("library.dat"))
            {
                using (StreamReader sr = new StreamReader(File.Open("library.dat", FileMode.Open)))
                {
                    string datalength = sr.ReadLine();
                    sr.Close();

                    string path = AppDomain.CurrentDomain.BaseDirectory;
                    string filename = Path.Combine(path, "library.dat");
                    Clear();
                    int booknumber;
                    WriteLine("Please enter number of book which do you want to delete: ");
                    booknumber = Convert.ToInt32(ReadLine());
                    FileUtility.DeleteBlock(booknumber, Book.BOOK_DATA_BLOCK_SIZE, filename);

                    do
                    {
                        byte[] nextbookbytes = FileUtility.ReadBlock(booknumber + 1, Book.BOOK_DATA_BLOCK_SIZE, filename);
                        FileUtility.UpdateBlock(nextbookbytes, booknumber, Book.BOOK_DATA_BLOCK_SIZE, filename);
                        booknumber++;
                    } while (booknumber <= (((datalength.Length) / (Book.BOOK_DATA_BLOCK_SIZE)) - 1));

                    FileUtility.DeleteBlock(((datalength.Length) / (Book.BOOK_DATA_BLOCK_SIZE)), Book.BOOK_DATA_BLOCK_SIZE, filename);
                }
            }
            else { Clear(); WriteLine("Library file couldn't found."); }
            WriteLine("Press any key to return...");
            ReadKey(true);
            BookTransactions();
        }
        private void EditBook()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string filename = Path.Combine(path, "library.dat");
            Clear();
            int booknumber;
            if (File.Exists("library.dat"))
            {
                WriteLine("Please enter number of book which do you want to edit: ");
                booknumber = Convert.ToInt32(ReadLine());


                using (StreamReader sr = new StreamReader(File.Open("library.dat", FileMode.Open)))
                {
                    string datalength = sr.ReadLine();
                    sr.Close();

                    Book book = new Book();
                    Write("Please enter book ID: ");
                    book.Id = Convert.ToInt32(ReadLine());
                    Write("\nPlease enter book title: ");
                    book.Title = "Title: " + ReadLine();
                    Write("\nPlease enter book description: ");
                    book.Description = "Description: " + ReadLine();
                    Write("\nPlease enter book year: ");
                    book.Year = "Year: " + ReadLine();
                    Write("\nPlease enter book pages: ");
                    book.Pages = "Pages: " + ReadLine();
                    Write("\nPlease enter book abstract: ");
                    book.Abstract = "Abstract: " + ReadLine();
                    Write("\nPlease enter book city: ");
                    book.City = "City: " + ReadLine();
                    Write("\nPlease enter book edition: ");
                    book.Edition = "Edition: " + ReadLine();
                    Write("\nPlease enter book publisher: ");
                    book.Publisher = "Publisher: " + ReadLine();
                    Write("\nPlease enter book catalogID: ");
                    book.CatalogId = "CatalogID: " + ReadLine();
                    Write("\nPlease enter book price: ");
                    book.Price = "Price: " + ReadLine();
                    Write("\nPlease enter book rack: ");
                    book.RackNo = "Rack: " + ReadLine();
                    Write("\nPlease enter book row: ");
                    book.RowNo = "Row: " + ReadLine();
                    book.Status = "In library";
                    Write("\nPlease enter book enter date: ");
                    book.Return = "Firstly added to library at: " + ReadLine() + "date";
                    book.Given = "Given: -";
                    Write("\nPlease enter book url: ");
                    book.Url = "Url: " + ReadLine();
                    Write("\nPlease enter book author: ");
                    book.Authors.Add("Author :" + ReadLine());
                    Write("\nPlease enter book tag: ");
                    book.Tags.Add("Tag: " + ReadLine());
                    Write("\nPlease enter book editor: ");
                    book.Editors.Add("Editor: " + ReadLine());
                    Write("\nPlease enter book category: ");
                    book.Categories.Add("Category: " + ReadLine());

                    byte[] bookBytes = Book.BookToByteArrayBlock(book);

                    FileUtility.UpdateBlock(bookBytes, booknumber, Book.BOOK_DATA_BLOCK_SIZE, filename);
                }
            }
            else { WriteLine("Library file couldn't found."); }

            WriteLine("Press any key to return...");
            ReadKey(true);
            BookTransactions();
        }
        private void ListBook()
        {
            Clear();
            if (File.Exists("library.dat"))
            {
                int i = 1;
                using (StreamReader sr = new StreamReader(File.Open("library.dat", FileMode.Open)))
                {
                    string datlength = sr.ReadLine();
                    sr.Close();
                    do
                    {
                        string path = AppDomain.CurrentDomain.BaseDirectory;
                        string filename = Path.Combine(path, "library.dat");


                        byte[] bookWrittenBytes = FileUtility.ReadBlock(i, Book.BOOK_DATA_BLOCK_SIZE, filename);
                        Book bookWrittenObject = Book.ByteArrayBlockToBook(bookWrittenBytes);

                        if (bookWrittenObject != null)
                        {
                            WriteLine("Book number : " + i);
                            WriteLine("ID:" + bookWrittenObject.Id);
                            WriteLine(bookWrittenObject.Title); 
                            WriteLine(bookWrittenObject.Description);
                            WriteLine(bookWrittenObject.Year);
                            WriteLine(bookWrittenObject.Pages);
                            WriteLine(bookWrittenObject.Abstract);
                            WriteLine(bookWrittenObject.City);
                            WriteLine(bookWrittenObject.Edition);
                            WriteLine(bookWrittenObject.Publisher);
                            WriteLine(bookWrittenObject.CatalogId);
                            WriteLine(bookWrittenObject.Price);
                            WriteLine(bookWrittenObject.RackNo);
                            WriteLine(bookWrittenObject.RowNo);
                            WriteLine(bookWrittenObject.Return);
                            WriteLine(bookWrittenObject.Given);
                            WriteLine(bookWrittenObject.Url);
                            WriteLine(bookWrittenObject.Authors[0]);
                            WriteLine(bookWrittenObject.Tags[0]);
                            WriteLine(bookWrittenObject.Editors[0]);
                            WriteLine(bookWrittenObject.Categories[0]);
                            WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");

                        }
                        i++;

                    } while (i < (((datlength.Length) / (Book.BOOK_DATA_BLOCK_SIZE)) + 1));

                    WriteLine("Press any key to return...");
                    ReadKey(true);
                    BookTransactions();
                }
            }
            else { Clear(); WriteLine("Library file couldn't found."); }

            WriteLine("Press any key to return...");
            ReadKey(true);
            BookTransactions();
        }
        private void SearchBook()
        {
            Clear();
            int i = 1;
            if (File.Exists("library.dat"))
            {
                Write("Please enter name or ID of the book which do you want to find: ");
                var search = ReadLine();
                if (ConversionUtility.IsNumeric(search) == false)
                {
                    using (StreamReader sr = new StreamReader(File.Open("library.dat", FileMode.Open)))
                    {
                        Clear();
                        string datlength = sr.ReadLine();
                        sr.Close();
                        do
                        {
                            string path = AppDomain.CurrentDomain.BaseDirectory;
                            string filename = Path.Combine(path, "library.dat");


                            byte[] bookWrittenBytes = FileUtility.ReadBlock(i, Book.BOOK_DATA_BLOCK_SIZE, filename);
                            Book bookWrittenObject = Book.ByteArrayBlockToBook(bookWrittenBytes);

                            if (bookWrittenObject != null && (bookWrittenObject.Title.Contains(search)))
                            {
                                WriteLine(i + ". - " + bookWrittenObject.Id + " | " + bookWrittenObject.Title + " | Description: " + bookWrittenObject.Description + " | Author: " + bookWrittenObject.Authors[0] + " | Category: " + bookWrittenObject.Categories[0] + "\n");
                            }
                            i++;

                        } while (i < (((datlength.Length) / (Book.BOOK_DATA_BLOCK_SIZE)) + 1));
                        WriteLine("Press any key to return...");
                        ReadKey(true);
                        BookTransactions();
                    }
                }
                else
                {
                    int searchint = Convert.ToInt32(search);

                    using (StreamReader sr = new StreamReader(File.Open("library.dat", FileMode.Open)))
                    {
                        Clear();
                        string datlength = sr.ReadLine();
                        sr.Close();
                        do
                        {
                            string path = AppDomain.CurrentDomain.BaseDirectory;
                            string filename = Path.Combine(path, "library.dat");


                            byte[] bookWrittenBytes = FileUtility.ReadBlock(i, Book.BOOK_DATA_BLOCK_SIZE, filename);
                            Book bookWrittenObject = Book.ByteArrayBlockToBook(bookWrittenBytes);

                            if (bookWrittenObject != null && (bookWrittenObject.Id.Equals(searchint)))
                            {
                                WriteLine(i + ". - " + bookWrittenObject.Id + " | " + bookWrittenObject.Title + " | Description: " + bookWrittenObject.Description + " | Author: " + bookWrittenObject.Authors[0] + " | Category: " + bookWrittenObject.Categories[0] + "\n");
                            }
                            i++;

                        } while (i < (((datlength.Length) / (Book.BOOK_DATA_BLOCK_SIZE)) + 1));
                        WriteLine("Press any key to return...");
                        ReadKey(true);
                        BookTransactions();
                    }
                }
            }
            else { Clear(); WriteLine("Library file couldn't found."); }

            WriteLine("Press any key to return...");
            ReadKey(true);
            BookTransactions();
        }
           

            private void CategoryTransactions()
        {
            string prompt = "*ADD CATEGORY*";
            string[] options = { "Add Category", "Delete Category", "Edit Category", "List Category" };
            Menu CategoryTransactions = new Menu(prompt, options);
            int SelectedIndex = CategoryTransactions.Run();

            switch (SelectedIndex)
            {
                case 0:
                    AddCategory();
                    break;
                case 1:
                    DeleteCategory();
                    break;
                case 2:
                    EditCategory();
                    break;
                case 3:
                    ListCategory();
                    break;
            }
        }
        private void AddCategory()
        {
            Clear();
            WriteLine("This section is not working yet. Press any key to return main menu.");
            ReadKey();
            RunMainMenu();
        }
        private void DeleteCategory()
        {
            Clear();
            WriteLine("This section is not working yet. Press any key to return main menu.");
            ReadKey();
            RunMainMenu();
        }
        private void EditCategory()
        {
            Clear();
            WriteLine("This section is not working yet. Press any key to return main menu.");
            ReadKey();
            RunMainMenu();
        }
        private void ListCategory()
        {
            Clear();
            WriteLine("This section is not working yet. Press any key to return main menu.");
            ReadKey();
            RunMainMenu();
        }


        private void ReaderTransactions()
        {
            string prompt = "*ADD READER*";
            string[] options = { "Add Reader", "Delete Reader", "Edit Reader", "List Reader" };
            Menu ReaderTransactions = new Menu(prompt, options);
            int SelectedIndex = ReaderTransactions.Run();

            switch (SelectedIndex)
            {
                case 0:
                    AddReader();
                    break;
                case 1:
                    DeleteReader();
                    break;
                case 2:
                    EditReader();
                    break;
                case 3:
                    ListReader();
                    break;
            }
        }
        private void AddReader()
        {
            Clear();
            WriteLine("This section is not working yet. Press any key to return main menu.");
            ReadKey();
            RunMainMenu();
        }
        private void DeleteReader()
        {
            Clear();
            WriteLine("This section is not working yet. Press any key to return main menu.");
            ReadKey();
            RunMainMenu();
        }
        private void EditReader()
        {
            Clear();
            WriteLine("This section is not working yet. Press any key to return main menu.");
            ReadKey();
            RunMainMenu();
        }
        private void ListReader()
        {
            Clear();
            WriteLine("This section is not working yet. Press any key to return main menu.");
            ReadKey();
            RunMainMenu();
        }


        private void BorrowBookTransactions()
        {
            {
                if (File.Exists("library.dat"))
                {
                    string path = AppDomain.CurrentDomain.BaseDirectory;
                    string filename = Path.Combine(path, "library.dat");
                    Console.Clear();
                    int booknumber;
                    string student;
                    string date;
                    Console.Write("Please enter number of book which do you want to borrow: ");
                    booknumber = Convert.ToInt32(Console.ReadLine());
                    Console.Write("\nWhat is the name of student who got the book: ");
                    student = Console.ReadLine();
                    Console.Write("\nDate: ");
                    date = Console.ReadLine();


                    using (StreamReader sr = new StreamReader(File.Open("library.dat", FileMode.Open)))
                    {
                        string datalength = sr.ReadLine();
                        sr.Close();

                        byte[] bookWrittenBytesforBorrow = FileUtility.ReadBlock(booknumber, Book.BOOK_DATA_BLOCK_SIZE, filename);
                        Book bookWrittenObject = Book.ByteArrayBlockToBook(bookWrittenBytesforBorrow);


                        if (bookWrittenObject != null)
                        {
                            Book book = new Book();
                            book = bookWrittenObject;
                            book.Status = "Borrowed by student: " + student;
                            book.Given = "Given date: " + date;
                            byte[] bookBytes = Book.BookToByteArrayBlock(book);

                            FileUtility.UpdateBlock(bookBytes, booknumber, Book.BOOK_DATA_BLOCK_SIZE, filename);
                        }

                        Console.WriteLine("Press any key to return...");
                        Console.ReadKey(true);
                        RunMainMenu();
                    }
                }
                else { Console.Clear(); Console.WriteLine("Library file couldn't found."); }
                Console.WriteLine("Press any key to return...");
                Console.ReadKey(true);
                RunMainMenu();
            }
        }
        
        private void BookReturnTransactions()
        {
            if (File.Exists("library.dat"))
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                string filename = Path.Combine(path, "library.dat");
                Console.Clear();
                int booknumber;
                string bookname;
                Console.Write("Please enter number of book which do you want to return: ");
                booknumber = Convert.ToInt32(Console.ReadLine());
                Console.Write("\nWhat is book name: ");
                bookname = Console.ReadLine();


                using (StreamReader sr = new StreamReader(File.Open("library.dat", FileMode.Open)))
                {
                    string datalength = sr.ReadLine();
                    sr.Close();

                    byte[] bookWrittenBytesforBorrow = FileUtility.ReadBlock(booknumber, Book.BOOK_DATA_BLOCK_SIZE, filename);
                    Book bookWrittenObject = Book.ByteArrayBlockToBook(bookWrittenBytesforBorrow);

                    if (bookWrittenObject != null)
                    {



                        Book book = new Book();
                        book = bookWrittenObject;
                        book.Given = "Status : Present in library";
                        byte[] bookBytes = Book.BookToByteArrayBlock(book);

                        FileUtility.UpdateBlock(bookBytes, booknumber, Book.BOOK_DATA_BLOCK_SIZE, filename);
                    }
                    else
                    {
                        Console.WriteLine("The book is not found !!! ");
                    }
                }

                Console.WriteLine("Press any key to return...");
                Console.ReadKey(true);
                RunMainMenu();
            }
            else { Console.Clear(); Console.WriteLine("Library file couldn't found."); }
            Console.WriteLine("Press any key to return...");
            Console.ReadKey(true);
            RunMainMenu();
        }
       
        
        private void AboutInfo()
        {
            Clear();
            WriteLine(" *<>*<>*<><>*<>*<><>*<>*<><>*<>*<><>*<>*<>*" +
                "\n\n I am Duygu AYVAZ. I am a 1st year Computer Engineering student." +
                "\n My Contact Information:" +
                "\n e-mail address: duygu.ayvaz.tr@gmail.com" +
                "\n GitHub link: https://github.com/duyguayvaz" +
                "\n LinkedIn link: https://www.linkedin.com/in/duygu-ayvaz-395a46251/" +
                "\n HackerRank link: https://www.hackerrank.com/duygu_ayvaz_tr" +
                "\n\n *<>*<>*<><>*<>*<><>*<>*<><>*<>*<><>*<>*<>*" +
                "\n\n This is a library management application. " +
                "\n With this application, you can add, edit, delete,borrow and list books." +
                "\n\n *<>*<>*<><>*<>*<><>*<>*<><>*<>*<><>*<>*<>*");
            ReadKey(true);
            RunMainMenu();
        }
        private void MainMenu()
        {
            RunMainMenu();
        }

        
        private void ExitInfo()
        {
            WriteLine("||| Press any key to exit |||");
            ReadKey(true);
            Environment.Exit(0);
        }
    }
}
