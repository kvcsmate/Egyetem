    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Persistence
{
    public class ReservationViewModel
    {
        
        [Key]
        public int Id { get; set; }
        [DisplayName("előjegyzett dátum kezdete")]
        public DateTime Start { get; set; }
        [DisplayName("előjegyzett dátum vége")]
         public DateTime End { get; set; }
         public int VolumeId { get; set; }

        public static explicit operator Reservation(ReservationViewModel wm) => new Reservation
        {
            Id=wm.Id,
            VolumeId=wm.VolumeId,
            End=wm.End,
            Start=wm.Start
        };
        public static explicit operator ReservationViewModel(Reservation r) => new ReservationViewModel
        {
            Id = r.Id,
            VolumeId = r.VolumeId,
            End = r.End,
            Start = r.Start
        };

    }
}
