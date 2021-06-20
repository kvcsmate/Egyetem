using Library.Persistence.DTO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Library.Desktop.Model
{
    public class LibraryApiService
    {
        private readonly HttpClient _client;
        public LibraryApiService(string baseAddress)
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(baseAddress)
            };
        }

        public async Task<bool> LoginAsync(string userName, string password)
        {
            LoginDto user = new LoginDto
            {
                UserName = userName,
                Password = password
            };
            var response= await _client.PostAsJsonAsync("api/Account/Login", user);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            if (response.StatusCode==HttpStatusCode.Unauthorized)
            {
                return false;
            }
            throw new NetworkException("Service returned response: " + response.StatusCode);
        }
        public async Task LogoutAsync()
        {
            HttpResponseMessage response = await _client.PostAsync("api/Account/Logout", null);

            if (response.IsSuccessStatusCode)
            {
                return;
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        #region Books
        public async Task<IEnumerable<BookDto>> LoadBooksAsync()
        {
            var response = await _client.GetAsync("api/Books/");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<BookDto>>();
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }
        public async Task CreateBookAsync(BookDto book)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/books/", book);
            book.Id = (await response.Content.ReadAsAsync<BookDto>()).Id;

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        public async Task UpdateBookAsync(BookDto book)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"api/books/{book.Id}", book);

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        public async Task DeleteBookAsync(Int32 bookId)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"api/books/{bookId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }
        #endregion

        #region Volumes
        public async Task CreateVolumeAsync(VolumeDto volume)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/volumes/", volume);
            volume.Id = (await response.Content.ReadAsAsync<VolumeDto>()).Id;

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        public async Task UpdateVolumeAsync(VolumeDto volume)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"api/volumes/{volume.Id}", volume);

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        public async Task<bool> DeleteVolumeAsync(Int32 volumeId)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"api/volumes/{volumeId}");
            bool isok = true;
            if (!response.IsSuccessStatusCode)
            {
                isok = false;
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
            return isok;
        }
        
        public async Task<IEnumerable<VolumeDto>> LoadVolumesAsync(int bookId)
        {
            var response = await _client.GetAsync($"api/Volumes/Book/{bookId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<VolumeDto>>();
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }

        #endregion
        #region Reservations
        public async Task<IEnumerable<ReservationDto>> LoadReservationsAsync(int volumeId)
        {
            var response = await _client.GetAsync($"api/Reservations/Volume/{volumeId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<ReservationDto>>();
            }

            throw new NetworkException("Service returned response: " + response.StatusCode);
        }
        public async Task CreateReservationAsync(ReservationDto reservation)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync("api/reservations/", reservation);
            reservation.Id = (await response.Content.ReadAsAsync<ReservationDto>()).Id;

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }

        public async Task<bool> UpdateReservationAsync(ReservationDto reservation)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync($"api/reservations/{reservation.Id}", reservation);
            bool isok = true;
            if (!response.IsSuccessStatusCode)
            {
                isok = false;
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
            return isok;
        }

        public async Task DeleteReservationAsync(Int32 reservationId)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"api/reservations/{reservationId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new NetworkException("Service returned response: " + response.StatusCode);
            }
        }
        #endregion

    }
}
