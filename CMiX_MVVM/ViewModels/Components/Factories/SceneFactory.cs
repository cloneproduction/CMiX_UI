using System;

namespace CMiX.MVVM.ViewModels.Components.Factories
{
    public class SceneFactory : IComponentFactory
    {
        public IComponent CreateComponent(int ID, IComponent parentComponent)
        {
            var component = new Scene(ID, parentComponent.MasterBeat);
            parentComponent.AddComponent(component);
            return component;
        }
    }
}
