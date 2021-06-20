using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Persistence
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("előjegyzett dátum kezdete")]
        public DateTime Start { get; set; }
        [DisplayName("előjegyzett dátum vége")]
        public DateTime End { get; set; }

        
        public int VolumeId { get; set; }
        public Volume volume { get; set; }

        public bool IsActive { get; set; }
        
        public ApplicationUser Reserver { get; set; }
    }
}
