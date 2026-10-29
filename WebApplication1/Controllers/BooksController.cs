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
        public IActionResult Create(Book b, IFormFile file, [FromServices] IWebHostEnvironment host)
        {
            try
            {
                var count = _booksRepository.GetBooks().Where(x => x.Isbn == b.Isbn).Count();
                if (count == 0)
                { 
                    if(file != null)
                    {
                        //absolutePath is needed to save the physical file
                        //to get the absolutePath I need a FRAMEWORK SERVICE called IWEBHOSTENVIRONMENT

                        string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                        string absolutePath = host.WebRootPath + "\\images\\" + filename;


                        using (var fsOut = new FileStream(absolutePath, FileMode.CreateNew))
                        {
                            using (var f = file.OpenReadStream())
                            {
                                f.CopyTo(fsOut);
                            }
                        }

                            //relativePath is needed to open & render the image and hence we're saving this
                            //in the database in the Cover property of Book
                            string relativePath = "\\images\\"+filename;
                         b.Cover = relativePath;
                    }

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
