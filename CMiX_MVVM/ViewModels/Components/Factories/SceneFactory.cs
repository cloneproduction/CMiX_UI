using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;

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
            var component = new Scene(ParentLayer, new SceneModel(ID));
            ID++;
            return component;
        }

        public Component CreateComponent(IComponentModel model)
        {
            return new Scene(ParentLayer, model as SceneModel);
        }
    }
}