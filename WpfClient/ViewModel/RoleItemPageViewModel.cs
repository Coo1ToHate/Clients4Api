using System.Windows;
using WpfClient.Command;
using WpfClient.Components.Pages;
using WpfClient.Models;
using WpfClient.Operations;

namespace WpfClient.ViewModel
{
    class RoleItemPageViewModel : ApplicationViewModel
    {
        private readonly ApiOperations _ops;
        private string _nameRole;

        public RoleItemPageViewModel(Role role = null)
        {
            _ops = new ApiOperations();
            Role = role;
            if (role != null)
            {
                NameRole = Role.Name;
                VisibilityAddBtn = Visibility.Collapsed;
            }
            else
            {
                VisibilityEditBtn = Visibility.Collapsed;
            }
        }

        public Role Role { get; }

        public string NameRole
        {
            get => _nameRole;
            set
            {
                _nameRole = value;
                OnPropertyChanged();
            }
        }

        public Visibility VisibilityAddBtn { get; set; }
        public Visibility VisibilityEditBtn { get; set; }

        private RelayCommand addCommand;
        private RelayCommand editCommand;
        private RelayCommand backCommand;

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                       (addCommand = new RelayCommand(obj =>
                       {
                           Role newRole = new Role
                           {
                               Name = NameRole
                           };

                           if (_ops.AddRole(newRole))
                           {
                               var mainWindow = (MainWindow)Application.Current.MainWindow;
                               mainWindow.MainFrame.NavigationService.Navigate(new RoleIndexPage());
                           }
                       }));
            }
        }

        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                       (editCommand = new RelayCommand(obj =>
                       {
                           Role newRole = new Role
                           {
                               Id = Role.Id,
                               Name = NameRole
                           };

                           if (_ops.EditRole(newRole))
                           {
                               var mainWindow = (MainWindow)Application.Current.MainWindow;
                               mainWindow.MainFrame.NavigationService.Navigate(new RoleIndexPage());
                           }
                       }));
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
