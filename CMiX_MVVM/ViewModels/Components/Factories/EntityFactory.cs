using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class EntityFactory : IComponentFactory
    {
        public EntityFactory(MessengerTerminal messengerTerminal)
        {
            this.MessengerTerminal = messengerTerminal;
        }

        private static int ID = 0;

        public MessengerTerminal MessengerTerminal { get; set; }

        public IComponent CreateComponent(IComponent parentComponent)
        {
            return (parentComponent is Scene) ? CreateEntity((Scene)parentComponent) : null;
        }

        public IComponent CreateComponent(IComponent parentComponent, IComponentModel model)
        {
            return (parentComponent is Scene) ? CreateEntity((Scene)parentComponent, model) : null;
        }

        private Entity CreateEntity(Scene parentComponent)
        {
            var component = new Entity(ID, MessengerTerminal, parentComponent.MasterBeat);
            ID++;
            return component;
        }

        private Entity CreateEntity(Scene parentComponent, IComponentModel componentModel)
        {
            var component = new Entity(componentModel.ID, MessengerTerminal, parentComponent.MasterBeat);
            component.SetViewModel(componentModel);
            return component;
        }
    }
}
