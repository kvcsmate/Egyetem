using Library.Desktop.Model;
using Library.Persistence.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Controls;

namespace Library.Desktop.ViewModel
{
    public class LoginViewModel:ViewModelBase
    {
        public Boolean IsLoading { get; set; }

        private readonly LibraryApiService _service;

        public String UserName { get; set; }

        public event EventHandler LoginSucceeded;

        public event EventHandler LoginFailed;

        public DelegateCommand LoginCommand { get; set; }

        public LoginViewModel(LibraryApiService service)
        {
            _service = service;

            IsLoading = false;

            LoginCommand = new DelegateCommand(_ => !IsLoading, param => LoginAsync(param as PasswordBox));
        }

        private async void LoginAsync(PasswordBox passwordBox)
        {
            try
            {
                IsLoading = true;
                Boolean result = await _service.LoginAsync(UserName, passwordBox.Password);

                if (result)
                    LoginSucceeded?.Invoke(this, EventArgs.Empty);
                else
                    LoginFailed?.Invoke(this, EventArgs.Empty);

            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            
            finally
            {
                IsLoading = false;
            }
        }
    }
}
