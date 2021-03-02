using CMiX.MVVM.Interfaces;
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

        public MessageTerminal MessageTerminal { get; set; }

        public Component CreateComponent(Component parentComponent)
        {
            return CreateLayer((Composition)parentComponent);
        }

        public Component CreateComponent(Component parentComponent, IComponentModel model)
        {
            return CreateLayer((Composition)parentComponent, model);
        }


        private Layer CreateLayer(Composition composition)
        {
            var component = new Layer(ID, MessageTerminal, composition);
            ID++;
            return component;
        }


        private Layer CreateLayer(Composition composition, IComponentModel componentModel)
        {
            var component = new Layer(componentModel.ID, MessageTerminal, composition);
            component.SetViewModel(componentModel);
            return component;
        }
    }
}
