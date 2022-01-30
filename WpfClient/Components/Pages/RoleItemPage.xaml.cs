using System.Windows.Controls;
using WpfClient.Models;
using WpfClient.ViewModel;

namespace WpfClient.Components.Pages
{
    /// <summary>
    /// Логика взаимодействия для RoleItemPage.xaml
    /// </summary>
    public partial class RoleItemPage : Page
    {
        public RoleItemPage(Role role = null)
        {
            InitializeComponent();
            DataContext = new RoleItemPageViewModel(role);
        }
    }
}
