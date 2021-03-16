using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookStore.Models.Repository
{
    public class BookRepository : IBookStoreRepository<Book>
    {

        List<Book> books = new List<Book>()
        {
            new Book
            {
                Id = 1, Title = "C#", Descrption = " No Descrpation",  Author = new Author()
            },
            new Book
            {
                Id = 2, Title = "Java", Descrption = " No Info" ,Author = new Author()
            },
            new Book
            {
                Id = 3, Title = "C++", Descrption = " No Data" ,Author = new Author()
            },


        };
        public void Add(Book entity)
        {
            entity.Id = books.Max(b=>b.Id)+1;
            books.Add(entity);
        }

        public void Delete(int id)
        {
            var book = Find(id); //books.SingleOrDefault(b => b.Id == id);

            books.Remove(book);
        }

        public Book Find(int id)
        {
            var Book = books.SingleOrDefault(b => b.Id == id);


            return Book;
        }

        public IList<Book> List()
        {
            return books;
        }

        public void Update(int id, Book newBook)
        {
            var Book = Find(id); //books.SingleOrDefault(b => b.Id == id);

            Book.Title = newBook.Title;
            Book.Descrption = newBook.Descrption;
            Book.Author = newBook.Author;
        }

       
    }
}
