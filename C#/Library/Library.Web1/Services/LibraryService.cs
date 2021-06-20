using Library.Web.Models;
using Library.Web.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Web.Services
{
    public class LibraryService:ILibraryService
    {
        public enum SortOrder { FAME_DESC, FAME_ASC, NAME_DESC, NAME_ASC }
        private readonly LibraryDbContext _context;
        public LibraryService(LibraryDbContext context)
        {
            _context = context;
        }
        public List<Book> GetBooks(string name = null, ILibraryService.SortOrder sortOrder = ILibraryService.SortOrder.FAME_DESC)
        {
            var books = _context.Books
                .Where(l => l.Name.Contains(name ?? ""));  
            switch (sortOrder)
            {
                case ILibraryService.SortOrder.FAME_DESC:books = books.OrderByDescending(b =>b.Rents).ThenBy(b=> b.Name);
                    break;
                case ILibraryService.SortOrder.FAME_ASC:books = books.OrderBy(b => b.Rents).ThenBy(b => b.Name); ;
                    break;
                case ILibraryService.SortOrder.NAME_DESC:books = books.OrderByDescending(b => b.Name).ThenByDescending(b => b.Rents);
                    break;
                case ILibraryService.SortOrder.NAME_ASC:books = books.OrderBy(b => b.Name).ThenByDescending(b => b.Rents); ;
                    break;
                default:
                    break;
            }
            return books.ToList(); ;
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
        public Volume GetVolumeDetails(int id)
        {
            return _context.Volumes
                .Include(r => r.Reservations)
                .Single(v => v.Id == id);
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
                   .OrderBy(v =>v.Start)
                   .ToList();
        }
        public bool CreateReservation(Reservation reservation)
        {
            try
            {
                _context.Add(reservation);
                var volume = GetVolume(reservation.VolumeId);
                var book = GetBookByID(volume.BookId);
                book.Rents++;
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
            catch(ArgumentNullException)
            {
                return false;
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }

        
    }
}
