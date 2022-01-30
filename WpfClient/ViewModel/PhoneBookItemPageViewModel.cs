using System.Windows;
using WpfClient.Command;
using WpfClient.Components.Pages;
using WpfClient.Models;
using WpfClient.Operations;

namespace WpfClient.ViewModel
{
    class PhoneBookItemPageViewModel : ApplicationViewModel
    {
        private readonly ApiOperations _ops;
        private readonly PhoneBook _phoneBook;
        private string _lastName;
        private string _firstName;
        private string _middleName;
        private string _numberPhone;
        private string _address;
        private string _desc;

        public PhoneBookItemPageViewModel(PhoneBook phoneBook = null)
        {
            _ops = new ApiOperations();
            _phoneBook = phoneBook;
            if (PhoneBook != null)
            {
                LastName = PhoneBook.LastName;
                FirstName = PhoneBook.FirstName;
                MiddleName = PhoneBook.MiddleName;
                NumberPhone = PhoneBook.NumberPhone;
                Address = PhoneBook.Address;
                Desc = PhoneBook.Desc;
                VisibilityAddBtn = Visibility.Collapsed;
                if (!Globals.IsAdmin())
                {
                    VisibilityEditBtn = Visibility.Collapsed;
                }
            }
            else
            {
                VisibilityEditBtn = Visibility.Collapsed;
                if (Globals.LoggedInUser == null)
                {
                    VisibilityAddBtn = Visibility.Collapsed;
                }
            }

        }

        public PhoneBook PhoneBook => _phoneBook;

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string MiddleName
        {
            get => _middleName;
            set
            {
                _middleName = value;
                OnPropertyChanged();
            }
        }

        public string NumberPhone
        {
            get => _numberPhone;
            set
            {
                _numberPhone = value;
                OnPropertyChanged();
            }
        }

        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }

        public string Desc
        {
            get => _desc;
            set
            {
                _desc = value;
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
                               PhoneBook newPhoneBook = new PhoneBook
                               {
                                   LastName = LastName,
                                   FirstName = FirstName,
                                   MiddleName = MiddleName,
                                   NumberPhone = NumberPhone,
                                   Address = Address,
                                   Desc = Desc
                               };

                               if (_ops.AddPhoneBook(newPhoneBook))
                               {
                                   var mainWindow = (MainWindow)Application.Current.MainWindow;
                                   mainWindow.MainFrame.NavigationService.Navigate(new PhoneBookIndexPage());
                               }
                           }, obj => LastName != null && NumberPhone != null));
            }
        }

        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                       (editCommand = new RelayCommand(obj =>
                       {
                           PhoneBook newPhoneBook = new PhoneBook
                           {
                               Id = PhoneBook.Id,
                               LastName = LastName,
                               FirstName = FirstName,
                               MiddleName = MiddleName,
                               NumberPhone = NumberPhone,
                               Address = Address,
                               Desc = Desc
                           };

                           if (_ops.EditPhoneBook(newPhoneBook))
                           {
                               var mainWindow = (MainWindow)Application.Current.MainWindow;
                               mainWindow.MainFrame.NavigationService.Navigate(new PhoneBookIndexPage());
                           }
                       }, obj => LastName != null && NumberPhone != null));
            }
        }

        public RelayCommand BackCommand
        {
            get { return backCommand ??
                         (backCommand = new RelayCommand(obj =>
                         {
                             var mainWindow = (MainWindow)Application.Current.MainWindow;
                             mainWindow.MainFrame.NavigationService.GoBack();
                         }));
            }
        }
    }
}
