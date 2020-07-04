using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CMiX.MVVM.ViewModels;

namespace CMiX.MVVM.ViewModels
{
    public class ComponentEditor : ViewModel
    {
        public ComponentEditor(Component component)
        {
            //Components = project.ComponentsInEditing;
            SelectedComponent = component;
            EditComponentCommand = new RelayCommand(p => EditComponent(p as Component));
            RemoveComponentCommand = new RelayCommand(p => RemoveComponentFromEditing(p as Component));
        }


        public void ComponentDeletedEvent(object sender, ComponentEventArgs e)
        {
            DeleteComponentFromEditing(e.Component);
        }

        public ICommand EditComponentCommand { get; set; }
        public ICommand RemoveComponentCommand { get; set; }


        private ObservableCollection<Component> _components;
        public ObservableCollection<Component> Components
        {
            get => _components;
            set => SetAndNotify(ref _components, value);
        }

        private Component _selectedComponent;
        public Component SelectedComponent
        {
            get => _selectedComponent;
            set
            {
                SetAndNotify(ref _selectedComponent, value);
                if(SelectedComponent != null)
                    System.Console.WriteLine(SelectedComponent.GetType()); 
            }
        }

        public void RemoveComponentFromEditing(Component component)
        {
            Components.Remove(component);
        }

        public void EditComponent(Component component)
        {
            if (!Components.Contains(component))
                Components.Insert(0, component);
            else
                Components.Move(Components.IndexOf(component), 0);

            SelectedComponent = component;
        }

        public void DeleteComponentFromEditing(Component component)
        {
            Components.Remove(component);
            foreach (var item in component.Components)
            {
                Components.Remove(item);
                DeleteComponentFromEditing(item);
            }
        }
    }
}
