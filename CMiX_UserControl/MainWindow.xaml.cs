using System;
using System.Windows;
using System.Windows.Input;
using CMiX.MVVM.Services;
using CMiX.ViewModels;
using Memento;

namespace CMiX
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Mementor mementor = new Mementor();
            MessageService messageService = new MessageService();
            ProjectView.DataContext = new Project(messageService, mementor);
        }

        private void UndoCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void UndoCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var proj = ProjectView.DataContext as Project;
            if (proj.Mementor.CanUndo)
                proj.Mementor.Undo();
        }

        private void RedoCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void RedoCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var proj = ProjectView.DataContext as Project;
            if (proj.Mementor.CanRedo)
                proj.Mementor.Redo();
        }
    }
}