using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;
using System;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class SceneFactory : IComponentFactory
    {
        public SceneFactory(Layer parentLayer)
        {
            ParentLayer = parentLayer;
        }

        private static int ID = 0;
        private Layer ParentLayer { get; set; }


        public Component CreateComponent(IMessageDispatcher messageDispatcher)
        {
            var model = new SceneModel(Guid.NewGuid());
            var component = new Scene(ParentLayer, model, messageDispatcher);
            ID++;
            return component;
        }

        public Component CreateComponent(IMessageDispatcher messageDispatcher, IComponentModel model)
        {
            return new Scene(ParentLayer, model as SceneModel, messageDispatcher);
        }
    }
}