using Library.Persistence.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Desktop.ViewModel
{
    public class VolumeViewModel:ViewModelBase
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }

        private int _bookId;
        public int BookId
        {
            get { return _bookId; }
            set { _bookId = value; OnPropertyChanged(); }
        }

        public VolumeViewModel ShallowClone()
        {
            return (VolumeViewModel)this.MemberwiseClone();
        }

        public void CopyFrom(VolumeViewModel v)
        {
            Id = v.Id;
            BookId = v.BookId;
        }

        public static explicit operator VolumeViewModel(VolumeDto dto) => new VolumeViewModel
        {
            Id = dto.Id,
            BookId = dto.BookId
        };

        public static explicit operator VolumeDto(VolumeViewModel vm) => new VolumeDto
        {
            Id = vm.Id,
            BookId = vm.BookId
        };
    }
}

