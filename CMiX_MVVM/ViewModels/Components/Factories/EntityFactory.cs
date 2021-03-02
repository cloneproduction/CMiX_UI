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

        public Component CreateComponent(Component parentComponent)
        {
            return (parentComponent is Scene) ? CreateEntity((Scene)parentComponent) : null;
        }

        public Component CreateComponent(Component parentComponent, IComponentModel model)
        {
            return (parentComponent is Scene) ? CreateEntity((Scene)parentComponent, model) : null;
        }

        private Entity CreateEntity(Scene parentScene)
        {
            var component = new Entity(ID, MessageTerminal, parentScene);
            ID++;
            return component;
        }

        private Entity CreateEntity(Scene parentScene, IComponentModel componentModel)
        {
            var component = new Entity(componentModel.ID, MessageTerminal, parentScene);
            component.SetViewModel(componentModel);
            return component;
        }
    }
}
