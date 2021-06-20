using Library.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Service
{
    public class LibraryService
    {
        private readonly LibraryDbContext _context;
        public LibraryService(LibraryDbContext context)
        {
            _context = context;
        }
        public List<Book> GetBooks(String name = null)
        {
                return _context.Books
                .Where(l => l.Name.Contains(name ?? "")) 
                .OrderBy(l => l.Name)
                .ToList();
        }
        public Book GetBookByID(int id)
        {
            return _context.Books
                .Single(l => l.Id == id); 
        }
        public List<Volume> GetVolumesByBookId(int id)
        {
            return _context.Volumes
                   .Where(l => l.BookId == id)
                   .ToList();
        }
        public Book GetBookDetails(int id)
        {
            return _context.Books
                .Include(l => l.Volumes)
                .Single(l => l.Id == id);
        }
        public Volume GetVolume(int id)
        {
            return _context.Volumes
                .FirstOrDefault(i => i.Id == id);
        }
        public Reservation GetReservation(int id)
        {
            return _context.Reservations
                .FirstOrDefault(i => i.Id == id);
        }
        public List<Reservation> GetReservationsByVolumeId(int id)
        {
            return _context.Reservations
                   .Where(l => l.VolumeId == id)
                   .ToList();
        }
        public bool CreateReservation(Reservation reservation)
        {
            try
            {
                _context.Add(reservation);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }
            return true;
        }



    }
}
