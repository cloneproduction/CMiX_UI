using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
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


        public Component CreateComponent()
        {
            var model = new SceneModel(Guid.NewGuid());
            var component = new Scene(ParentLayer, model);
            ID++;
            return component;
        }

        public Component CreateComponent(IComponentModel model)
        {
            return new Scene(ParentLayer, model as SceneModel);
        }
    }
}