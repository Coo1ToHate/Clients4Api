using System.Windows.Controls;
using WpfClient.Models;
using WpfClient.ViewModel;

namespace WpfClient.Components.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserItemPage.xaml
    /// </summary>
    public partial class UserItemPage : Page
    {
        public UserItemPage(User user = null)
        {
            InitializeComponent();
            DataContext = new UserItemPageViewModel(user);
        }
    }
}
