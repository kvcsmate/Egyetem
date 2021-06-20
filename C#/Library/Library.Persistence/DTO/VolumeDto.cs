using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Persistence.DTO
{
    public class VolumeDto
    {
        public int Id { get; set; }



        public int BookId { get; set; }


        public static explicit operator Volume(VolumeDto dto) => new Volume
        {
            Id = dto.Id,
            BookId = dto.BookId
        };

        public static explicit operator VolumeDto(Volume v) => new VolumeDto
        {
            Id = v.Id,
            BookId = v.BookId
        };
    }
}
