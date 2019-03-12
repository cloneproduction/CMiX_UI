using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CMiX.ViewModels
{
    public class Project : ViewModel
    {
        public Project()
        {
            AddTabCommand = new RelayCommand(p => AddComposition());
            DeleteCompositionCommand = new RelayCommand(p => DeleteComposition(p));

            Compositions = new ObservableCollection<Composition>();
            Compositions.Add(new Composition { Name = "This is a super super long name for composition" });
            Compositions.Add(new Composition { Name = "VFX TEST" });
            Compositions.Add(new Composition{Name = "PROPS"});
        }

        private void AddComposition()
        {
            Compositions.Add(new Composition { Name = "Comp1" });
        }

        private void DeleteComposition(object compo)
        {
            Compositions.Remove(compo as Composition);
        }

        public ICommand AddTabCommand { get; }
        public ICommand DeleteCompositionCommand { get; }

        public ObservableCollection<Composition> Compositions { get; set; }
    }
}
