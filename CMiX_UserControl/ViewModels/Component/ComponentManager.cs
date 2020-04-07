using System.Collections.ObjectModel;
using System.Windows.Input;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using CMiX.Studio.ViewModels;
using System.Linq;
using System;

namespace CMiX.ViewModels
{
    public class ComponentManager : ViewModel
    {
        public ComponentManager(ObservableCollection<Component> components)
        {
            Projects = components;

            CreateComponentCommand = new RelayCommand(p => CreateComponent(p as Component));
            DuplicateComponentCommand = new RelayCommand(p => DuplicateComponent(p as Component));
            DeleteComponentCommand = new RelayCommand(p => DeleteComponent());
            RenameComponentCommand = new RelayCommand(p => RenameComponent(p as Component));
        }

        public ICommand CreateComponentCommand { get; }
        public ICommand DuplicateComponentCommand { get; }
        public ICommand DeleteComponentCommand { get; }
        public ICommand RenameComponentCommand { get; }
        public ICommand CreateLayerMaskCommand { get; }
        public ICommand DeleteLayerMaskCommand { get; }
        public ICommand MoveComponentToCommand { get;  }

        public ObservableCollection<Component> Projects { get; set; }

        public event EventHandler<ComponentEventArgs> ComponentDeleted;
        private void OnComponentDeleted(Component deletedComponent)
        {
            ComponentDeleted?.Invoke(this, new ComponentEventArgs(deletedComponent));
        }

        private Component _SelectedComponent;
        public Component SelectedComponent
        {
            get => _SelectedComponent;
            set => SetAndNotify(ref _SelectedComponent, value);
        }

        public void RenameComponent(Component component)
        {
            component.IsRenaming = true;
        }

        public Component CreateComponent(Component component)
        {
            Component result = null;
            
            if (component is Project)
                result = CreateComposition(component as Project);
            else if (component is Composition)
                result = CreateLayer(component as Composition);
            else if (component is Scene)
                result = CreateEntity(component as Scene);
            else if (component is Mask)
                result = CreateEntity(component as Mask);
            else result = null;

            return result;
        }

        public Component DuplicateComponent(Component component)
        {
            Component result = null;

            if (component is Composition)
                result = DuplicateComposition(component as Composition);
            else if (component is Layer)
                result = DuplicateLayer(component as Layer);
            else if (component is Entity)
                result = DuplicateEntity(component as Entity);
            result = null;

            return result;
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

        public void DeleteComponent()
        {
            DeleteSelectedComponent(Projects);
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

        int CompositionID = 0;
        private Composition CreateComposition(Project project)
        {
            var newCompo = new Composition(CompositionID, project.MessageAddress, project.Beat, new MessageService(), project.Mementor);

            project.AddComponent(newCompo);
            CompositionID++;

            return newCompo;
        }


        private Composition DuplicateComposition(Composition project)
        {
            var newCompo = new Composition(CompositionID, project.MessageAddress, project.Beat, new MessageService(), project.Mementor);

            var selectedCompo = SelectedComponent as Composition;
            newCompo.SetViewModel(selectedCompo.GetModel());
            newCompo.ID = CompositionID;
            newCompo.Name += " -Copy";
            project.AddComponent(newCompo);
            CompositionID++;
            
            return newCompo;
        }


        int LayerID = 0;
        private Layer CreateLayer(Composition compo)
        {
            Layer newLayer = new Layer(LayerID, compo.Beat, compo.MessageAddress, compo.MessageService, compo.Mementor);

            newLayer.IsExpanded = true;
            compo.AddComponent(newLayer);
            LayerID++;

            return newLayer;
        }


        private Layer DuplicateLayer(Layer layer)
        {
            var parent = GetSelectedParent(Projects);
            Layer newLayer = new Layer(LayerID, parent.Beat, parent.MessageAddress, parent.MessageService, parent.Mementor);
            newLayer.SetViewModel(layer.GetModel());
            newLayer.Name += " -Copy";
            parent.Components.Insert(parent.Components.IndexOf(layer) + 1, newLayer);
            LayerID++;
            return newLayer;
        }


        int EntityID = 0;
        private Entity CreateEntity(Scene scene)
        {
            var newEntity = new Entity(EntityID, scene.Beat, scene.MessageAddress, scene.MessageService, scene.Mementor);
            scene.Components.Add(newEntity);
            scene.IsExpanded = true;
            EntityID++;
            return newEntity;
        }

        private Entity CreateEntity(Mask mask)
        {
            var newEntity = new Entity(EntityID, mask.Beat, mask.MessageAddress, mask.MessageService, mask.Mementor);
            mask.Components.Add(newEntity);
            mask.IsExpanded = true;
            EntityID++;
            return newEntity;
        }

        private Entity DuplicateEntity(Entity entity)
        {
            var parent = GetSelectedParent(Projects);
            var component = new Entity(EntityID, parent.Beat, parent.MessageAddress, parent.MessageService, parent.Mementor);
            component.SetViewModel(entity.GetModel());
            component.Name += " -Copy";
            parent.Components.Insert(parent.Components.IndexOf(entity) + 1, component);
            EntityID++;
            return component;
        }
    }
}
