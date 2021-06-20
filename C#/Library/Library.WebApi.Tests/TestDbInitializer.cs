using Library.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.WebApi.Tests
{
    public class TestDbInitializer
    {
        public static void Initialize(LibraryDbContext context)
        {
            //context.Database.Migrate(); //progam automatikusan migrál
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            IList<Book> defaultBooks = new List<Book>
            {
                new Book
                {
                    Id=1,
                    Name="A Gyűrűk Ura",
                    Author="J.R.R Tolkien",
                    ReleaseDate=1968,
                    ISBN="9781402516276",
                    Rents=450,

                    Volumes= new List<Volume>
                    {
                        new Volume
                        {
                            Id=111,
                            Reservations=new List<Reservation>
                            {
                                new Reservation
                                {
                                    Id=1111,
                                    IsActive=true,
                                    Start=DateTime.Now.AddDays(-1),
                                    End = DateTime.Now.AddDays(2)
                                },
                                new Reservation
                                {
                                    Id=1112,
                                    IsActive=false,
                                    Start= DateTime.Now.AddDays(3),
                                    End=DateTime.Now.AddDays(5)
                                }

                            }

                        },
                        new Volume
                        {
                            Id=112,
                            Reservations=new List<Reservation>
                            {
                                new Reservation
                                {
                                    Id=1121,
                                    IsActive=true,
                                    Start=DateTime.Now.AddDays(-2),
                                    End = DateTime.Now.AddDays(2)
                                },
                                new Reservation
                                {
                                    Id=1122,
                                    IsActive=false,
                                    Start= DateTime.Now.AddDays(4),
                                    End=DateTime.Now.AddDays(6)
                                }

                            }
                        },
                        new Volume
                        {
                            Id=113,
                            Reservations=new List<Reservation>
                            {
                                new Reservation
                                {
                                    Id=1131,
                                    IsActive=true,
                                    Start=DateTime.Now.AddDays(-3),
                                    End = DateTime.Now.AddDays(1)
                                },
                                new Reservation
                                {
                                    Id=1132,
                                    IsActive=false,
                                    Start= DateTime.Now.AddDays(2),
                                    End=DateTime.Now.AddDays(6)
                                }

                            }
                        }
                    }
                },
                new Book
                {
                    Id=2,
                    Name="Hogyan csináljunk spagetti MVC alkalmazásokat",
                    Author="Kovács Máté",
                    ReleaseDate=2021,
                    ISBN="42069",
                    
                    Rents=1000,
                    Volumes= new List<Volume>
                    {
                        new Volume
                        {
                            Id=444,
                            Reservations=new List<Reservation>
                            {
                                new Reservation
                                {
                                    Id=4441,
                                    IsActive=true,
                                    Start=DateTime.Now.AddDays(-3),
                                    End = DateTime.Now.AddDays(1)
                                },
                                new Reservation
                                {
                                    Id=4442,
                                    IsActive=false,
                                    Start= DateTime.Now.AddDays(5),
                                    End=DateTime.Now.AddDays(6)
                                }

                            }

                        }


                    }
                },
                new Book
                {
                    Id=3,
                    Name="Nemo kapitány",
                    Author="Jules Verne",
                    ReleaseDate=1869,
                    ISBN="963 11 1463 5",
                    Rents=53,
                    
                    Volumes= new List<Volume>
                    {
                        new Volume
                        {

                            Reservations=new List<Reservation>
                            {
                                new Reservation
                                {
                                    IsActive=true,
                                    Start=DateTime.Now.AddDays(-3),
                                    End = DateTime.Now.AddDays(1)
                                },
                                new Reservation
                                {
                                    IsActive=false,
                                    Start= DateTime.Now.AddDays(5),
                                    End=DateTime.Now.AddDays(6)
                                }

                            }

                        }


                    }
                },
                new Book
                {
                    Id=4,
                    Name="BeoWulf",
                    Author="J.R.R Tolkien",
                    ReleaseDate=2014,
                    ISBN="978 963 07 9937 9",
                    Rents=23,
                    
                    Volumes= new List<Volume>
                    {
                        new Volume
                        {

                            Reservations=new List<Reservation>
                            {
                                new Reservation
                                {
                                    IsActive=true,
                                    Start=DateTime.Now.AddDays(-3),
                                    End = DateTime.Now.AddDays(1)
                                },
                                new Reservation
                                {
                                    IsActive=false,
                                    Start= DateTime.Now.AddDays(5),
                                    End=DateTime.Now.AddDays(6)
                                }

                            }

                        }


                    }
                }

            };

            context.AddRange(defaultBooks);
            //context.Add(defaultLists.First()); egyesével
            context.SaveChanges(); //commit kb

        }
    }
}