using System;

namespace CMiX.MVVM.ViewModels
{
    public static class ComponentFactory
    {
        private static int _id = 0;
        public static int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public static Component CreateComponent(Component component = null)
        {
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

        public static Project CreateProject()
        {
            
            var newProject = new Project(ID, null);
            ID++;
            return newProject;
        }

        public static Composition CreateComposition(Component parentComponent)
        {
            var component = new Composition(ID);
            parentComponent.AddComponent(component);
            SubscribeToEvent(parentComponent, component);
            ID++;
            return component;
        }

        public static Layer CreateLayer(Composition parentComponent)
        {
            var component = new Layer(ID, parentComponent.MasterBeat);
            parentComponent.AddComponent(component);
            SubscribeToEvent(parentComponent, component);
            ID++;
            return component;
        }

        public static Scene CreateScene(Layer parentComponent)
        {
            var component = new Scene(ID, parentComponent.MasterBeat);
            parentComponent.AddComponent(component);
            SubscribeToEvent(parentComponent, component);
            ID++;
            return component;
        }


        public static Entity CreateEntity(Scene parentComponent)
        {
            var component = new Entity(ID, parentComponent.MasterBeat);
            parentComponent.AddComponent(component);
            SubscribeToEvent(parentComponent, component);
            ID++;
            return component;
        }

        private static void SubscribeToEvent(Component parentComponent, Component childComponent)
        {
            childComponent.SendChangeEvent += parentComponent.OnChildPropertyToSendChange;
            parentComponent.ReceiveChangeEvent += childComponent.OnParentReceiveChange;
        }
    }
}