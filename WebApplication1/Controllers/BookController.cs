using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    public class BookController : Controller
    {
      private readonly IBookRepository _bookRepository;

        public BookController()
        {
            _bookRepository = new BookRepository(new SampleDbEntities());
        }

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public ActionResult Index()
        {
            var books = _bookRepository.GetBooks();
            return View(books);
        }

        public ActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// Uset to Add the Book 
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _bookRepository.NewBook(book);
                _bookRepository.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var book = _bookRepository.GetBookById(id);
            return View(book);
        }
        /// <summary>
        /// This method is use to delete by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            int idc = 0;
            if (id != null)
            {
                idc = (int)id;
            }

            _bookRepository.DeleteBook(idc);
            _bookRepository.Save();
            return RedirectToAction("Index", "Book");
        }
        /// <summary>
        /// Its use to search the book by Name,Author,Price and Category
        /// </summary>
        /// <param name="seachstring"></param>
        /// <returns></returns>
        public ViewResult Search(string seachstring)
        {

            var books = _bookRepository.GetBooks();
            int id = Convert.ToInt32(Request["SearchType"]);
            var searchParameter = "Searching";

            if (!string.IsNullOrWhiteSpace(seachstring))
            {
                switch (id)
                {
                    case 0:
                        books = books.Where(p => p.Bookname.Contains(seachstring));
                        searchParameter += " Id for ' " + seachstring + " '";
                        break;
                    case 1:
                        books = books.Where(p => p.Authorname.Contains(seachstring));
                        searchParameter += " First Name for ' " + seachstring + " '";
                        break;
                    case 2:
                        int price = 0;
                        bool isInt = int.TryParse(seachstring, out price);
                        books = books.Where(p => p.Bookprice.Equals(Convert.ToInt32(price)));
                        searchParameter += " Last Name for '" + seachstring + "'";
                        break;
                    case 3:
                        books = books.Where(p => p.Category.Contains(seachstring));
                        searchParameter += " Last Name for '" + seachstring + "'";
                        break;
                }
            }
            else
            {
                searchParameter += "ALL";
            }
            ViewBag.SearchParameter = searchParameter;
            return View(books);
        }

        /// <summary>
        /// Deliver book
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Deliver(int id)
        {
            var book = _bookRepository.GetBookById(id);
            return View(book);
        }
        /// <summary>
        /// Deliver book 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deliver(int? id)
        {
             
            return RedirectToAction("Index", "Book");
        }
    }
}