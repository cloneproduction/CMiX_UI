using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class EntityFactory : IComponentFactory
    {
        public EntityFactory(Scene parentScene, IMessageTerminal messageTerminal)
        {
            ParentScene = parentScene;
            MessageTerminal = messageTerminal;
        }


        private static int ID = 0;
        private Scene ParentScene { get; set; }
        private IMessageTerminal MessageTerminal { get; set; }


        public Component CreateComponent()
        {
            EntityModel entityModel = new EntityModel(ID);
            var component = new Entity(MessageTerminal, ParentScene, entityModel);
            ID++;
            return component;
        }

        public Component CreateComponent(IComponentModel model)
        {
            return new Entity(MessageTerminal, ParentScene, model as EntityModel);
        }
    }
}
