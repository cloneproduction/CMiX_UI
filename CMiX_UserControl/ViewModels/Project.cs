using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GuiLabs.Undo;
using Memento;

namespace CMiX.ViewModels
{
    public class Project : ViewModel
    {
        public Project()
        {
            AddTabCommand = new RelayCommand(p => AddComposition());
            DeleteCompositionCommand = new RelayCommand(p => DeleteComposition(p));
            this.ActionManager = new ActionManager();
            Mementor = new Mementor();
            Compositions = new ObservableCollection<Composition>();
            Compositions.Add(new Composition(this.ActionManager, Mementor) { Name = "This is a super super long name for composition" });
            Compositions.Add(new Composition(this.ActionManager, Mementor) { Name = "VFX TEST" });
            Compositions.Add(new Composition(this.ActionManager, Mementor) { Name = "PROPS"});
        }

        public ICommand AddTabCommand { get; }
        public ICommand DeleteCompositionCommand { get; }

        public ObservableCollection<Composition> Compositions { get; set; }

        private void AddComposition()
        {
            Compositions.Add(new Composition(this.ActionManager, Mementor));
        }

        private void DeleteComposition(object compo)
        {
            Compositions.Remove(compo as Composition);
        }
    }
}