using System.Windows.Controls;
using WpfClient.ViewModel;

namespace WpfClient.Components.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserIndexPage.xaml
    /// </summary>
    public partial class UserIndexPage : Page
    {
        public UserIndexPage()
        {
            InitializeComponent();
            DataContext = new UserIndexPageViewModel();
        }
    }
}
