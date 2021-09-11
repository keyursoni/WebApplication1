using System;
using System.Collections.Generic;
using System.Linq;
/// <summary>
/// Book Repository class ,It impletments all the method from the repository class
/// </summary>

using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class BookRepository :IBookRepository
    {
        private readonly SampleDbEntities _dbContext;

        public BookRepository()
        {
            _dbContext = new SampleDbEntities();
        }
        public BookRepository(SampleDbEntities context)
        {
            _dbContext = context;
        }

        public IEnumerable<Book> GetBooks()
        {
            return _dbContext.Books.ToList();
        }

        public Book GetBookById(int id)
        {
            return _dbContext.Books.Find(id);
        }

        public void NewBook(Book book)
        {
            _dbContext.Books.Add(book);
            Save();
        }


        public void DeleteBook(int id)
        {
            var book = _dbContext.Books.Find(id);
            if (book != null) _dbContext.Books.Remove(book);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}