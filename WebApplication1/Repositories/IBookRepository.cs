/// <summary>
/// Repository Interface It will defind the Book method
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
   
    public interface IBookRepository
    {
        IEnumerable<Book> GetBooks();
        Book GetBookById(int id);
        void NewBook(Book book);
        void DeleteBook(int id);
        void Save();
    }
}
