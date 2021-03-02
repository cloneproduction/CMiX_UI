using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class SceneFactory : IComponentFactory
    {
        public SceneFactory(MessageTerminal MessageTerminal)
        {
            this.MessageTerminal = MessageTerminal;
        }

        private static int ID = 0;

        public MessageTerminal MessageTerminal { get; set; }

        public Component CreateComponent(Component parentComponent)
        {
            return CreateScene((Layer)parentComponent);
        }

        public Component CreateComponent(Component parentComponent, IComponentModel model)
        {
            return CreateScene((Layer)parentComponent, model);
        }

        private Scene CreateScene(Layer parentLayer)
        {
            var component = new Scene(ID, MessageTerminal, parentLayer);
            ID++;
            return component;
        }


        private Scene CreateScene(Layer parentLayer, IComponentModel componentModel)
        {
            var component = new Scene(componentModel.ID, MessageTerminal, parentLayer);
            component.SetViewModel(componentModel);
            return component;
        }
    }
}
