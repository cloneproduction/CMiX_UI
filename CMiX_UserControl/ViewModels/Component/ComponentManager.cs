using System.Collections.ObjectModel;
using System.Windows.Input;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using CMiX.Studio.ViewModels;
using System.Linq;

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

        public ObservableCollection<IComponent> Projects{ get; set; }

        public ICommand CreateComponentCommand { get; }
        public ICommand DuplicateComponentCommand { get; }
        public ICommand DeleteComponentCommand { get; }
        public ICommand RenameComponentCommand { get; }

        private IComponent _SelectedComponent;
        public IComponent SelectedComponent
        {
            get => _SelectedComponent;
            set => SetAndNotify(ref _SelectedComponent, value);
        }

        public void RenameComponent(IComponent component)
        {
            System.Console.WriteLine("RenameComponent");
            component.IsRenaming = true;
        }

        public IComponent CreateComponent(IComponent component)
        {
            System.Console.WriteLine("CreateComponent");
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
            System.Console.WriteLine("DeleteComponent");
            DeleteSelectedComponent(Projects);
        }

        public void DeleteSelectedComponent(ObservableCollection<IComponent> components)
        {
            //IComponent toDelete = null;
            foreach (var component in components)
            {
                if (component.IsSelected)
                {
                    components.Remove(component);
                    break;
                }
                    
                else
                    DeleteSelectedComponent(component.Components);
            }

                //if (components.Any(x => x.IsSelected))
                //{
                //    var toDelete = components.Where(x => x.IsSelected).First();
                //    components.Remove(toDelete);
                //}
                //else
                //{
                //    foreach (var component in components)
                //    {

                //    }
                //}
                //    DeleteSelectedComponent(component.Components);
        }


        //public IComponent FindSelectedComponent(ObservableCollection<IComponent> components)
        //{
        //    IComponent item = null;
        //    foreach (var component in components)
        //    {
        //        if (component.IsSelected)
        //            item = component;
        //        else
        //            FindSelectedComponent(component.Components);
        //    }
        //    return item;
        //}

        //public IComponent FindSelectedParent(ObservableCollection<IComponent> components)
        //{
        //    IComponent parent = null;
        //    foreach (var component in components)
        //    {
        //        if (component.Components.Any(x => x.IsSelected))
        //        {
        //            parent = component;
        //            break;
        //        }
        //    }
        //    return parent;
        //}


        int CompositionID = 0;
        private Composition CreateComposition(Project project)
        {
            System.Console.WriteLine("CreateComposition");
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
            System.Console.WriteLine("CreateLayer");
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
            System.Console.WriteLine("CreateEntity");
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
