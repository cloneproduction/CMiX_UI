using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class EntityFactory : IComponentFactory
    {
        public EntityFactory(MessageTerminal MessageTerminal)
        {
            this.MessageTerminal = MessageTerminal;
        }

        private static int ID = 0;

        public MessageTerminal MessageTerminal { get; set; }

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
            var component = new Entity(ID, MessageTerminal, parentComponent.MasterBeat);
            ID++;
            return component;
        }

        private Entity CreateEntity(Scene parentComponent, IComponentModel componentModel)
        {
            var component = new Entity(componentModel.ID, MessageTerminal, parentComponent.MasterBeat);
            component.SetViewModel(componentModel);
            return component;
        }
    }
}
