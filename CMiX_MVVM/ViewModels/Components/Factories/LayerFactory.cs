using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels.MessageService;
using System;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class LayerFactory : IComponentFactory
    {
        public LayerFactory(Composition parentComposition)
        {
            ParentComposition = parentComposition;
        }

        private static int ID = 0;
        public Composition ParentComposition { get; set; }


        public Component CreateComponent(IMessageDispatcher messageDispatcher)
        {
            var model = new LayerModel(Guid.NewGuid());
            var component = new Layer(ParentComposition, model);
            component.SetMessageCommunication(messageDispatcher);
            ID++;
            return component;
        }

        public Component CreateComponent(IMessageDispatcher messageDispatcher, IComponentModel model)
        {
            var component = new Layer(ParentComposition, model as LayerModel);
            component.SetMessageCommunication(messageDispatcher);
            return component;
        }
    }
}
