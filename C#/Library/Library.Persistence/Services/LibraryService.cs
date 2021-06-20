using Library.Persistence;
using Library.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Persistence.Services
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
                .Where(l => l.Name
                .Contains(name ?? ""));  
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
            return _context.Volumes.AsNoTracking()
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
            return _context.Reservations.AsNoTracking()
                   .Where(l => l.VolumeId == id)
                   .OrderBy(v =>v.Start)
                   .ToList();
        }
        public Reservation CreateReservation(Reservation reservation)
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
                return null;
            }
            catch (DbUpdateException)   
            {
                return null;
            }
            catch(ArgumentNullException)
            {
                return null;
            }
            catch(Exception)
            {
                return null;
            }

            return reservation;
        }

        public bool UpdateBook(Book book)
        {
            try
            {
                _context.Update(book);
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

        public Volume CreateVolume(Volume volume)
        {
            try
            {
                _context.Add(volume);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            catch (DbUpdateException)
            {
                return null;
            }

            return volume;
        }

        public bool UpdateReservation(Reservation reservation)
        {
            try
            {
                //reservation.IsActive = !reservation.IsActive;
                _context.Update(reservation);
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

        public bool DeleteVolume(int id)
        {
            var volume = _context.Volumes.Find(id);
            if (volume == null)
            {
                return false;
            }

            try
            {
                _context.Remove(volume);
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

        public bool DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return false;
            }

            try
            {
                _context.Remove(book);
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
        public Book CreateBook(Book book)
        {
            try
            {
                _context.Add(book);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            catch (DbUpdateException)
            {
                return null;
            }

            return book;
        }

        public bool Deletereservation(int id)
        {
            var reservation = _context.Reservations.Find(id);
            if (reservation == null)
            {
                return false;
            }


            try
            {
                _context.Remove(reservation);
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

        public bool UpdateVolume(Volume volume)
        {
            try
            {
                _context.Update(volume);
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

        public ApplicationUser GetUser(string id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
