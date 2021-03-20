using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class SceneFactory : IComponentFactory
    {
        public SceneFactory(MessageTerminal messageTerminal)
        {
            this.MessageTerminal = messageTerminal;
        }

        private static int ID = 0;

        private MessageTerminal MessageTerminal { get; set; }

        public Component CreateComponent(Component parentComponent)
        {
            return CreateScene((Layer)parentComponent);
        }

        public Component CreateComponent(Component parentComponent, IComponentModel model)
        {
            return CreateScene(parentComponent as Layer, model as SceneModel);
        }

        private Scene CreateScene(Layer parentLayer)
        {
            var component = new Scene(this.MessageTerminal, parentLayer, new SceneModel(ID));
            ID++;
            return component;
        }

        private Scene CreateScene(Layer parentLayer, SceneModel componentModel)
        {
            var component = new Scene(MessageTerminal, parentLayer, componentModel);
            component.SetViewModel(componentModel);
            return component;
        }
    }
}
