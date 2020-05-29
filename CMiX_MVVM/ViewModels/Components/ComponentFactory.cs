using System;

namespace CMiX.MVVM.ViewModels
{
    public static class ComponentFactory
    {
        private static int CompositionID { get; set; }
        private static int LayerID { get; set; }
        private static int EntityID { get; set; }
        private static int MaskID { get; set; }
        private static int SceneID { get; set; }

        public static Project CreateProject()
        {
            CompositionID = 0;
            LayerID = 0;
            EntityID = 0;
            MaskID = 0;
            SceneID = 0;
            var masterBeat = new MasterBeat();
            var newProject = new Project(0, masterBeat, null);
            return newProject;
        }

        public static Composition CreateComposition(Component parentComponent)
        {
            var component = new Composition(CompositionID, parentComponent.Beat);
            SubscribeToEvent(parentComponent, component);
            CompositionID++;
            return component;
        }

        public static Layer CreateLayer(Component parentComponent)
        {
            var component = new Layer(LayerID, parentComponent.Beat);
            SubscribeToEvent(parentComponent, component);
            LayerID++;
            return component;
        }

        public static Scene CreateScene(Component parentComponent)
        {
            var component = new Scene(SceneID, parentComponent.Beat);
            SubscribeToEvent(parentComponent, component);
            SceneID++;
            return component;
        }

        public static Mask CreateMask(Component parentComponent)
        {
            var component = new Mask(MaskID, parentComponent.Beat);
            SubscribeToEvent(parentComponent, component);
            MaskID++;
            return component;
        }

        public static Entity CreateEntity(Component parentComponent)
        {
            var component = new Entity(EntityID, parentComponent.Beat);
            SubscribeToEvent(parentComponent, component);
            EntityID++;
            return component;
        }

        private static void SubscribeToEvent(Component parentComponent, Component childComponent)
        {
            childComponent.SendChangeEvent += parentComponent.OnChildPropertyToSendChange;
            parentComponent.ReceiveChangeEvent += childComponent.OnParentReceiveChange;
        }
    }
}