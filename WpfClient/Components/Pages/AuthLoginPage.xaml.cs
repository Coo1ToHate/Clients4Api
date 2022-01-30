using System.Windows.Controls;
using WpfClient.ViewModel;

namespace WpfClient.Components.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthLoginPage.xaml
    /// </summary>
    public partial class AuthLoginPage : Page
    {

        public AuthLoginPage()
        {
            InitializeComponent();
            DataContext = new AuthLoginPageViewModel();
        }
    }
}
