using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class SceneFactory : IComponentFactory
    {
        public SceneFactory(Layer parentLayer, IMessageTerminal messageTerminal)
        {
            ParentLayer = parentLayer;
            MessageTerminal = messageTerminal;
        }


        private static int ID = 0;
        private Layer ParentLayer { get; set; }
        private IMessageTerminal MessageTerminal { get; set; }


        public Component CreateComponent()
        {
            var component = new Scene(MessageTerminal, ParentLayer, new SceneModel(ID));
            ID++;
            return component;
        }

        public Component CreateComponent(IComponentModel model)
        {
            return new Scene(MessageTerminal, ParentLayer, model as SceneModel);
        }
    }
}