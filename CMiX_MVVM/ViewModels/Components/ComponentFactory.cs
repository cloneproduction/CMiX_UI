using CMiX.Studio.ViewModels.MessageService;
using System;

namespace CMiX.MVVM.ViewModels
{
    public static class ComponentFactory
    {
        private static int ID = 0;

        public static Component CreateComponent(Component component = null)
        {
            ID++;
            if (component == null)
                return CreateProject();
            else if (component is Project)
                return CreateComposition(component);
            else if (component is Composition)
                return CreateLayer(component as Composition);
            else if (component is Layer)
                return CreateScene(component as Layer);
            else if (component is Scene)
                return CreateEntity(component as Scene);
            else
                return null;
        }

        private static Project CreateProject()
        {
            return new Project(ID, null);
        }

        private static Composition CreateComposition(Component parentComponent)
        {
            var component = new Composition(ID);
            parentComponent.AddComponent(component);
            return component;
        }

        private static Layer CreateLayer(Composition parentComponent)
        {
            var component = new Layer(ID, parentComponent.MasterBeat);
            parentComponent.AddComponent(component);
            return component;
        }

        private static Scene CreateScene(Layer parentComponent)
        {
            var component = new Scene(ID, parentComponent.MasterBeat);
            parentComponent.AddComponent(component);
            return component;
        }

        private static Entity CreateEntity(Scene parentComponent)
        {
            var component = new Entity(ID, parentComponent.MasterBeat);
            parentComponent.AddComponent(component);
            return component;
        }
    }
}