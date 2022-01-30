using System.Collections.ObjectModel;
using System.Windows;
using WpfClient.Command;
using WpfClient.Components.Pages;
using WpfClient.Models;
using WpfClient.Operations;

namespace WpfClient.ViewModel
{
    class UserIndexPageViewModel : ApplicationViewModel
    {
        private readonly ApiOperations _ops;
        private ObservableCollection<User> _userList;

        public UserIndexPageViewModel()
        {
            _ops = new ApiOperations();
            UserList = _ops.GetAllUsers();
        }

        public ObservableCollection<User> UserList
        {
            get => _userList;
            set
            {
                _userList = value;
                OnPropertyChanged();
            }
        }

        public User SelectedUser { get; set; }

        private RelayCommand addCommand;
        private RelayCommand editCommand;
        private RelayCommand delCommand;

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                       (addCommand = new RelayCommand(obj =>
                           {
                               var mainWindow = (MainWindow)Application.Current.MainWindow;
                               mainWindow.MainFrame.NavigationService.Navigate(new UserItemPage());

                           },
                           obj => Globals.IsAdmin()));
            }
        }

        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                       (editCommand = new RelayCommand(obj =>
                           {
                               var mainWindow = (MainWindow)Application.Current.MainWindow;
                               mainWindow.MainFrame.NavigationService.Navigate(new UserItemPage(SelectedUser));
                           },
                           obj => SelectedUser != null));
            }
        }

        public RelayCommand DelCommand
        {
            get
            {
                return delCommand ??
                       (delCommand = new RelayCommand(obj =>
                           {
                               ApiOperations ops = new ApiOperations();
                               if (ops.DelUser(SelectedUser))
                               {
                                   var mainWindow = (MainWindow)Application.Current.MainWindow;
                                   mainWindow.MainFrame.NavigationService.Navigate(new UserIndexPage());
                               }
                           },
                           obj => SelectedUser != null));
            }
        }
    }
}
