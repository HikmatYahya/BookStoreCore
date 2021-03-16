using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookStore.Models.Repository
{
    public class AuthorDbRepository : IBookStoreRepository<Author>
    {
        BookstoreDbContext db;


        public AuthorDbRepository(BookstoreDbContext _db)
        {
            db = _db;
        }
        public void Add(Author entity)
        {
            //entity.Id = db.Authors.Max(b => b.Id) + 1;
            db.Authors.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var author = Find(id);

            db.Authors.Remove(author);
            db.SaveChanges();
        }

        public Author Find(int id)
        {
            var auther = db.Authors.SingleOrDefault(a => a.Id == id);
            return auther;
        }

        public IList<Author> List()
        {
            return db.Authors.ToList();
        }



        public void Update(int id, Author newAuthor)
        {
            db.Update(newAuthor);
            db.SaveChanges();
        }
    }
}       
