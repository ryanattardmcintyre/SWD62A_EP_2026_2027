using Common.Interfaces;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class BooksJsonRepository : IBooksRepository
    {
        public BooksJsonRepository(string _pathToJson) {

            pathToJson = _pathToJson;
            if(System.IO.File.Exists(pathToJson) == false)
            {
                using (var fs = System.IO.File.Create(pathToJson))
                    fs.Close();
            }
        
        }


        private string pathToJson = "Books.json";
        public void AddBook(Book book)
        {
            //1. get the entire list
            List<Book> myBooks = GetBooks().ToList();
            //2. add new book to the existent list
            if (GetBook(book.Isbn) == null)
                myBooks.Add(book);
            else throw new Exception("Book already exists");
            //3. save it back
            //   Convert from object => (json) string
            //   aka json Serialization
            string jsonBooks = JsonSerializer.Serialize(myBooks);

            System.IO.File.WriteAllText(pathToJson, jsonBooks);
        }

        public void DeleteBook(string isbn)
        {
            throw new NotImplementedException();
        }

        public Book? GetBook(string isbn)
        {
            return GetBooks().SingleOrDefault(x => x.Isbn == isbn);
        }

        public IQueryable<Book> GetBooks()
        {
            string fileContents = System.IO.File.ReadAllText(pathToJson);
            //convert from json (string) => object (List<Book>)
            //aka json
            if (string.IsNullOrEmpty(fileContents) == false)
            {
                try
                {
                    List<Book> myBooks = JsonSerializer.Deserialize<List<Book>>(fileContents);
                    return myBooks.AsQueryable();

                }catch (Exception ex)
                {
                    //logging
                    return new List<Book>().AsQueryable();
                }
            }
            else
            {
                return new List<Book>().AsQueryable();
            }
        }

        public IQueryable<Book> GetBooks(string keyword)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Book> GetBooks(string keyword, int pageSize, int startAt)
        {
            throw new NotImplementedException();
        }

        public void UpdateBook(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
