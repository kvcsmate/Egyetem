using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Web.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Cím")]
        public String Name { get; set; }

        [Required]
        [DisplayName("ISBN azonosító")]
        public string ISBN { get; set; }

        [Required]
        [DisplayName("Szerző")]
        public string Author { get; set; }
        [Required]
        [DisplayName("Kiadási év")]
        public int ReleaseDate { get; set; }

        public byte[] Image { get; set; }
        [Required]
        [DisplayName("Kölcsönzések száma")]
        public int Rents { get; set; }

        public virtual ICollection<Volume> Volumes { get; set; }



    }
}
