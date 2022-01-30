using System.Windows.Controls;
using WpfClient.ViewModel;

namespace WpfClient.Components.Pages
{
    /// <summary>
    /// Логика взаимодействия для PhoneBookIndexPage.xaml
    /// </summary>
    public partial class PhoneBookIndexPage : Page
    {
        public PhoneBookIndexPage()
        {
            InitializeComponent();
            DataContext = new PhoneBookIndexPageViewModel();
        }
    }
}
