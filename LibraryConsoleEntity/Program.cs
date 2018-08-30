using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryConsoleEntity
{
    class Program
    {

        static void AddAuthor(Author author)
        {
            using (Library2Entities db=new Library2Entities())
            {
                db.Author.Add(author);
                db.SaveChanges();
                Console.WriteLine("New Author Added:"+author.LastName);
            }
        }

        static void AddBook(Book book)
        {
            using (Library2Entities db = new Library2Entities())
            {
                    db.Book.Add(book);
                    db.SaveChanges();
                    Console.WriteLine("New Book Added:" + book.Title);     
            }
        }

        static void AddUsers(Users user)
        {
            using (Library2Entities db = new Library2Entities())
            {
                db.Users.Add(user);
                Console.WriteLine("New User Added:" + user.UserLastName);
                db.SaveChanges();
            }
        }

        static void AllDebtors()    //Должники
        {
            using(Library2Entities db=new Library2Entities())
            {
                var users = db.Users.Where(x => x.IsDebtor == true).ToList();
                Console.WriteLine("Список Должников");
                foreach (var item in users)
                {
                    Console.WriteLine(item.UserName+" "+item.UserLastName);
                }
            }
        }

        static void listAuthorsOfBook()
        {
            using (Library2Entities db = new Library2Entities())
            {
                var book = db.Book.Take(3).ToList().Skip(2).Single();  //книга №3

                var authors = (from x in db.Author where x.Id == book.IdAuthor || x.Id == book.IdAuthor2 select x).ToList();

                Console.WriteLine("Список авторов книги №3");
                
                    
                foreach(var item in authors)
                {
                    Console.WriteLine(item.LastName);
                }
            }
        }

        static void AllFreeBooks()
        {
            using (Library2Entities db=new Library2Entities())
            {
                var UsersBookId = (from x in db.Users select x.TakeBookId).ToList();
                var result = (from b in db.Book where UsersBookId.Contains(b.Id)!=true select b.Title).ToList();

                Console.WriteLine("Free Books");
                foreach (var item in result)
                {
                    Console.WriteLine("{0}", item);
                }
            }
        }

        static void BooksTakeUser2()
        {
            using (Library2Entities db=new Library2Entities())
            {
                var user2 = db.Users.Take(2).ToList().Skip(1).Single();
                var bookName = db.Book.Where(b => b.Id == user2.TakeBookId).ToList();
                Console.WriteLine("Книга на руках у пользователя №2.");
                foreach (var item in bookName)
                {
                    Console.WriteLine(item.Title);
                }
               
            }
        }

        static void AllDebtorsNull()  //Обнуление долгов
        {
            using (Library2Entities db=new Library2Entities())
            {
                var debtors = db.Users.Where(u => u.IsDebtor==true).ToList();

                Console.WriteLine("Должники");
                foreach (var item in debtors)
                {
                    item.IsDebtor = false;
                    db.SaveChanges();
                    Console.WriteLine("{0} {1}", item.UserLastName, item.IsDebtor);
                }
            }
        }

        static void Main(string[] args)
        {
            Author author1 = new Author { FirstName = "Alex", LastName = "Petrov" };
            Author author2 = new Author { FirstName = "Lev", LastName = "Tolstoi" };
            Author author3 = new Author { FirstName = "Alexander", LastName = "Pushkin" };
            Author author4 = new Author { FirstName = "Taras", LastName = "Shevchenko" };
            Author author5 = new Author { FirstName = "Mixail", LastName = "Lermontov" };

            //AddAuthor(author1);
            //AddAuthor(author2);
            //AddAuthor(author3);
            //AddAuthor(author4);
            //AddAuthor(author5);

            Book book1 = new Book { Title = "Kolobok", IdAuthor = 1, Pages = 205, Price = 320, IdPublisher=4 };
            Book book2 = new Book { Title = "Voina i Mir", IdAuthor = 2, Pages = 800, Price = 430, IdPublisher = 4 };
            Book book3 = new Book { Title = "Onegin", IdAuthor = 3, Pages = 380, Price = 220, IdPublisher = 4 };
            Book book4 = new Book { Title = "Kobzar", IdAuthor = 4, Pages = 470, Price = 180, IdPublisher = 4 };
            Book book5 = new Book { Title = "Demon", IdAuthor = 5, Pages = 155, Price = 98, IdPublisher = 4 };
            Book book6 = new Book { Title = "Borodino", IdAuthor = 5, Pages = 147, Price = 178, IdPublisher = 4 };
            Book book7 = new Book { Title = "FixBook", IdAuthor = 5, Pages = 147, Price = 178, IdAuthor2 = 2, IdPublisher = 4 };
            Book book8 = new Book { Title = "Onegin2", IdAuthor = 3, Pages = 582, Price = 520, IdPublisher = 4 };
            Book book9 = new Book { Title = "Kolobok2", IdAuthor = 1, Pages = 100, Price = 80, IdPublisher = 4 };

            //AddBook(book1);
            //AddBook(book2);
            //AddBook(book3);
            //AddBook(book4);
            //AddBook(book5);
            //AddBook(book6);
            //AddBook(book7);
            //AddBook(book8);
            //AddBook(book9);

            Users user1 = new Users { UserName = "Ivan", UserLastName = "Sidorov", TakeBookId = 9, IsDebtor = false };
            Users user2 = new Users { UserName = "Serg", UserLastName = "Maxovich", TakeBookId = 16, IsDebtor = true };
            Users user3 = new Users { UserName = "Bob", UserLastName = "Bobov", TakeBookId = 11, IsDebtor = false };
            Users user4 = new Users { UserName = "Max", UserLastName = "Maxov", TakeBookId = 12, IsDebtor = false };
            Users user5 = new Users { UserName = "Jon", UserLastName = "Jason", TakeBookId = 13, IsDebtor = true };
            Users user6 = new Users { UserName = "Dmitr", UserLastName = "Dmitriev", TakeBookId = 14, IsDebtor = false };
            Users user7 = new Users { UserName = "Evgen", UserLastName = "Evgenov", TakeBookId = 15, IsDebtor = true };

            //AddUsers(user1);
            //AddUsers(user2);
            //AddUsers(user3);
            //AddUsers(user4);
            //AddUsers(user5);
            //AddUsers(user6);
            //AddUsers(user7);

            AllDebtors();   //Должники

            listAuthorsOfBook();  //Список авторов книги №3

            AllFreeBooks();  //свободные книги

            BooksTakeUser2(); // книги у пользователя №2

            AllDebtorsNull(); //Обнуление задолженности
        }
    }
}
