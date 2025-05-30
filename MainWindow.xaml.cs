using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;

namespace Kinematics_Calculator_2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenDrawer_Click(object sender, RoutedEventArgs e)
        {
            MyDrawerHost.IsLeftDrawerOpen = true;
        }

        private void ToggleDrawer_Click(object sender, RoutedEventArgs e)
        {
            MyDrawerHost.IsLeftDrawerOpen = !MyDrawerHost.IsLeftDrawerOpen;
        }

        private void CloseDrawer_Click(object sender, RoutedEventArgs e)
        {
            MyDrawerHost.IsLeftDrawerOpen = false;
        }
    }
}