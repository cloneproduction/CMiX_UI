using System;
using System.Collections.ObjectModel;
using System.Windows;
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
            AddLayerCommand = new RelayCommand(p => AddLayer());
            Compositions = new ObservableCollection<Composition>();
        }

        private Composition _selectedcomposition;
        public Composition SelectedComposition
        {
            get { return _selectedcomposition; }
            set { _selectedcomposition = value; }
        }

        #region PROPERTIES
        public ICommand AddCompositionCommand { get; }
        public ICommand DeleteCompositionCommand { get; }
        public ICommand DuplicateCompositionCommand { get; }
        public ICommand AddTabCommand { get; }
        public ICommand AddLayerCommand { get; }

        public ObservableCollection<Composition> Compositions { get; set; }
        #endregion

        #region ADD/DELETE/DUPLICATE COMPOSITION
        private void AddComposition()
        {
            Composition comp = new Composition();
            Compositions.Add(comp);
            SelectedComposition = comp;
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
            Composition newcomp = new Composition();
            newcomp.Paste(compDTO);
            Compositions.Add(newcomp);
        }
        #endregion


        private void AddLayer()
        {
            SelectedComposition.AddLayer();
        }
    }
}