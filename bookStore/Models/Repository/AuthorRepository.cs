using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookStore.Models.Repository
{
    public class AuthorRepository : IBookStoreRepository<Author>
    {
        IList<Author> authors;


        public AuthorRepository()
        {
            authors = new List<Author>()
            {
                new Author {Id = 1 , FullName = "Ahmad"},
                new Author {Id = 2 , FullName = "Mohd"},
                new Author {Id = 3 , FullName = "Khalid"},

            };

        }
        public void Add(Author entity)
        {
           // entity.Id = authors.Max(b => b.Id) + 1;
            authors.Add(entity);
        }

        public void Delete(int id)
        {
            var author = Find(id);
            authors.Remove(author);
        }

        public Author Find(int id)
        {
            var auther = authors.SingleOrDefault(a=> a.Id == id);
            return auther;
        }

        public IList<Author> List()
        {
            return authors;
        }

        

        public void Update(int id, Author newAuthor)
        {
            var author = Find(id);

            author.Id = newAuthor.Id;
            author.FullName = newAuthor.FullName;
        }
    }
}
