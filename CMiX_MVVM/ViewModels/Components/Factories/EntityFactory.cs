using System;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class EntityFactory : IComponentFactory
    {
        public IComponent CreateComponent(int ID, IComponent parentComponent)
        {
            var component = new Entity(ID, parentComponent.MasterBeat);
            parentComponent.AddComponent(component);
            return component;
        }
    }
}