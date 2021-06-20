using Library.Desktop.Model;
using Library.Desktop.View;
using Library.Desktop.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Library.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private LibraryApiService _service;
        private MainViewModel _mainViewModel;
        private LoginViewModel _loginViewModel;
        private LoginWindow _loginView;
        private MainWindow _mainView;
        private BookEditorWindow _editorView;

        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _service = new LibraryApiService(ConfigurationManager.AppSettings["baseAddress"]);

            

            _loginViewModel = new LoginViewModel(_service);
            _loginViewModel.LoginSucceeded += _loginViewModel_LoginSucceeded;
            _loginViewModel.LoginFailed += _loginViewModel_LoginFailed;
            _loginViewModel.MessageApplication += onMessageApplication;

            _loginView = new LoginWindow
            {
                DataContext = _loginViewModel
            };

            _mainViewModel = new MainViewModel(_service);
            _mainViewModel.MessageApplication += onMessageApplication;
            _mainViewModel.LogoutSucceeded += _mainViewModel_LogoutSucceeded;
            _mainViewModel.StartingBookEdit += _mainViewModel_StartingBookEdit;
            _mainViewModel.FinishingBookEdit += _mainViewModel_FinishingBookEdit;
            _mainViewModel.StartingImageChange += _mainViewModel_StartingBookChange;
            _mainView = new MainWindow
            {
                DataContext = _mainViewModel
            };

            _loginView.Show();
        }

        private void _mainViewModel_StartingBookEdit(object sender, EventArgs e)
        {
            _editorView = new BookEditorWindow
            {
                DataContext = _mainViewModel
            };
            _editorView.ShowDialog();
        }

        private void _mainViewModel_FinishingBookEdit(object sender, EventArgs e)
        {
            if (_editorView.IsActive)
            {
                _editorView.Close();
            }
        }

        private async void _mainViewModel_StartingBookChange(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Filter = "Images|*.jpg;*.jpeg;*.bmp;*.tif;*.gif;*.png;",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };
            if (dialog.ShowDialog(_editorView).GetValueOrDefault(false))
            {
                _mainViewModel.EditableBook.Image = await File.ReadAllBytesAsync(dialog.FileName);
            }
        }

        private void _mainViewModel_LogoutSucceeded(object sender, EventArgs e)
        {
            _mainView.Hide();
            _loginView.Show();
        }

        private void _loginViewModel_LoginFailed(object sender, EventArgs e)
        {
            MessageBox.Show("Login Failed!", "Library", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void _loginViewModel_LoginSucceeded(object sender, EventArgs e)
        {
            _loginView.Hide();
            _mainView.Show();
        }

        private void onMessageApplication(object sender, MessageEventArgs e)
        {
            MessageBox.Show(e.Message, "Library", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
    }
}
