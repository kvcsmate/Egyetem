using Library.Persistence.DTO;
using System;

namespace Library.Desktop.ViewModel
{
    public class BookViewModel :ViewModelBase
    {
        private byte[] _image { get; set; }
        public byte[] Image
        {
            get { return _image; }
            set { _image = value; OnPropertyChanged(); }
        }
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }

        private String _name;

        public String Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }
        private String _isbn;
        public String ISBN {
            get { return _isbn ; }
            set { _isbn = value;OnPropertyChanged(); } 
        }

        private string _author;
        public string Author
        {
            get { return _author; }
            set { _author = value; OnPropertyChanged(); }
        }

        private int _releaseDate;
        public int ReleaseDate
        {
            get { return _releaseDate; }
            set { _releaseDate = value; OnPropertyChanged(); }
        }
        private int _rents;
        public int Rents
        {
            get { return _rents; }
            set { _rents = value; OnPropertyChanged(); }
        }

        public BookViewModel ShallowClone()
        {
            return (BookViewModel)this.MemberwiseClone();
        }

        public void CopyFrom(BookViewModel v)
        {
            Id = v.Id;
            ISBN = v.ISBN;
            Name = v.Name;
            Image = v.Image;
            Author = v.Author;
            Rents = v.Rents;
            ReleaseDate = v.ReleaseDate;
        }

        public static explicit operator BookViewModel(BookDto dto) => new BookViewModel
        {
            Id = dto.Id,
            Name = dto.Name,
            Author = dto.Author,
            ReleaseDate = dto.ReleaseDate,
            Image = dto.Image,
            Rents = dto.Rents,
            ISBN = dto.ISBN
        };

        public static explicit operator BookDto(BookViewModel vm) => new BookDto
        {
            Id = vm.Id,
            Name = vm.Name,
            Author = vm.Author,
            ReleaseDate = vm.ReleaseDate,
            Image = vm.Image,
            Rents = vm.Rents,
            ISBN = vm.ISBN
        };
        

       

        

    }
}
