using System.Windows.Controls;
using WpfClient.ViewModel;

namespace WpfClient.Components.Pages
{
    /// <summary>
    /// Логика взаимодействия для RoleIndexPage.xaml
    /// </summary>
    public partial class RoleIndexPage : Page
    {
        public RoleIndexPage()
        {
            InitializeComponent();
            DataContext = new RoleIndexPageViewModel();
        }
    }
}
