using System.Windows.Controls;
using WpfClient.ViewModel;

namespace WpfClient.Components.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthRegisterPage.xaml
    /// </summary>
    public partial class AuthRegisterPage : Page
    {
        public AuthRegisterPage()
        {
            InitializeComponent();
            DataContext = new AuthRegisterPageViewModel();
        }
    }
}
