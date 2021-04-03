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


        public Component CreateComponent()
        {
            var model = new LayerModel(Guid.NewGuid());
            var messageDispatcher = new MessageDispatcher();
            var component = new Layer(ParentComposition, model, messageDispatcher);
            ID++;
            return component;
        }

        public Component CreateComponent(IComponentModel model)
        {
            var messageDispatcher = new MessageDispatcher();
            return new Layer(ParentComposition, model as LayerModel, messageDispatcher);
        }

    }
}
