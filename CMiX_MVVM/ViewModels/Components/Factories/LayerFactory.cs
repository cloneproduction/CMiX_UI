using System;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class LayerFactory : IComponentFactory
    {
        public IComponent CreateComponent(int ID, IComponent parentComponent)
        {
            var component = new Layer(ID, parentComponent.MasterBeat);
            parentComponent.AddComponent(component);
            return component;
        }
    }
}
