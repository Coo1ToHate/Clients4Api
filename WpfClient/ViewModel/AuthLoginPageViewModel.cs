using System.Windows;
using System.Windows.Controls;
using WpfClient.Command;
using WpfClient.Components.Pages;
using WpfClient.Models;
using WpfClient.Operations;

namespace WpfClient.ViewModel
{
    class AuthLoginPageViewModel : ApplicationViewModel
    {
        private readonly ApiOperations _ops;
        private string _userName;

        public AuthLoginPageViewModel()
        {
            _ops = new ApiOperations();
        }

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand loginCommand;
        private RelayCommand registerCommand;
        private RelayCommand backCommand;

        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand ??
                       (loginCommand = new RelayCommand(obj =>
                       {
                           PasswordBox pwBox = obj as PasswordBox;
                           User user = _ops.AuthenticateUser(UserName, pwBox.Password);

                           if (user == null)
                           {
                               return;
                           }

                           Globals.LoggedInUser = user;
                           Globals.RoleUser = _ops.RoleUsers(user);
                           
                           var mainWindow = (MainWindow)Application.Current.MainWindow;

                           if (Globals.IsAdmin())
                           {
                               mainWindow.btnUser.Visibility = Visibility.Visible;
                               mainWindow.btnRole.Visibility = Visibility.Visible;
                           }
                           mainWindow.MainFrame.NavigationService.Navigate(new PhoneBookIndexPage());

                       }, obj => Globals.LoggedInUser == null));
            }
        }

        public RelayCommand RegisterCommand
        {
            get
            {
                return registerCommand ??
                       (registerCommand = new RelayCommand(obj =>
                       {
                           var mainWindow = (MainWindow)Application.Current.MainWindow;
                           mainWindow.MainFrame.NavigationService.Navigate(new AuthRegisterPage());
                       }, obj => Globals.LoggedInUser == null));
            }
        }

        public RelayCommand BackCommand
        {
            get
            {
                return backCommand ??
                       (backCommand = new RelayCommand(obj =>
                       {
                           var mainWindow = (MainWindow)Application.Current.MainWindow;
                           mainWindow.MainFrame.NavigationService.GoBack();
                       }));
            }
        }
    }
}
