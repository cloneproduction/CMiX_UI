using System.Collections.ObjectModel;
using System.Windows.Input;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using CMiX.Studio.ViewModels;
using System.Linq;
using Memento;
using System;

namespace CMiX.ViewModels
{
    public class ComponentManager : ViewModel
    {
        public ComponentManager(Project project)
        {
            Components = project.Components;
            Mementor = project.Mementor;

            CreateComponentCommand = new RelayCommand(p => CreateComponent(p as Component));
            DuplicateComponentCommand = new RelayCommand(p => DuplicateComponent(p as Component));
            DeleteComponentCommand = new RelayCommand(p => DeleteComponent());
            RenameComponentCommand = new RelayCommand(p => RenameComponent(p as Component));
        }

        public ICommand CreateComponentCommand { get; }
        public ICommand DuplicateComponentCommand { get; }
        public ICommand DeleteComponentCommand { get; }
        public ICommand RenameComponentCommand { get; }

        public Mementor Mementor { get; set; }

        public ObservableCollection<Component> Components { get; set; }

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


        public void CreateComponent(Component component)
        {
            if (component is Composition)
                component.AddComponent(CreateLayer(component));
                
            else if (component is Scene || component is Mask)
                component.AddComponent(CreateEntity(component));

            else if (component is Project)
                component.AddComponent(CreateComposition(component));
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
            result = null;

            return result;
        }


        public void DeleteComponent()
        {
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


        int ProjectID = 0;
        public Project CreateProject()
        {
            var messageService = new MessageService();
            var masterBeat = new MasterBeat(messageService);
            var newProject = new Project(ProjectID, string.Empty, new MasterBeat(messageService), messageService, Mementor, null);
            ProjectID++;
            System.Console.WriteLine("Create Project");
            return newProject;
        }

        int CompositionID = 0;
        private Composition CreateComposition(Component component)
        {
            var newCompo = new Composition(CompositionID, component.MessageAddress, new MasterBeat(null), new MessageService(), component.Mementor);
            CompositionID++;
            return newCompo;
        }

        int LayerID = 0;
        private Layer CreateLayer(Component component)
        {
            Layer newLayer = new Layer(LayerID, component.Beat, component.MessageAddress, component.MessageService, component.Mementor);
            LayerID++;
            return newLayer;
        }

        int EntityID = 0;
        private Entity CreateEntity(Component component)
        {
            var newEntity = new Entity(EntityID, component.Beat, component.MessageAddress, component.MessageService, component.Mementor);
            EntityID++;
            return newEntity;
        }





        private Composition DuplicateComposition(Composition composition)
        {
            var parent = GetSelectedParent(Components);
            var newCompo = new Composition(CompositionID, composition.MessageAddress, composition.Beat, new MessageService(), composition.Mementor);

            var selectedCompo = SelectedComponent as Composition;
            newCompo.SetViewModel(selectedCompo.GetModel());
            newCompo.ID = CompositionID;
            newCompo.Name += " -Copy";
            parent.Components.Insert(parent.Components.IndexOf(composition) + 1, newCompo);
            CompositionID++;
            
            return newCompo;
        }


        private Layer DuplicateLayer(Layer layer)
        {
            var parent = GetSelectedParent(Components);
            Layer newLayer = new Layer(LayerID, parent.Beat, parent.MessageAddress, parent.MessageService, parent.Mementor);
            newLayer.SetViewModel(layer.GetModel());
            newLayer.Name += " -Copy";
            parent.Components.Insert(parent.Components.IndexOf(layer) + 1, newLayer);
            LayerID++;
            return newLayer;
        }


        private Entity DuplicateEntity(Entity entity)
        {
            var parent = GetSelectedParent(Components);
            var component = new Entity(EntityID, parent.Beat, parent.MessageAddress, parent.MessageService, parent.Mementor);
            component.SetViewModel(entity.GetModel());
            component.Name += " -Copy";
            parent.Components.Insert(parent.Components.IndexOf(entity) + 1, component);
            EntityID++;
            return component;
        }
    }
}
