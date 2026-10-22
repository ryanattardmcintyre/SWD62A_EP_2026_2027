using Common.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class BooksController : Controller
    {
        private BooksRepository _booksRepository;
        private CategoriesRepository _categoriesRepository;
        public BooksController(BooksRepository booksRepository,
                CategoriesRepository categoriesRepository)
        {
            _booksRepository = booksRepository;
            _categoriesRepository = categoriesRepository;
        }

        //step 1: user types in the url to add a book, clicks ENTER and a request is forwarded to our Controller
        //     GET:     /Books/Create
        //step 2: page is returned back to the user with empty text fields
        //step 3: user fills in the text fields and submits
        //     POST:      /Books/Create

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _categoriesRepository.GetCategories();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Book b)
        {
            try
            {
                var count = _booksRepository.GetBooks().Where(x => x.Isbn == b.Isbn).Count();
                if (count == 0)
                { 
                     _booksRepository.AddBook(b);
                    //ViewBag or TempData
                    TempData["message"] = "Book added successfully";
                }
                else
                {
                    TempData["error"] = "Book already exists";
                }
                    
            }
            catch (Exception ex)
            {
                //log ex into a file or cloud
                TempData["error"] = "Book was not added. Check your inputs";
            }

            ViewBag.Categories = _categoriesRepository.GetCategories();
            return View(); //redirecting the user back to the page where he came from

        }

       

    }
}
