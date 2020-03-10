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

            CreateLayerMaskCommand = new RelayCommand(p => CreateLayerMask(p as IComponent));
            DeleteLayerMaskCommand = new RelayCommand(p => DeleteLayerMask(p as IComponent));
        }

        public ICommand CreateComponentCommand { get; }
        public ICommand DuplicateComponentCommand { get; }
        public ICommand DeleteComponentCommand { get; }
        public ICommand RenameComponentCommand { get; }

        public ICommand CreateLayerMaskCommand { get; }
        public ICommand DeleteLayerMaskCommand { get; }

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

        public void CreateLayerMask(IComponent component)
        {
            if(component is Layer)
                ((Layer)component).AddLayerMask();
        }

        public void DeleteLayerMask(IComponent component)
        {
            if (component is Layer)
                ((Layer)component).DeleteLayerMask();
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
            IComponent result = null;

            if (component is Composition)
                result = DuplicateComposition(component as Composition);
            else if (component is Layer)
                result = DuplicateLayer(component as Layer);
            else if (component is Entity)
                result = DuplicateEntity(component as Entity);
            result = null;

            return result;
        }

       

        public IComponent GetSelectedParent(ObservableCollection<IComponent> components)
        {
            IComponent result = null;

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

        public void MoveToMask(ObservableCollection<IComponent> components)
        {
            foreach (var component in components)
            {
                if(component is Layer)
                {
                    foreach (var item in ((Layer)component).ContentComponents)
                    {
                        if (item.IsSelected)
                        {
                            
                        }
                    }
                }
                else
                {
                    MoveToMask(component.Components);
                }
                //if (component.IsSelected)
                //{
                //    components.Remove(component);
                //    OnComponentDeleted(component);
                //    break;
                //}
                //else
                    
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


        private Composition DuplicateComposition(Composition project)
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


        private Layer DuplicateLayer(Layer layer)
        {
            var parent = GetSelectedParent(Projects);
            Layer newLayer = new Layer(LayerID, parent.Beat, parent.MessageAddress, parent.MessageService, parent.Assets, parent.Mementor);
            newLayer.SetViewModel(layer.GetModel());
            newLayer.Name += " -Copy";
            parent.Components.Insert(parent.Components.IndexOf(layer) + 1, newLayer);
            LayerID++;
            return newLayer;
        }


        int EntityID = 0;
        private Entity CreateEntity(Layer layer)
        {
            var newEntity = new Entity(EntityID, layer.Beat, layer.MessageAddress, layer.MessageService, layer.Assets, layer.Mementor);
            if (layer.MaskChecked)
            {
                layer.MaskComponents.Add(newEntity);
            }
            else
            {
                layer.AddComponent(newEntity);
            }
            EntityID++;
            return newEntity;
        }


        private Entity DuplicateEntity(Entity entity)
        {
            var parent = GetSelectedParent(Projects);
            var component = new Entity(EntityID, parent.Beat, parent.MessageAddress, parent.MessageService, parent.Assets, parent.Mementor);
            component.SetViewModel(entity.GetModel());
            component.Name += " -Copy";
            parent.Components.Insert(parent.Components.IndexOf(entity) + 1, component);
            EntityID++;
            return component;
        }
    }
}
