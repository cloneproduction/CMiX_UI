// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CMiX.Core.Presentation.ViewModels.Components
{
    public class ComponentManager : ViewModel
    {
        public ComponentManager(ObservableCollection<Component> components)
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


        public ObservableCollection<Component> Components { get; set; }


        private Component _selectedComponent;
        public Component SelectedComponent
        {
            get => _selectedComponent;
            set => SetAndNotify(ref _selectedComponent, value);
        }


        public void RenameComponent(Component component) => SelectedComponent.IsRenaming = true;


        public void CreateComponent(Component component)
        {
            var newComponent = component.ComponentFactory.CreateComponent();
            newComponent.SetCommunicator(component.Communicator);
            //newComponent.SetSender(component.MessageSender);
            component.AddComponent(newComponent);
        }


        public void DeleteComponent(Component component)
        {
            var selectedParent = GetParent(Components);
            selectedParent.RemoveComponent(component);
            component.Dispose();
        }


        public void InsertComponent(int index, Component parentComponent, Component componentToInsert)
        {
            parentComponent.InsertComponent(index, componentToInsert);
        }


        public Component DuplicateComponent(Component component)
        {
            Component result = null;
            // = GetSelectedParent(Components);
            return result;
        }


        public void DeleteSelectedComponent(ObservableCollection<Component> components)
        {
            foreach (Component component in components)
            {
                if (component.IsSelected)
                {
                    components.Remove(component);
                    break;
                }
                else
                    DeleteSelectedComponent(component.Components);
            }
        }



        private Component GetParent(ObservableCollection<Component> components)
        {
            Component result = null;
            foreach (Component component in components)
            {
                if (component.Components.Any(c => c.IsSelected))
                {
                    result = component;
                    break;
                }
                else
                {
                    result = GetParent(component.Components);
                }
            }
            return result;
        }
    }
}