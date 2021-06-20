using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Model
{
    public class LibraryDbContext: IdentityDbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Volume> Volumes { get; set; }

        public DbSet<Reservation> Reservations { get; set; }
        public LibraryDbContext()
        {

        }
    }
}
