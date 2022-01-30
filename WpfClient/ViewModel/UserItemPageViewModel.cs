using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfClient.Command;
using WpfClient.Components.Pages;
using WpfClient.Models;
using WpfClient.Operations;

namespace WpfClient.ViewModel
{
    class UserItemPageViewModel : ApplicationViewModel
    {
        private readonly ApiOperations _ops;
        private readonly User _user;
        private string _userName;
        private ObservableCollection<Role> _roles;
        private Role _selectedRole;

        public UserItemPageViewModel(User user = null)
        {
            _ops = new ApiOperations();
            _user = user;
            VisibilityExitBtn = Visibility.Collapsed;
            if (User != null)
            {
                VisibilityPassword = Visibility.Collapsed;
                VisibilityAddBtn = Visibility.Collapsed;
                UserName = User.UserName;
                Roles = _ops.GetAllRoles();
                SelectedRole = Roles.FirstOrDefault(n => n.Name.Contains(_ops.RoleUsers(user).FirstOrDefault()));

                if (!Globals.IsAdmin())
                {
                    VisibilityEditBtn = Visibility.Collapsed;
                }

                if (Globals.LoggedInUser.Id.Contains(User.Id))
                {
                    VisibilityExitBtn = Visibility.Visible;
                }
            }
            else
            {
                VisibilityRoles = Visibility.Collapsed;
                VisibilityEditBtn = Visibility.Collapsed;
            }
        }

        public ObservableCollection<Role> Roles
        {
            get => _roles;
            set
            {
                _roles = value;
                OnPropertyChanged();
            }
        }

        public Role SelectedRole
        {
            get => _selectedRole;
            set
            {
                _selectedRole = value;
                OnPropertyChanged();
            }
        }

        public User User => _user;

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        public Visibility VisibilityAddBtn { get; set; }
        public Visibility VisibilityEditBtn { get; set; }
        public Visibility VisibilityExitBtn { get; set; }
        public Visibility VisibilityPassword { get; set; }
        public Visibility VisibilityRoles { get; set; }

        private RelayCommand addCommand;
        private RelayCommand editCommand;
        private RelayCommand backCommand;
        private RelayCommand exitCommand;

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                       (addCommand = new RelayCommand(obj =>
                       {
                           PasswordBox pwBox = obj as PasswordBox;

                           User user = _ops.AddUser(UserName, pwBox.Password);

                           if (user == null)
                           {
                               MessageBox.Show("Username already exists");
                               return;
                           }

                           var mainWindow = (MainWindow)Application.Current.MainWindow;
                           mainWindow.MainFrame.NavigationService.Navigate(new UserIndexPage());

                       }, obj => Globals.IsAdmin()));
            }
        }

        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                       (editCommand = new RelayCommand(obj =>
                       {
                           User newUser = new User
                           {
                               Id = User.Id,
                               UserName = UserName
                           };

                           if (_ops.EditUser(newUser) && _ops.EditRoleUser(newUser, SelectedRole.Name))
                           {
                               var mainWindow = (MainWindow)Application.Current.MainWindow;
                               mainWindow.MainFrame.NavigationService.Navigate(new UserIndexPage());
                           }

                       }, obj => UserName != null));
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

        public RelayCommand ExitCommand
        {
            get
            {
                return exitCommand ??
                       (exitCommand = new RelayCommand(obj =>
                       {
                           Globals.LoggedInUser = null;
                           Globals.RoleUser = null;
                           var mainWindow = (MainWindow)Application.Current.MainWindow;
                           mainWindow.btnUser.Visibility = Visibility.Collapsed;
                           mainWindow.btnRole.Visibility = Visibility.Collapsed;
                           mainWindow.MainFrame.NavigationService.Navigate(new PhoneBookIndexPage());
                       }));
            }
        }
    }
}
