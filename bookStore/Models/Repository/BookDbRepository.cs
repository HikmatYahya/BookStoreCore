using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookStore.Models.Repository
{
    public class BookDbRepository : IBookStoreRepository<Book>
    {
        BookstoreDbContext db;


        public BookDbRepository(BookstoreDbContext _db)
        {
            db = _db;
        }




        public void Add(Book entity)
        {
           // entity.Id = db.Books.Max(b => b.Id) + 1;
            db.Books.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = Find(id); //books.SingleOrDefault(b => b.Id == id);

            db.Books.Remove(book);
            db.SaveChanges();
        }

        public Book Find(int id)
        {
            var Book = db.Books.Include(a => a.Author).SingleOrDefault(b => b.Id == id);


            return Book;
        }

        public IList<Book> List()
        {
            return db.Books.Include(a => a  .Author).ToList();
        }

        public void Update(int id, Book newBook)
        {
            db.Update(newBook);
            db.SaveChanges();
        }


    }
}