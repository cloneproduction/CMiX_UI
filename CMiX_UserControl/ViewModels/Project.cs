using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CMiX.Models;
using Memento;

namespace CMiX.ViewModels
{
    public class Project : ViewModel
    {
        public Project() : base(new ObservableCollection<Services.OSCMessenger>())
        {
            AddCompositionCommand = new RelayCommand(p => AddComposition());
            AddTabCommand = new RelayCommand(p => AddComposition());
            DeleteCompositionCommand = new RelayCommand(p => DeleteComposition(p));
            DuplicateCompositionCommand = new RelayCommand(p => DuplicateComposition(p));
            Compositions = new ObservableCollection<Composition>();
        }

        #region PROPERTIES
        public ICommand AddCompositionCommand { get; }
        public ICommand DeleteCompositionCommand { get; }
        public ICommand DuplicateCompositionCommand { get; }
        public ICommand AddTabCommand { get; }

        public ObservableCollection<Composition> Compositions { get; set; }
        #endregion

        #region ADD/DELETE/DUPLICATE COMPOSITION
        private void AddComposition()
        {
            Composition comp = new Composition(Mementor = new Mementor());
            Compositions.Add(comp);
        }

        private void DeleteComposition(object compo)
        {
            Composition comp = compo as Composition;
            comp.Mementor.Dispose();
            Compositions.Remove(comp);
        }

        private void DuplicateComposition(object compo)
        {
            Composition comp = compo as Composition;
            CompositionDTO compDTO = new CompositionDTO();
            comp.Copy(compDTO);
            Composition newcomp = new Composition(new Mementor());
            newcomp.Paste(compDTO);
            Compositions.Add(newcomp);
        }
        #endregion
    }
}