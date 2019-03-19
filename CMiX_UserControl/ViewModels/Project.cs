using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Memento;

namespace CMiX.ViewModels
{
    public class Project : ViewModel
    {
        public Project() : base(new ObservableCollection<Services.OSCMessenger>())
        {
            AddTabCommand = new RelayCommand(p => AddComposition());
            DeleteCompositionCommand = new RelayCommand(p => DeleteComposition(p));
            UndoCommand = new RelayCommand(p => Undo());
            RedoCommand = new RelayCommand(p => Redo());
            Compositions = new ObservableCollection<Composition>();
        }

        public ICommand AddTabCommand { get; }
        public ICommand DeleteCompositionCommand { get; }
        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }

        public ObservableCollection<Composition> Compositions { get; set; }

        private void AddComposition()
        {
            Composition comp = new Composition(Mementor = new Mementor());
            Compositions.Add(comp);
            Mementor.ElementAdd(Compositions, comp);
            Console.WriteLine("AddComposition");
        }

        private void DeleteComposition(object compo)
        {
            var comp = compo as Composition;
            Mementor.ElementRemove(Compositions, comp);
            Compositions.Remove(comp);
            Console.WriteLine("DeleteComposition");
        }

        #region UNDO/REDO
        void Undo()
        {
            if (Mementor.CanUndo)
                Mementor.Undo();
        }

        void Redo()
        {
            if (Mementor.CanRedo)
                Mementor.Redo();
        }
        #endregion
    }
}