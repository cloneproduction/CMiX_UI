using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using System;
using CMiX.MVVM.ViewModels;

namespace CMiX.ViewModels
{
    public class ComponentManager : ViewModel
    {
        public ComponentManager(Project project)
        {
            Components = project.Components;

            CreateComponentCommand = new RelayCommand(p => CreateComponent(p as Component));
            DuplicateComponentCommand = new RelayCommand(p => DuplicateComponent(p as Component));
            DeleteComponentCommand = new RelayCommand(p => DeleteComponent());
            RenameComponentCommand = new RelayCommand(p => RenameComponent(p as Component));
        }

        public ICommand CreateComponentCommand { get; }
        public ICommand DuplicateComponentCommand { get; }
        public ICommand DeleteComponentCommand { get; }
        public ICommand RenameComponentCommand { get; }

        public ObservableCollection<Component> Components { get; set; }


        public void RenameComponent(Component component)
        {
            component.IsRenaming = true;
        }


        public void CreateComponent(Component component)
        {
            ComponentFactory.CreateComponent(component);
            component.OnSendChange(component.GetModel(), component.GetMessageAddress());
        }


        public Component DuplicateComponent(Component component)
        {
            Component result = null;
            var parent = GetSelectedParent(Components);

            if (component is Composition)
                result = DuplicateComposition(component as Composition);
            else if (component is Layer)
                result = DuplicateLayer(component as Layer);
            else if (component is Entity)
                result = DuplicateEntity(component as Entity);

            return result;
        }


        public void DeleteComponent()
        {
            var selectedParent = GetSelectedParent(Components);
            if(selectedParent != null)
                selectedParent.OnSendChange(selectedParent.GetModel(), selectedParent.GetMessageAddress());
            DeleteSelectedComponent(Components);
        }

        public event EventHandler<ComponentEventArgs> ComponentDeletedEvent;

        public void OnComponentDeleted(Component component)
        {
            ComponentDeletedEvent?.Invoke(this, new ComponentEventArgs(component));
        }


        public void DeleteSelectedComponent(ObservableCollection<Component> components)
        {
            foreach (var component in components)
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



        public Component GetSelectedParent(ObservableCollection<Component> components)
        {
            Component result = null;
            foreach (var component in components)
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


        private Composition DuplicateComposition(Composition composition)
        {
            var parent = GetSelectedParent(Components);
            var newCompo = ComponentFactory.CreateComposition(parent);

            newCompo.SetViewModel(composition.GetModel());
            newCompo.Name += " -Copy";
            parent.Components.Insert(parent.Components.IndexOf(composition) + 1, newCompo);
            
            return newCompo;
        }


        private Layer DuplicateLayer(Layer layer)
        {
            var parent = GetSelectedParent(Components);
            Layer newLayer = ComponentFactory.CreateLayer(parent);
            newLayer.SetViewModel(layer.GetModel());
            newLayer.Name += " -Copy";
            parent.Components.Insert(parent.Components.IndexOf(layer) + 1, newLayer);
            return newLayer;
        }


        private Entity DuplicateEntity(Entity entity)
        {
            var parent = GetSelectedParent(Components);
            var component = ComponentFactory.CreateEntity(parent);
            component.SetViewModel(entity.GetModel());
            component.Name += " -Copy";
            parent.Components.Insert(parent.Components.IndexOf(entity) + 1, component);
            return component;
        }
    }
}
