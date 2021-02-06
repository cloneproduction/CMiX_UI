using System;
using System.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace CMiX.MVVM.ViewModels
{
    public class ComponentManager : ViewModel
    {
        public ComponentManager(ObservableCollection<IComponent> components)
        {
            Components = components;

            CreateComponentCommand = new RelayCommand(p => CreateComponent(p as Component));
            DuplicateComponentCommand = new RelayCommand(p => DuplicateComponent(p as Component));
            DeleteComponentCommand = new RelayCommand(p => DeleteComponent(p as Component));
            RenameComponentCommand = new RelayCommand(p => RenameComponent(p as Component));
        }

        public ICommand CreateComponentCommand { get; }
        public ICommand DuplicateComponentCommand { get; }
        public ICommand DeleteComponentCommand { get; }
        public ICommand RenameComponentCommand { get; }

        public ObservableCollection<IComponent> Components { get; set; }

        private Component _selectedComponent;
        public Component SelectedComponent
        {
            get => _selectedComponent;
            set => SetAndNotify(ref _selectedComponent, value);
        }


        public void RenameComponent(Component component) => SelectedComponent.IsRenaming = true;

        public void CreateComponent(Component component) => component.CreateAndAddComponent();


        public Component DuplicateComponent(Component component)
        {
            Component result = null;
            var parent = GetSelectedParent(Components);

            //if (component is Composition)
            //    result = DuplicateComposition(component as Composition);
            //else if (component is Layer)
            //    result = DuplicateLayer(component as Layer);
            //else if (component is Entity)
            //    result = DuplicateEntity(component as Entity);

            return result;
        }


        public void DeleteComponent(Component component)
        {
            var selectedParent = GetSelectedParent(Components);
            selectedParent.RemoveComponent(component);
        }

        public event EventHandler<ComponentEventArgs> ComponentDeletedEvent;

        public void OnComponentDeleted(Component component)
        {
            ComponentDeletedEvent?.Invoke(this, new ComponentEventArgs(component));
        }


        public void DeleteSelectedComponent(ObservableCollection<IComponent> components)
        {
            foreach (Component component in components)
            {
                if (component.IsSelected)
                {
                    components.Remove(component);
                    OnComponentDeleted(component);
                    break;
                }
                else
                    DeleteSelectedComponent(component.Components);
            }
        }



        public Component GetSelectedParent(ObservableCollection<IComponent> components)
        {
            Component result = null;
            foreach (Component component in components)
            {
                if(component.Components.Any(c => c.IsSelected))
                {
                    result = component;
                    break;
                }
                else
                {
                    result = GetSelectedParent(component.Components);
                }
            }
            return result;
        }
    }
}