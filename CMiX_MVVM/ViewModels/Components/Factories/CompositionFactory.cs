using System;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class CompositionFactory : IComponentFactory
    {
        public IComponent CreateComponent(int ID, IComponent parentComponent)
        {
            var component = new Composition(ID);
            parentComponent.AddComponent(component);
            return component;
        }
    }
}
