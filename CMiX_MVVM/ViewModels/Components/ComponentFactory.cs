using System;

namespace CMiX.MVVM.ViewModels
{
    public static class ComponentFactory
    {
        private static int ID { get; set; }
        //private static int CompositionID { get; set; }
        //private static int LayerID { get; set; }
        //private static int EntityID { get; set; }
        //private static int MaskID { get; set; }
        //private static int SceneID { get; set; }

        public static Project CreateProject()
        {
            ID = 0;
            //CompositionID = 0;
            //LayerID = 0;
            //EntityID = 0;
            //MaskID = 0;
            //SceneID = 0;
            var masterBeat = new MasterBeat();
            var newProject = new Project(0, masterBeat, null);
            return newProject;
        }

        public static Composition CreateComposition(Component parentComponent)
        {
            var component = new Composition(ID, parentComponent.Beat);
            SubscribeToEvent(parentComponent, component);
            ID++;
            return component;
        }

        public static Layer CreateLayer(Component parentComponent)
        {
            var component = new Layer(ID, parentComponent.Beat);
            SubscribeToEvent(parentComponent, component);
            ID++;
            return component;
        }

        public static Scene CreateScene(Component parentComponent)
        {
            Console.WriteLine("CreateScene");
            var component = new Scene(ID, parentComponent.Beat);
            SubscribeToEvent(parentComponent, component);
            ID++;
            return component;
        }

        public static Mask CreateMask(Component parentComponent)
        {
            var component = new Mask(ID, parentComponent.Beat);
            SubscribeToEvent(parentComponent, component);
            ID++;
            return component;
        }

        public static Entity CreateEntity(Component parentComponent)
        {
            var component = new Entity(ID, parentComponent.Beat);
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