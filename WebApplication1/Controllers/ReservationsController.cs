using Common.Interfaces;
using Common.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class ReservationsController : Controller
    {
        ReservationsRepository _reservationsRepository;
        IBooksRepository _booksRepository;
        IBookingPolicy _winterPolicy;
        IBookingPolicy _summerPolicy;

        public ReservationsController(
            ReservationsRepository rr, 
            IBooksRepository br,
            [FromKeyedServices("winter")] IBookingPolicy wp,
            [FromKeyedServices("summer")] IBookingPolicy sp
            ) 
        {
            _reservationsRepository = rr;
            _booksRepository = br;
            _winterPolicy = wp;
            _summerPolicy = sp;
        }

        //todo: get reservations for logged in user
        [Authorize]
        public IActionResult Index()
        {
            var list = _reservationsRepository.GetReservations().Where(x => x.Username ==
                User.Identity.Name
            );
            return View(list);
        }

        [HttpGet]
        public IActionResult SelfBorrow() { return View(); }

        [HttpPost]
        public IActionResult SelfBorrow(Reservation r) 
        {
            if( _booksRepository.GetBook(r.BookFK) ==null)
            {
                TempData["error"] = "Book doesn't exist";
                return RedirectToAction("Index");
            }

            if(_reservationsRepository.GetReservations().Where(x=>x.DateReturned == null 
            && x.BookFK == r.BookFK).Count() ==1)
            {
                TempData["error"] = "Book cannot be borrowed at the moment";
                return RedirectToAction("Index");
            }

            r.DateTaken = DateTime.Now;
            r.Username = User.Identity.Name;
            r.DateReturned = null;

            if (r.DateTaken.Month >= 6 && r.DateTaken.Month <= 9)
                r.DateDue = _summerPolicy.CalculateDueDate(r.DateTaken);
            else r.DateDue = _winterPolicy.CalculateDueDate(r.DateTaken);

                _reservationsRepository.ReserveABook(r);

            TempData["message"] = "Reservation saved";
            return RedirectToAction("Index");
        }
    }
}
