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
            ComponentManager = new ComponentManager();
            RootView.DataContext = new Root();
        }

        public static readonly DependencyProperty ComponentManagerProperty =
         DependencyProperty.Register("ComponentManager", typeof(ComponentManager),
         typeof(MainWindow), new FrameworkPropertyMetadata());

        public ComponentManager ComponentManager
        {
            get { return (ComponentManager)GetValue(ComponentManagerProperty); }
            set { SetValue(ComponentManagerProperty, value); }
        }


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
    }
}