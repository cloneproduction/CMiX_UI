using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class LayerFactory : IComponentFactory
    {
        public LayerFactory(Composition parentComposition, IMessageTerminal messageTerminal)
        {
            ParentComposition = parentComposition;
            MessageTerminal = messageTerminal;
        }


        private static int ID = 0;
        public Composition ParentComposition { get; set; }
        public IMessageTerminal MessageTerminal { get; set; }


        public Component CreateComponent()
        {
            var model = new LayerModel(ID);
            var component = new Layer(MessageTerminal, ParentComposition, model);
            ID++;
            return component;
        }

        public Component CreateComponent(IComponentModel model)
        {
            return new Layer(MessageTerminal, ParentComposition, model as LayerModel);
        }

    }
}
