using CMiX.MVVM.ViewModels;
using CMiX.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Studio.ViewModels
{
    public class Outliner : ViewModel
    {
        public Outliner(ObservableCollection<Project> projects, ComponentManager componentManager)
        {
            Projects = projects;
            ComponentManager = componentManager;
        }

        public ComponentManager ComponentManager { get; set; }

        private ObservableCollection<Project> _projects;
        public ObservableCollection<Project> Projects
        {
            get => _projects;
            set => SetAndNotify(ref _projects, value);
        }

        private IComponent _selectedComponent;
        public IComponent SelectedComponent
        {
            get => _selectedComponent;
            set => SetAndNotify(ref _selectedComponent, value);
        }
    }
}
