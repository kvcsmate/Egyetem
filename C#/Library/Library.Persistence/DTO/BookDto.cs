using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Persistence.DTO
{
    public class BookDto
    {
        
        public int Id { get; set; }

        public String Name { get; set; }

       
        public string ISBN { get; set; }

       
        public string Author { get; set; }
       
        public int ReleaseDate { get; set; }

        public byte[] Image { get; set; }
       
        public int Rents { get; set; }

        public virtual ICollection<Volume> Volumes { get; set; }

        public static explicit operator Book(BookDto dto) => new Book
        {
            Id = dto.Id,
            Name = dto.Name,
            Author = dto.Author,
            ReleaseDate = dto.ReleaseDate,
            Image = dto.Image,
            Rents = dto.Rents,
            ISBN = dto.ISBN
        };

        public static explicit operator BookDto(Book b) => new BookDto
        {
            Id = b.Id,
            Name = b.Name,
            Author = b.Author,
            ReleaseDate = b.ReleaseDate,
            Image = b.Image,
            Rents = b.Rents,
            ISBN = b.ISBN
        };
    }
}
