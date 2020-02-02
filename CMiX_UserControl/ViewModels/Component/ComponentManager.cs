using System.Windows.Input;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using CMiX.Studio.ViewModels;

namespace CMiX.ViewModels
{
    public class ComponentManager : ViewModel
    {
        public ComponentManager()
        {
            CreateComponentCommand = new RelayCommand(p => CreateComponent(p as IComponent));
            DuplicateComponentCommand = new RelayCommand(p => DuplicateComponent(p as IComponent));
        }

        public ICommand CreateComponentCommand { get; }
        public ICommand DuplicateComponentCommand { get; }

        private IComponent _SelectedComponent;
        public IComponent SelectedComponent
        {
            get => _SelectedComponent;
            set
            {
                SetAndNotify(ref _SelectedComponent, value);
                System.Console.WriteLine("SelectedComponent Changed");
            }
        }


        public IComponent CreateComponent(IComponent component)
        {
            IComponent result;

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
        private  Entity CreateEntity(Layer layer)
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

        //public T CreateInstance<T>(params object[] constructorArguments) where T : class, IComponent
        //{
        //    return (T)Activator.CreateInstance(typeof(T), constructorArguments);
        //}
    }
}
