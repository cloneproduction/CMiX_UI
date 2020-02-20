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
        public ComponentManager(ObservableCollection<IComponent> projects)
        {
            Projects = projects;

            CreateComponentCommand = new RelayCommand(p => CreateComponent(p as IComponent));
            DuplicateComponentCommand = new RelayCommand(p => DuplicateComponent(p as IComponent));
            DeleteComponentCommand = new RelayCommand(p => DeleteComponent());
            RenameComponentCommand = new RelayCommand(p => RenameComponent(p as IComponent));
        }

        public ICommand CreateComponentCommand { get; }
        public ICommand DuplicateComponentCommand { get; }
        public ICommand DeleteComponentCommand { get; }
        public ICommand RenameComponentCommand { get; }

        public ObservableCollection<IComponent> Projects { get; set; }

        public event EventHandler<ComponentEventArgs> ComponentDeleted;
        private void OnComponentDeleted(IComponent deletedComponent)
        {
            if (ComponentDeleted != null)
                ComponentDeleted(this, new ComponentEventArgs(deletedComponent));
        }

        private IComponent _SelectedComponent;
        public IComponent SelectedComponent
        {
            get => _SelectedComponent;
            set => SetAndNotify(ref _SelectedComponent, value);
        }

        public void RenameComponent(IComponent component)
        {
            component.IsRenaming = true;
        }

        public IComponent CreateComponent(IComponent component)
        {
            IComponent result = null;
            
            if (component is Project)
                result = CreateComposition(component as Project);
            else if (component is Composition)
                result = CreateLayer(component as Composition);
            else if (component is Layer)
                result = CreateEntity(component as Layer);
            else result = null;

            return result;
        }

        public IComponent DuplicateComponent(IComponent component)
        {
            IComponent result;

            if (component is Project)
                result = DuplicateComposition(component as Project);
            else if (component is Composition)
                result = DuplicateLayer(component as Composition);
            else if (component is Layer)
                result = DuplicateEntity(component as Layer);
            result = null;

            return result;
        }


        public void DeleteComponent()
        {
            DeleteSelectedComponent(Projects);
        }


        public void DeleteSelectedComponent(ObservableCollection<IComponent> components)
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
            var newCompo = new Composition(CompositionID, project.MessageAddress, project.Beat, new MessageService(), project.Assets, project.Mementor);
            project.AddComponent(newCompo);
            CompositionID++;
            return newCompo;
        }


        private Composition DuplicateComposition(Project project)
        {
            var newCompo = new Composition(CompositionID, project.MessageAddress, project.Beat, new MessageService(), project.Assets, project.Mementor);
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
            Layer newLayer = new Layer(LayerID, compo.Beat, compo.MessageAddress, compo.MessageService, compo.Assets, compo.Mementor);
            compo.AddComponent(newLayer);
            LayerID++;
            return newLayer;
        }


        private Layer DuplicateLayer(Composition compo)
        {
            Layer newLayer = new Layer(LayerID, compo.Beat, compo.MessageAddress, compo.MessageService, compo.Assets, compo.Mementor);
            var selectedLayer = SelectedComponent as Layer;
            newLayer.SetViewModel(selectedLayer.GetModel());
            newLayer.Name += " -Copy";
            compo.AddComponent(newLayer);
            LayerID++;
            return newLayer;
        }


        int EntityID = 0;
        private Entity CreateEntity(Layer layer)
        {
            var newEntity = new Entity(EntityID, layer.Beat, layer.MessageAddress, layer.MessageService, layer.Assets, layer.Mementor);
            layer.AddComponent(newEntity);
            EntityID++;
            return newEntity;
        }


        private Entity DuplicateEntity(Layer layer)
        {
            var newEntity = new Entity(EntityID, layer.Beat, layer.MessageAddress, layer.MessageService, layer.Assets, layer.Mementor);
            var selectedEntity = SelectedComponent as Entity;
            newEntity.SetViewModel(selectedEntity.GetModel());
            newEntity.Name += " -Copy";
            layer.AddComponent(newEntity);
            EntityID++;
            return newEntity;
        }
    }
}
