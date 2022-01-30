using System.Collections.ObjectModel;
using System.Windows;
using WpfClient.Command;
using WpfClient.Components.Pages;
using WpfClient.Models;
using WpfClient.Operations;

namespace WpfClient.ViewModel
{
    class PhoneBookIndexPageViewModel : ApplicationViewModel
    {
        private readonly ApiOperations _ops;
        private ObservableCollection<PhoneBook> _phoneBooks;

        public PhoneBookIndexPageViewModel()
        {
            _ops = new ApiOperations();
            PhoneBooks = _ops.GetAllPhoneBooks();
            if (Globals.LoggedInUser == null)
            {
                VisibilityAddBtn = Visibility.Collapsed;
                VisibilityDelBtn = Visibility.Collapsed;
            }
            else if (!Globals.IsAdmin())
            {
                VisibilityDelBtn = Visibility.Collapsed;
            }
        }

        public Visibility VisibilityAddBtn { get; set; }

        public Visibility VisibilityDelBtn { get; set; }

        public ObservableCollection<PhoneBook> PhoneBooks
        {
            get => _phoneBooks;
            set
            {
                _phoneBooks = value;
                OnPropertyChanged();
            }
        }

        public PhoneBook SelectedPhoneBook { get; set; }

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
                           mainWindow.MainFrame.NavigationService.Navigate(new PhoneBookItemPage());

                       },
                           obj => Globals.LoggedInUser != null));
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
                             mainWindow.MainFrame.NavigationService.Navigate(new PhoneBookItemPage(SelectedPhoneBook));
                         },
                             obj => SelectedPhoneBook != null));
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
                           if (ops.DelPhoneBook(SelectedPhoneBook))
                           {
                               var mainWindow = (MainWindow)Application.Current.MainWindow;
                               mainWindow.MainFrame.NavigationService.Navigate(new PhoneBookIndexPage());
                           }
                       },
                           obj => SelectedPhoneBook != null));
            }
        }
    }
}
