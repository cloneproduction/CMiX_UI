using System.Windows;
using System.Windows.Input;
using CMiX.ViewModels;

namespace CMiX
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ProjectView.DataContext = new Project();
        }

        private void UndoCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void UndoCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var proj = ProjectView.DataContext as Project;
            if (proj.SelectedComposition.Mementor.CanUndo)
                proj.SelectedComposition.Mementor.Undo();
        }

        private void RedoCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void RedoCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var proj = ProjectView.DataContext as Project;
            if (proj.SelectedComposition.Mementor.CanRedo)
                proj.SelectedComposition.Mementor.Redo();
        }
    }
}