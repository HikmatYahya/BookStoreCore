using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using bookStore.Models;
using bookStore.Models.Repository;
using bookStore.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace bookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookStoreRepository<Book> bookRepository;
        private readonly IBookStoreRepository<Author> authorRepository;
        private readonly IHostEnvironment hosting;

        public BookController(IBookStoreRepository<Book> bookRepository, IBookStoreRepository<Author> authorRepository,
            IHostEnvironment hosting)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.hosting = hosting;
        }
        // GET: Book
        public ActionResult Index()
        {
            var books = bookRepository.List();
            return View(books);
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            var books = bookRepository.Find(id);
            return View(books);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            var model = new BookAuthorViewModel
            {
                Authors = FillselectList()
            };

            return View(model);
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = string.Empty;

                    if (model.File !=null)
                    {
                        string uploads = Path.Combine(hosting.ContentRootPath,"uploads");
                        fileName = model.File.FileName;
                        string fullpath = Path.Combine(uploads,fileName);
                        model.File.CopyTo(new FileStream(fullpath,FileMode.Create));

                    }

                    if (model.AuthorId == -1)
                    {
                        ViewBag.Message = "Please select an auhtor form the list";

                        return View(GetAllAhuthors());
                    }

                    var author = authorRepository.Find(model.AuthorId);

                    Book book = new Book
                    {
                        Id = model.BookId,
                        Title = model.Title,
                        Descrption = model.Description,
                        Author = author,
                        Imageurl = fileName
                    };

                    bookRepository.Add(book);
                    // TODO: Add insert logic here

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }

            ModelState.AddModelError("", "You have to fill all requierd fields");
            return View(GetAllAhuthors());

        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookRepository.Find(id);
            var authorId = book.Author == null ? book.Author.Id = 0 : book.Author.Id;

            var viewmodel = new BookAuthorViewModel
            {
                BookId = book.Id,
                Description = book.Descrption,
                Title = book.Title,
                AuthorId = authorId,
                Authors = authorRepository.List().ToList(),
            };

            return View(viewmodel);
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookAuthorViewModel viewModel)
        {
            try
            {
                var author = authorRepository.Find(viewModel.AuthorId);

                Book book = new Book
                {
                    Id = viewModel.BookId,
                    Title = viewModel.Title,
                    Descrption = viewModel.Description,
                    Author = author
                };

                bookRepository.Update(viewModel.BookId, book);
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepository.Find(id);
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                bookRepository.Delete(id);


                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        List<Author> FillselectList()
        {
            var authors = authorRepository.List().ToList();
            authors.Insert(0, new Author { Id = -1, FullName = "--- Please Select an author ---" });
            return authors;
        }


        BookAuthorViewModel GetAllAhuthors()
        {
            var vmodel = new BookAuthorViewModel
            {
                Authors = FillselectList()
            };

            return vmodel;
        }
    }
}