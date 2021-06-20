using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Persistence.DTO
{
    public class ReservationDto
    {
      
        public int Id { get; set; }
        
        public DateTime Start { get; set; }
       
        public DateTime End { get; set; }

        public int VolumeId { get; set; }

        public bool IsActive { get; set; }

        public static explicit operator Reservation(ReservationDto dto) => new Reservation
        {
            IsActive = dto.IsActive,
            Id = dto.Id,
            Start = dto.Start,
            End = dto.End,
            VolumeId = dto.VolumeId
            
        };

        public static explicit operator ReservationDto(Reservation v) => new ReservationDto
        {
            IsActive = v.IsActive,
            Id = v.Id,
            Start= v.Start,
            End = v.End,
            VolumeId = v.VolumeId
        };
    }
}

