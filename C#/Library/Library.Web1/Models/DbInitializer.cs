using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Web.Models
{   
    public class DbInitializer
    {

        public static void Initialize(LibraryDbContext context, string imageDirectory) 
        {
            context.Database.Migrate(); //progam automatikusan migrál
            if (context.Books.Any()) //vannak-e már adatok
            {
               //context.RemoveRange(context.Books);
                return;
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
                            Name="A Gyűrű szövetsége",
                            Reservations=new List<Reservation>
                            {
                                new Reservation
                                {
                                    Start=DateTime.Now.AddDays(-1),
                                    End = DateTime.Now.AddDays(2)
                                },
                                new Reservation
                                {
                                    Start= DateTime.Now.AddDays(3),
                                    End=DateTime.Now.AddDays(5)
                                }

                            }
                            
                        },
                        new Volume
                        {
                            Name="A két torony",
                            Reservations=new List<Reservation>
                            {
                                new Reservation
                                {
                                    Start=DateTime.Now.AddDays(-2),
                                    End = DateTime.Now.AddDays(2)
                                },
                                new Reservation
                                {
                                    Start= DateTime.Now.AddDays(4),
                                    End=DateTime.Now.AddDays(6)
                                }

                            }
                        },
                        new Volume
                        {
                            Name="A király visszatér",
                            Reservations=new List<Reservation>
                            {
                                new Reservation
                                {
                                    Start=DateTime.Now.AddDays(-3),
                                    End = DateTime.Now.AddDays(1)
                                },
                                new Reservation
                                {
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
                            Name="Hogyan csináljunk spagetti MVC alkalmazásokat 1. kiadás",
                            Reservations=new List<Reservation>
                            {
                                new Reservation
                                {
                                    Start=DateTime.Now.AddDays(-3),
                                    End = DateTime.Now.AddDays(1)
                                },
                                new Reservation
                                {
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
                            Name="Nemo kapitány 8.Kiadás",
                            Reservations=new List<Reservation>
                            {
                                new Reservation
                                {
                                    Start=DateTime.Now.AddDays(-3),
                                    End = DateTime.Now.AddDays(1)
                                },
                                new Reservation
                                {
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
                            Name="Beowulf Európa Kiadó",
                            Reservations=new List<Reservation>
                            {
                                new Reservation
                                {
                                    Start=DateTime.Now.AddDays(-3),
                                    End = DateTime.Now.AddDays(1)
                                },
                                new Reservation
                                {
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
            //context.Add(defaultLists.First()); egyesével
            context.SaveChanges(); //commit kb

        }
    }
}
