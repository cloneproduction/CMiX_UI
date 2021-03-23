using System;
using System.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels.MessageService;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels.Components
{
    public class ComponentManager : ViewModel
    {
        public ComponentManager(ObservableCollection<Component> components, MessageTerminal messageTerminal)
        {
            MessageTerminal = messageTerminal;
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

        public MessageTerminal MessageTerminal { get; set; }
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
            component.CreateAndAddComponent();
            MessageTerminal.MessageReceived += component.MessageTerminal_MessageReceived;
        }

        public void DeleteComponent(Component component)
        {
            GetSelectedParent(Components).RemoveComponent(component);
            MessageTerminal.MessageReceived -= component.MessageTerminal_MessageReceived;
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



        public Component GetSelectedParent(ObservableCollection<Component> components)
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