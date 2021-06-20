using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Model
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public int VolumeId { get; set; }
        public Volume volume { get; set; }
    }
}
