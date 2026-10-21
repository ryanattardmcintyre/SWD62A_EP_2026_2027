using Common.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class BooksController : Controller
    {
        private BooksRepository _booksRepository;
        public BooksController(BooksRepository booksRepository) 
        { 
                _booksRepository = booksRepository;
        }

        //step 1: user types in the url to add a book, clicks ENTER and a request is forwarded to our Controller
        //     GET:     /Books/Create
        //step 2: page is returned back to the user with empty text fields
        //step 3: user fills in the text fields and submits
        //     POST:      /Books/Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book b)
        {
            try
            {
                _booksRepository.AddBook(b);
                //ViewBag or TempData
                TempData["message"] = "Book added successfully";
            }
            catch (Exception ex)
            {
                //log ex into a file or cloud
                TempData["error"] = "Book was not added. Check your inputs";
            }
            return View(); //redirecting the user back to the page where he came from

        }

       

    }
}
