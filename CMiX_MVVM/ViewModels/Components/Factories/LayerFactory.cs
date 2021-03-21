using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class LayerFactory : IComponentFactory
    {
        public LayerFactory(MessageTerminal MessageTerminal)
        {
            this.MessageTerminal = MessageTerminal;
        }

        private static int ID = 0;

        private MessageTerminal MessageTerminal { get; set; }

        public Component CreateComponent(Component parentComponent)
        {
            return CreateLayer((Composition)parentComponent);
        }

        public Component CreateComponent(Component parentComponent, IComponentModel model)
        {
            return CreateLayer((Composition)parentComponent, model as LayerModel);
        }


        private Layer CreateLayer(Composition composition)
        {
            var component = new Layer(MessageTerminal, composition, new LayerModel(ID));
            ID++;
            return component;
        }


        private Layer CreateLayer(Composition composition, LayerModel componentModel)
        {
            var component = new Layer(MessageTerminal, composition, componentModel);
            component.SetViewModel(componentModel);
            return component;
        }
    }
}
