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

        public IActionResult Index()
        {
            var list =_booksRepository.GetBooks();
            list = list.OrderBy(b => b.Title);

            return View(list); //passing an object/list of objects to a page
                               //passing a Model of type List<Book> to the page

        }

        
        public IActionResult Search(string keyword)
        {
            var list = _booksRepository.GetBooks(keyword);
            list = list.OrderBy(b => b.Title);


            return View("Index", list);
        }

        public IActionResult Details(string id)
        {
            var book = _booksRepository.GetBook(id);
            if (book == null)
            {
                TempData["error"] = "No books found";
                return RedirectToAction("Index"); //we are redirecting the user to the Index method!!
              
            }
            else
            {
                return View(book);
            }

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
                    ModelState.Clear();
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
