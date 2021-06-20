using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Model
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Author { get; set; }
        [Required]
        public int ReleaseDate { get; set; }

        public byte[] Image { get; set; }
        [Required]
        public int Rents { get; set; }

        public virtual ICollection<Volume> Volumes { get; set; }



    }
}
