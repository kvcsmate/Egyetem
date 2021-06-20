using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Library.Desktop.Model;
using Library.Persistence.DTO;

namespace Library.Desktop.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly LibraryApiService _service;
        private ObservableCollection<BookViewModel> _books;
        private ObservableCollection<VolumeViewModel> _volumes;
        private ObservableCollection<ReservationViewModel> _reservations;

        private BookViewModel _selectedBook;
        private VolumeViewModel _selectedVolume;
        private ReservationViewModel _selectedReservation;
        private BookViewModel _editableBook;
        private String _selectedBookName;

        public VolumeViewModel SelectedVolume
        {
            get { return _selectedVolume; }
            set { _selectedVolume = value; OnPropertyChanged(); }
        }

        public BookViewModel SelectedBook
        {
            get { return _selectedBook; }
            set { _selectedBook = value; OnPropertyChanged(); }
        }

        public String SelectedBookName
        {
            get { return _selectedBookName; }
            set { _selectedBookName = value; OnPropertyChanged(); }
        }
        public ReservationViewModel SelectedReservation
        {
            get { return _selectedReservation; }
            set { _selectedReservation = value; OnPropertyChanged(); }
        }
        public BookViewModel EditableBook
        {
            get { return _editableBook; }
            set { _editableBook = value; OnPropertyChanged(); }
        }

        public ObservableCollection<BookViewModel> Books
        {
            get { return _books; }
            set { _books = value; OnPropertyChanged(); }
        }
        public ObservableCollection<ReservationViewModel> Reservations
        {
            get { return _reservations; }
            set { _reservations = value; OnPropertyChanged(); }
        }
          

        public ObservableCollection<VolumeViewModel> Volumes
        {
            get { return _volumes; }
            set { _volumes = value; OnPropertyChanged(); }
        }

        public DelegateCommand RefreshBooksCommand { get; private set; }
        public DelegateCommand SelectBookCommand { get; private set; }
        public DelegateCommand SelectVolumeCommand { get; private set; }
        public DelegateCommand LogoutCommand { get; private set; }

        public DelegateCommand AddBookCommand { get; private set; }
        public DelegateCommand AddVolumeCommand { get; private set; }
        public DelegateCommand DeleteVolumeCommand { get; private set; }
        public DelegateCommand DeleteBookCommand { get; private set; }
        public DelegateCommand SwitchReservationCommand { get; private set; }

        public DelegateCommand EditBookCommand { get; private set; }
        public DelegateCommand SaveBookEditCommand { get; private set; }
        public DelegateCommand CancelBookEditCommand { get; private set; }
        public DelegateCommand ChangeImageCommand { get; private set; }

        public event EventHandler StartingBookEdit;

        public event EventHandler FinishingBookEdit;

        public event EventHandler StartingImageChange;
        public event EventHandler LogoutSucceeded;

        public MainViewModel(LibraryApiService service)
        {
            _service = service;

            RefreshBooksCommand = new DelegateCommand(_ => LoadBooksAsync());
            SelectBookCommand = new DelegateCommand(_ => LoadVolumesAsync(SelectedBook));
            SelectVolumeCommand = new DelegateCommand(_ => LoadReservationsAsync(SelectedVolume));
            LogoutCommand = new DelegateCommand(_ => LogoutAsync());

            AddBookCommand = new DelegateCommand(_ => AddBookAsync());
            AddVolumeCommand = new DelegateCommand(_ => !(SelectedBook is null), _ => AddVolume());
            DeleteVolumeCommand = new DelegateCommand(_ => !(SelectedVolume is null), _ => DeleteVolume(SelectedVolume));
            DeleteBookCommand = new DelegateCommand(_ => !(SelectedBook is null), _ => DeleteBook(SelectedBook));

            SwitchReservationCommand = new DelegateCommand(_ => !(SelectedReservation is null || SelectedVolume is null),_ => SwitchReservation(SelectedReservation, SelectedVolume));
            EditBookCommand = new DelegateCommand(_ => !(SelectedBook is null), _ => StartEditBook());
            CancelBookEditCommand = new DelegateCommand(_ => CancelBookEdit());
            SaveBookEditCommand = new DelegateCommand(_ => SaveBookEdit());

            ChangeImageCommand = new DelegateCommand(_ => StartingImageChange?.Invoke(this, EventArgs.Empty));

        }

        private async void DeleteBook(BookViewModel book)
        {
            try
            {
                await _service.DeleteBookAsync(book.Id);
                Books.Remove(SelectedBook);
                SelectedBook = null;
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        private void StartEditBook()
        {
            EditableBook = SelectedBook.ShallowClone();
            StartingBookEdit?.Invoke(this, EventArgs.Empty);
        }

        private async void SaveBookEdit()
        {
            try
            {
                SelectedBook.CopyFrom(EditableBook);
                await _service.UpdateBookAsync((BookDto)SelectedBook);
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            FinishingBookEdit?.Invoke(this, EventArgs.Empty);
        }
    

        private void CancelBookEdit()
        {
            EditableBook = null;
            FinishingBookEdit?.Invoke(this, EventArgs.Empty);
        }

        private async void SwitchReservation(ReservationViewModel r, VolumeViewModel v)
        {
            try
            {
                await _service.UpdateReservationAsync((ReservationDto)SelectedReservation); 
                SelectedReservation.IsActive = !SelectedReservation.IsActive;
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            
        }

        private async void DeleteVolume(VolumeViewModel volume)
        {
            try
            {
               
               if(await _service.DeleteVolumeAsync(volume.Id))
                {
                    Volumes.Remove(SelectedVolume);
                    SelectedVolume = null;
                }   
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        private async void AddVolume()
        {
            var newVolume = new VolumeViewModel
            {
                BookId = SelectedBook.Id
            };

            var itemDto = (VolumeDto)newVolume;

            try
            {
                await _service.CreateVolumeAsync(itemDto);
                newVolume.Id = itemDto.Id;
                Volumes.Add(newVolume);
                SelectedVolume = newVolume;
            }

            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        private async void AddBookAsync()
        {
            var newBook = new BookViewModel
            {
                Name = "New Book",
                Image = null,
                ISBN="",
                ReleaseDate=0,
                Author="",
                Rents=0

            };

            var bookDto = (BookDto)newBook;

            try
            {
                await _service.CreateBookAsync(bookDto);
                newBook.Id = bookDto.Id;
                Books.Add(newBook);
                SelectedBook = newBook;
            }

            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        private async void LogoutAsync()
        {
            try
            {
                await _service.LogoutAsync();
                LogoutSucceeded?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        private async void LoadBooksAsync()
        {
            try
            {
                Books = new ObservableCollection<BookViewModel>((await _service.LoadBooksAsync()).Select(book => (BookViewModel)book));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }
        private async void LoadVolumesAsync(BookViewModel book)
        {
            
            if (book is null)
                return;

            try
            {
                Volumes = new ObservableCollection<VolumeViewModel>((await _service.LoadVolumesAsync(book.Id)).Select(volume => (VolumeViewModel)volume));
                
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }
        private async void LoadReservationsAsync(VolumeViewModel volume)
        {
            if (volume is null)
                return;

            try
            {
                Reservations = new ObservableCollection<ReservationViewModel>((await _service.LoadReservationsAsync(volume.Id)).Select(res => (ReservationViewModel)res));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }


    }
}
