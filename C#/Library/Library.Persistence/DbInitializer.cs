using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Persistence
{   
    public class DbInitializer
    {
        private static  RoleManager<IdentityRole> _roleManager;
        private static  UserManager<ApplicationUser> _userManager;
        public static async Task Initialize(LibraryDbContext context, string imageDirectory, IServiceProvider _serviceProvider) 
        {
            _roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            _userManager = _serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            //context.Database.Migrate(); //progam automatikusan migrál
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            //return;
            var users = new ApplicationUser[]
                   {
                        new ApplicationUser { UserName="lib1"},
                        new ApplicationUser { UserName="lib2"},
                        new ApplicationUser { UserName="lib3"},

                        new ApplicationUser { UserName="usr1"},
                        new ApplicationUser { UserName="usr2"},
                        new ApplicationUser { UserName="usr3"}
                   };
            
            if (!context.Users.Any())
            {

                await _roleManager.CreateAsync(new IdentityRole("Librarian"));
                await _roleManager.CreateAsync(new IdentityRole("User"));

                await _userManager.CreateAsync(users[0], "lib1");
                await _userManager.CreateAsync(users[1], "lib2");
                await _userManager.CreateAsync(users[2], "lib3");
                await _userManager.CreateAsync(users[3], "usr1");
                await _userManager.CreateAsync(users[4], "usr2");
                await _userManager.CreateAsync(users[5], "usr3");

                await _userManager.AddToRoleAsync(users[0], "Librarian");
                await _userManager.AddToRoleAsync(users[1], "Librarian");
                await _userManager.AddToRoleAsync(users[2], "Librarian");
                await _userManager.AddToRoleAsync(users[3], "User");
                await _userManager.AddToRoleAsync(users[4], "User");
                await _userManager.AddToRoleAsync(users[5], "User");
                
            }
            
            var lotrpath = Path.Combine(imageDirectory, "lotr.png");
            var beowulfpath = Path.Combine(imageDirectory, "beowulf.png");
            var nemopath = Path.Combine(imageDirectory, "nemo.png");
            var spagettipath = Path.Combine(imageDirectory, "spagetti.png");
            var placeholderpath = Path.Combine(imageDirectory, "placeholder.png");
            IList<Book> defaultBooks = new List<Book>
            {
                new Book
                {
                    Name="A Gyűrűk Ura",
                    Author="J.R.R Tolkien",
                    ReleaseDate=1968,
                    ISBN="9781402516276",
                    Rents=450,
                    Image=File.Exists(lotrpath) ? File.ReadAllBytes(lotrpath):null,
                    Volumes= new List<Volume>
                    {
                        new Volume
                        {

                            Reservations=new List<Reservation>
                            {
                                new Reservation
                                {
                                    IsActive=true,
                                    Reserver = users[0],
                                    Start=DateTime.Now.AddDays(-3),
                                    End = DateTime.Now.AddDays(-2)
                                },
                                new Reservation
                                {
                                    Reserver = users[1],
                                    IsActive=false,
                                    Start= DateTime.Now.AddDays(3),
                                    End=DateTime.Now.AddDays(5)
                                }

                            }
                            
                        },
                        new Volume
                        {
                            
                            Reservations=new List<Reservation>
                            {
                                new Reservation
                                {
                                    IsActive=true,
                                    Reserver = users[0],
                                    Start=DateTime.Now.AddDays(-2),
                                    End = DateTime.Now.AddDays(2)
                                },
                                new Reservation
                                {
                                    IsActive=false,
                                    Reserver = users[2],
                                    Start= DateTime.Now.AddDays(4),
                                    End=DateTime.Now.AddDays(6)
                                }

                            }
                        },
                        new Volume
                        {
                            
                            Reservations=new List<Reservation>
                            {
                                new Reservation
                                {
                                    IsActive=true,
                                    Reserver = users[3],
                                    Start=DateTime.Now.AddDays(-3),
                                    End = DateTime.Now.AddDays(1)
                                },
                                new Reservation
                                {
                                    IsActive=false,
                                    Reserver = users[4],
                                    Start= DateTime.Now.AddDays(2),
                                    End=DateTime.Now.AddDays(6)
                                }

                            }
                        }
                    }
                },
                new Book
                {
                    Name="Hogyan csináljunk spagetti MVC alkalmazásokat",
                    Author="Kovács Máté",
                    ReleaseDate=2021,
                    ISBN="42069",
                    Image=File.Exists(spagettipath) ? File.ReadAllBytes(spagettipath):null,
                    Rents=1000,
                    Volumes= new List<Volume>
                    {
                        new Volume
                        {
                            
                            Reservations=new List<Reservation>
                            {
                                new Reservation
                                {
                                    IsActive=true,
                                    Reserver = users[5],
                                    Start=DateTime.Now.AddDays(-3),
                                    End = DateTime.Now.AddDays(1)
                                },
                                new Reservation
                                {
                                    IsActive=false,
                                    Reserver = users[1],
                                    Start= DateTime.Now.AddDays(5),
                                    End=DateTime.Now.AddDays(6)
                                }

                            }

                        }


                    }
                },
                new Book
                {
                    Name="Nemo kapitány",
                    Author="Jules Verne",
                    ReleaseDate=1869,
                    ISBN="963 11 1463 5",
                    Rents=53,
                    Image=File.Exists(nemopath) ? File.ReadAllBytes(nemopath):null,
                    Volumes= new List<Volume>
                    {
                        new Volume
                        {
                           
                            Reservations=new List<Reservation>
                            {
                                new Reservation
                                {
                                    IsActive=true,
                                   // Reserver = users[0],
                                    Start=DateTime.Now.AddDays(-3),
                                    End = DateTime.Now.AddDays(1)
                                },
                                new Reservation
                                {
                                    IsActive=false,
                                    Reserver = users[2],
                                    Start= DateTime.Now.AddDays(5),
                                    End=DateTime.Now.AddDays(6)
                                }

                            }

                        }


                    }
                },
                new Book
                {
                    Name="BeoWulf",
                    Author="J.R.R Tolkien",
                    ReleaseDate=2014,
                    ISBN="978 963 07 9937 9",
                    Rents=23,
                    Image=File.Exists(beowulfpath) ? File.ReadAllBytes(beowulfpath):null,
                    Volumes= new List<Volume>
                    {
                        new Volume
                        {
                            
                            Reservations=new List<Reservation>
                            {
                                new Reservation
                                {
                                    IsActive=true,
                                    Reserver = users[3],
                                    Start=DateTime.Now.AddDays(-3),
                                    End = DateTime.Now.AddDays(1)
                                },
                                new Reservation
                                {
                                    IsActive=false,
                                    Reserver = users[2],
                                    Start= DateTime.Now.AddDays(5),
                                    End=DateTime.Now.AddDays(6)
                                }

                            }

                        }


                    }
                }

            };
            for(int i =0;i<30;++i)
            {
                context.AddRange(defaultBooks);
                /* defaultBooks.Append<Book>(
                         new Book
                         {
                             Name = i + "",
                             Author = i+"",
                             ReleaseDate = i,
                             ISBN = "978 963 07 9937 9",
                             Rents = i,
                             Image = File.Exists(placeholderpath) ? File.ReadAllBytes(placeholderpath) : null,
                             Volumes = new List<Volume>
                             {
                                 new Volume
                                 {
                                     Name=i+""
                                 }
                             }
                         }

                     );*/
            }

            context.AddRange(defaultBooks);
            //context.Add(users);
            //context.Add(defaultLists.First()); egyesével
            context.SaveChanges(); //commit kb

        }
    }
}
