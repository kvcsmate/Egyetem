//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Web.Models
{
    public class LibraryDbContext: IdentityDbContext<ApplicationUser>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Volume> Volumes { get; set; }

        public DbSet<Reservation> Reservations { get; set; }
        public LibraryDbContext(DbContextOptions options):base(options)
        {

        }
    }
}
