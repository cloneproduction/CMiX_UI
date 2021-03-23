using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class SceneFactory : IComponentFactory
    {
        public SceneFactory()
        {
        }

        private static int ID = 0;

        public Component CreateComponent(Component parentComponent, MessageDispatcher messageDispatcher)
        {
            return CreateScene((Layer)parentComponent, messageDispatcher);
        }

        public Component CreateComponent(Component parentComponent, MessageDispatcher messageDispatcher, IComponentModel model)
        {
            return CreateScene(parentComponent as Layer, messageDispatcher, model as SceneModel);
        }

        private Scene CreateScene(Layer parentLayer, MessageDispatcher messageDispatcher)
        {
            var component = new Scene(messageDispatcher, parentLayer, new SceneModel(ID));
            ID++;
            return component;
        }

        private Scene CreateScene(Layer parentLayer, MessageDispatcher messageDispatcher, SceneModel componentModel)
        {
            return new Scene(messageDispatcher, parentLayer, componentModel);
        }
    }
}
