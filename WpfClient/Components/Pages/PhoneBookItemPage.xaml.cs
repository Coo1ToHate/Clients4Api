using System.Windows.Controls;
using WpfClient.Models;
using WpfClient.ViewModel;

namespace WpfClient.Components.Pages
{
    /// <summary>
    /// Логика взаимодействия для PhoneBookItemPage.xaml
    /// </summary>
    public partial class PhoneBookItemPage : Page
    {
        public PhoneBookItemPage(PhoneBook phoneBook = null)
        {
            InitializeComponent();
            DataContext = new PhoneBookItemPageViewModel(phoneBook);
        }
    }
}
