using Common.Models;
using DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    //What is the role of a Repository?
    //The repository class will define CRUD (Create Read Update Delete)
    //operations which will be LINQ based

    //The idea about the management of instance creation
    //is to create an instance once
    //in the beginning and inject it in the client classes (such as
    //BooksRepository)
    //Without knowing we are utilizing a design pattern called
    //DEPENDENCY INJECTION

    //In practice: an instance is created once in the beginning
    //             in Program.cs
    //             So in Program.cs there will be a service collection
    //             containing all the instances created in the application

    //Dependency Injection will create and pass instances into the 
    //client classes using
    //way 1: CONSTRUCTOR INJECTION
    //way 2: METHOD INJECTION
    //way 3: property injection
    
    public class BooksRepository
    {
        private LibraryDbContext _context;
        public BooksRepository(LibraryDbContext context) 
        {
            _context = context;
        }

        //Select
        //to do: IQueryable vs List
        
        public IQueryable<Book> GetBooks()
        {
            return _context.Books;
        }
        //Create
        public void AddBook(Book b)
        {
            _context.Books.Add(b);
        }
        //Delete
        //Update
    }
}
