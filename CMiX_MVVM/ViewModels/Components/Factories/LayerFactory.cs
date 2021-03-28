using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
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
            var component = new Layer(ParentComposition, model);
            ID++;
            return component;
        }

        public Component CreateComponent(IComponentModel model)
        {
            return new Layer(ParentComposition, model as LayerModel);
        }

    }
}
