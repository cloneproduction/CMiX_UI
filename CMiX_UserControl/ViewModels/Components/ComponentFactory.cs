using System;

namespace CMiX.Studio.ViewModels
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
            component.SendChangeEvent += parentComponent.OnChildPropertyToSendChange;
            parentComponent.ReceiveChangeEvent += component.OnParentReceiveChange;
            CompositionID++;
            return component;
        }

        public static Layer CreateLayer(Component parentComponent)
        {
            var component = new Layer(LayerID, parentComponent.Beat);
            component.SendChangeEvent += parentComponent.OnChildPropertyToSendChange;
            LayerID++;
            return component;
        }

        public static Scene CreateScene(Component parentComponent)
        {
            var component = new Scene(SceneID, parentComponent.Beat);
            component.SendChangeEvent += parentComponent.OnChildPropertyToSendChange;
            SceneID++;
            return component;
        }

        public static Mask CreateMask(Component parentComponent)
        {
            var component = new Mask(MaskID, parentComponent.Beat);
            component.SendChangeEvent += parentComponent.OnChildPropertyToSendChange;
            MaskID++;
            return component;
        }

        public static Entity CreateEntity(Component parentComponent)
        {
            var component = new Entity(EntityID, parentComponent.Beat);
            component.SendChangeEvent += parentComponent.OnChildPropertyToSendChange;
            EntityID++;
            return component;
        }
    }
}