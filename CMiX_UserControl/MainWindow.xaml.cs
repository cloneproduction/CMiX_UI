using System.Windows;
using SharpOSC;
using System;
using System.Windows.Input;
using CMiX.ViewModels;
using Memento;
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
            Project proj = ProjectView.DataContext as Project;
            if(proj.Compositions.Count > 0)
            {
                if (proj.Compositions[proj.SelectedComposition].Mementor.CanUndo)
                {
                    proj.Compositions[proj.SelectedComposition].Mementor.Undo();
                }
            }
        }
    }
}