using CMiX.Studio.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class ComponentFactory : IComponentFactory
    {
        public ComponentFactory()
        {

        }

        private MessengerManager MessengerManager { get; set; }

        private static int ID = 0;

        public IComponent CreateComponent(IComponent parentComponent)
        {

            if (parentComponent is Project)
                return CreateComposition(parentComponent as Project);
            else if (parentComponent is Composition)
                return CreateLayer(parentComponent as Composition);
            else if (parentComponent is Layer)
                return CreateScene(parentComponent as Layer);
            else if (parentComponent is Scene)
                return CreateEntity(parentComponent as Scene);
            else
                return null;
        }

        private Composition CreateComposition(Project parentComponent)
        {
            var component = new Composition(ID, parentComponent.MessengerManager, new MasterBeat());
            ID++;
            return component;
        }

        private Layer CreateLayer(Composition parentComponent)
        {
            var component = new Layer(ID, parentComponent.MessengerManager, parentComponent.MasterBeat);
            ID++;
            return component;
        }

        private Scene CreateScene(Layer parentComponent)
        {
            var component = new Scene(ID, parentComponent.MessengerManager, parentComponent.MasterBeat);
            ID++;
            return component;
        }

        private Entity CreateEntity(Scene parentComponent)
        {
            var component = new Entity(ID, parentComponent.MessengerManager, parentComponent.MasterBeat);
            ID++;
            return component;
        }
    }
}
