using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Web.Models
{
    public class Volume
    {
        public int Id { get; set; }
        [DisplayName("Kötet címe")]
        public string Name { get; set; }

        //  public virtual ICollection<DateTime> BeginDate { get; set; }

        // public ICollection<DateTime> EndDate { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }

        public int BookId { get; set; }

        public Book book { get; set; }

    }
}
