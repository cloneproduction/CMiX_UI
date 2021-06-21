using System.Windows;
using CMiXPlayer.ViewModels;

namespace CMiXPlayer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ProjectView.DataContext = new Project();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
    }
}