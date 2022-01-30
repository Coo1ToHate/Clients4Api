using System.Collections.ObjectModel;
using System.Windows;
using WpfClient.Command;
using WpfClient.Components.Pages;
using WpfClient.Models;
using WpfClient.Operations;

namespace WpfClient.ViewModel
{
    class RoleIndexPageViewModel : ApplicationViewModel
    {
        private readonly ApiOperations _ops;
        private ObservableCollection<Role> _roleList;

        public RoleIndexPageViewModel()
        {
            _ops = new ApiOperations();
            RoleList = _ops.GetAllRoles();

        }

        public ObservableCollection<Role> RoleList
        {
            get => _roleList;
            set
            {
                _roleList = value;
                OnPropertyChanged();
            }
        }

        public Role SelectedRole { get; set; }

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
                               mainWindow.MainFrame.NavigationService.Navigate(new RoleItemPage());

                           },
                           obj => Globals.LoggedInUser != null && Globals.IsAdmin()));
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
                               mainWindow.MainFrame.NavigationService.Navigate(new RoleItemPage(SelectedRole));
                           },
                           obj => SelectedRole != null));
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
                               if (ops.DelRole(SelectedRole))
                               {
                                   var mainWindow = (MainWindow)Application.Current.MainWindow;
                                   mainWindow.MainFrame.NavigationService.Navigate(new RoleIndexPage());
                               }
                           },
                           obj => SelectedRole != null));
            }
        }
    }
}
