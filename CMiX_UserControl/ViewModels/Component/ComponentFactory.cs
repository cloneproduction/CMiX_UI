using CMiX.MVVM.Services;
using Memento;
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

        //public static Component CreateComponent(Component component = null)
        //{
        //    Component newComponent = null;
        //    if (component == null)
        //        newComponent = CreateProject();
        //    else if (component is Project)
        //        newComponent = CreateComposition(component);
        //    return newComponent;
        //}

        public static Project CreateProject()
        {
            CompositionID = 0;
            LayerID = 0;
            EntityID = 0;
            MaskID = 0;
            SceneID = 0;
            var masterBeat = new MasterBeat();
            var newProject = new Project(0, string.Empty, masterBeat, null);
            return newProject;
        }

        public static Composition CreateComposition(Component parentComponent)
        {
            var newCompo = new Composition(CompositionID, parentComponent.Beat);
            CompositionID++;
            return newCompo;
        }

        public static Layer CreateLayer(Component parentComponent)
        {
            Layer newLayer = new Layer(LayerID, parentComponent.Messengers, parentComponent.Beat);
            LayerID++;
            return newLayer;
        }

        public static Scene CreateScene(Component parentComponent)
        {
            Scene newScene = new Scene(SceneID, parentComponent.Beat);
            SceneID++;
            return newScene;
        }

        public static Mask CreateMask(Component parentComponent)
        {
            Mask newMask = new Mask(MaskID, parentComponent.Beat);
            MaskID++;
            return newMask;
        }

        public static Entity CreateEntity(Component parentComponent)
        {
            var newEntity = new Entity(EntityID, parentComponent.Beat);
            EntityID++;
            return newEntity;
        }
    }
}