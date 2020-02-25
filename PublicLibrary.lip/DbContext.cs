using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicLibrary.lip
{
    public class DbContext
    {
        public DbContext(string Path)
        {
            this.Path = Path;
        }

        private string Path { get; set; }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            using (var db = new LiteDatabase(Path))
            {
                users = db.GetCollection<User>("User").FindAll().ToList();
            }

            return users;
        }

        public void GetUser(User user)
        {
            using (var db = new LiteDatabase(Path))
            {
                user = db.GetCollection<User>("User")
                         .FindOne(f => f.Login == user.Login 
                         && f.Password == user.Password);
            }
        }

        public bool RegUser(User user)
        {
            try
            {
                using (var db = new LiteDatabase(Path))
                {
                    var users = db.GetCollection<User>("User");
                    users.Insert(user);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool AddBook(Book book)
        {
            try
            {
                using (var db = new LiteDatabase(Path))
                {
                    var books = db.GetCollection<Book>("Book");
                    books.Insert(book);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Book> GetBooks()
        {
            List<Book> books = new List<Book>();

            using (var db = new LiteDatabase(Path))
            {
                books = db.GetCollection<Book>("Book").FindAll().ToList();
                return books;
            }

        }
        public Book GetBookbyId(int id)
        {
            Book book = new Book();

            using (var db = new LiteDatabase(Path))
            {
                book = db.GetCollection<Book>("Book").FindById(id);
            }
            return book;
        }

        public bool EditBook(Book book)
        {
            using (var db = new LiteDatabase(Path))
            {
               var books = db.GetCollection<Book>("Book");
                books.Update(book);
            }
            return true;
        }


        public bool AddPublisher(Publisher pub)
        {
            using (var db = new LiteDatabase(Path))
            {
                var pubs = db.GetCollection<Publisher>("Publisher");
                pubs.Insert(pub);
            }
            return true;
        }

        public bool EditPublisher(Publisher pub)
        {
            using (var db = new LiteDatabase(Path))
            {
                var pubs = db.GetCollection<Publisher>("Publisher");
                pubs.Update(pub);
            }
            return true;
        }

        public List<Publisher> GetAllPubs()
        {
            using (var db = new LiteDatabase(Path))
            {
                return db.GetCollection<Publisher>("Publisher").FindAll().ToList();
            }
        }

    }
}
