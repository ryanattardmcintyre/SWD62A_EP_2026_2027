using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IBooksRepository
    {
        void AddBook(Book book);
        IQueryable<Book> GetBooks();
        Book? GetBook(string isbn);

        IQueryable<Book> GetBooks(string keyword);
        IQueryable<Book> GetBooks(string keyword, int pageSize, int startAt);

        void UpdateBook(Book book);
        void DeleteBook(string isbn);
    }
}
