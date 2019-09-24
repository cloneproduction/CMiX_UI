using System;
using System.Windows;
using System.Windows.Controls;
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
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
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

        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("Window_PreviewMouseDown");
            WindowMainGrid.Focus();
            
        }
    }
}