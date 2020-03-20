using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CMiX.MVVM.Controls;
using CMiX.MVVM.Services;
using CMiX.Studio.ViewModels;
using CMiX.ViewModels;

namespace CMiX
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            RootView.DataContext = new Root();
        }

        public static readonly DependencyProperty ComponentManagerProperty =
         DependencyProperty.Register("ComponentManager", typeof(ComponentManager),
         typeof(MainWindow), new FrameworkPropertyMetadata());


        private void UndoCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void UndoCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var root = RootView.DataContext as Root;
            if (root.Mementor.CanUndo)
                root.Mementor.Undo();
        }

        private void RedoCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void RedoCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var root = RootView.DataContext as Root;
            if (root.Mementor.CanRedo)
                root.Mementor.Redo();
        }

        private void CloseClick(object sender, RoutedEventArgs e)
     {
         var window = (Window)((FrameworkElement)sender).TemplatedParent;
         window.Close();
     }
    
     private void MaximizeRestoreClick(object sender, RoutedEventArgs e)
     {
       var window = (Window)((FrameworkElement)sender).TemplatedParent;
         if (window.WindowState == System.Windows.WindowState.Normal)
             window.WindowState = System.Windows.WindowState.Maximized;
         else
             window.WindowState = System.Windows.WindowState.Normal;
     }
    
     private void MinimizeClick(object sender, RoutedEventArgs e)
     {
         var window = (Window)((FrameworkElement)sender).TemplatedParent;
         window.WindowState = System.Windows.WindowState.Minimized;
     }
    }
}