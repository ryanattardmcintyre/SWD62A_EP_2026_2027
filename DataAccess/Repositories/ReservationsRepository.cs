using Common.Models;
using DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ReservationsRepository
    {
        LibraryDbContext _context;
        public ReservationsRepository(LibraryDbContext context) 
        {
            _context = context;
        }

        public void ReserveABook(Reservation r)
        {
            _context.Reservations.Add(r);
            _context.SaveChanges();
        }

        public IQueryable<Reservation> GetReservations() {

            return _context.Reservations;
        }

        public Reservation? GetReservation(int id)
        { 
            return GetReservations().SingleOrDefault(r => r.Id == id);
        }
    }
}
