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

        public static void CreateComponent(Component component)
        {
            if (component is Project)
                CreateComposition(component);
            else if (component is Composition)
                CreateLayer(component);
            else if (component is Layer)
                CreateScene(component);
            else if (component is Scene)
                CreateEntity(component);
        }

        public static Project CreateProject()
        {
            var masterBeat = new MasterBeat();
            var newProject = new Project(ID, masterBeat, null);
            ID++;
            return newProject;
        }

        public static Composition CreateComposition(Component parentComponent)
        {
            var component = new Composition(ID, parentComponent.Beat);
            parentComponent.AddComponent(component);
            SubscribeToEvent(parentComponent, component);
            ID++;
            return component;
        }

        public static Layer CreateLayer(Component parentComponent)
        {
            var component = new Layer(ID, parentComponent.Beat);
            parentComponent.AddComponent(component);
            SubscribeToEvent(parentComponent, component);
            ID++;
            return component;
        }

        public static Scene CreateScene(Component parentComponent)
        {
            var component = new Scene(ID, parentComponent.Beat);
            parentComponent.AddComponent(component);
            SubscribeToEvent(parentComponent, component);
            ID++;
            return component;
        }

        public static Mask CreateMask(Component parentComponent)
        {
            var component = new Mask(ID, parentComponent.Beat);
            parentComponent.AddComponent(component);
            SubscribeToEvent(parentComponent, component);
            ID++;
            return component;
        }

        public static Entity CreateEntity(Component parentComponent)
        {
            var component = new Entity(ID, parentComponent.Beat);
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