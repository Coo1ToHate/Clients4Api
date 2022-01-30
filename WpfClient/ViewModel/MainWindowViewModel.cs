using System.Windows;
using WpfClient.Command;
using WpfClient.Components.Pages;
using WpfClient.Models;

namespace WpfClient.ViewModel
{
    class MainWindowViewModel : ApplicationViewModel
    {
        private RelayCommand phoneBookCommand;
        private RelayCommand loginCommand;
        private RelayCommand userCommand;
        private RelayCommand roleCommand;

        public RelayCommand PhoneBookCommand
        {
            get
            {
                return phoneBookCommand ??
                       (phoneBookCommand = new RelayCommand(obj =>
                           {
                               var mainWindow = (MainWindow)Application.Current.MainWindow;
                               mainWindow.MainFrame.NavigationService.Navigate(new PhoneBookIndexPage());
                           }));
            }
        }

        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand ??
                       (loginCommand = new RelayCommand(obj =>
                       {
                           var mainWindow = (MainWindow)Application.Current.MainWindow;
                           if (Globals.LoggedInUser == null)
                           {
                               mainWindow.MainFrame.NavigationService.Navigate(new AuthLoginPage());
                           }
                           else
                           {
                               mainWindow.MainFrame.NavigationService.Navigate(new UserItemPage(Globals.LoggedInUser));
                           }
                       }));
            }
        }

        public RelayCommand UserCommand
        {
            get
            {
                return userCommand ??
                       (userCommand = new RelayCommand(obj =>
                       {
                           var mainWindow = (MainWindow)Application.Current.MainWindow;
                           mainWindow.MainFrame.NavigationService.Navigate(new UserIndexPage());
                       },
                           obj => Globals.IsAdmin()));
            }
        }

        public RelayCommand RoleCommand
        {
            get
            {
                return roleCommand ??
                       (roleCommand = new RelayCommand(obj =>
                           {
                               var mainWindow = (MainWindow)Application.Current.MainWindow;
                               mainWindow.MainFrame.NavigationService.Navigate(new RoleIndexPage());
                           },
                           obj => Globals.IsAdmin()));
            }
        }
    }
}
