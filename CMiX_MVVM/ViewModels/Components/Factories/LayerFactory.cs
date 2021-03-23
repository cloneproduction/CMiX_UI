using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class LayerFactory : IComponentFactory
    {
        public LayerFactory()
        {

        }

        private static int ID = 0;

        public Component CreateComponent(Component parentComponent, MessageDispatcher messageDispatcher)
        {
            return CreateLayer((Composition)parentComponent, messageDispatcher);
        }

        public Component CreateComponent(Component parentComponent, MessageDispatcher messageDispatcher, IComponentModel model)
        {
            return CreateLayer((Composition)parentComponent, messageDispatcher, model as LayerModel);
        }


        private Layer CreateLayer(Composition composition, MessageDispatcher messageDispatcher)
        {
            var model = new LayerModel(ID);
            var component = new Layer(messageDispatcher, composition, model);
            ID++;
            return component;
        }


        private Layer CreateLayer(Composition composition, MessageDispatcher messageDispatcher, LayerModel componentModel)
        {
            return new Layer(messageDispatcher, composition, componentModel);
        }
    }
}
