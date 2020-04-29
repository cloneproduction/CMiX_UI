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

        public static Project CreateProject(MessengerService messengerService, Mementor mementor)
        {
            CompositionID = 0;
            LayerID = 0;
            EntityID = 0;
            MaskID = 0;
            SceneID = 0;
            var masterBeat = new MasterBeat(messengerService);
            var newProject = new Project(0, string.Empty, masterBeat, new Mementor(), null);
            return newProject;
        }

        public static Composition CreateComposition(Component parentComponent)
        {
            var newCompo = new Composition(CompositionID, parentComponent.Beat, parentComponent.Mementor);
            CompositionID++;
            return newCompo;
        }

        public static Layer CreateLayer(Component parentComponent)
        {
            Layer newLayer = new Layer(LayerID, parentComponent.Beat, parentComponent.Mementor);
            LayerID++;
            return newLayer;
        }

        public static Scene CreateScene(Component parentComponent)
        {
            Scene newScene = new Scene(SceneID, parentComponent.Beat, parentComponent.Mementor);
            SceneID++;
            return newScene;
        }

        public static Mask CreateMask(Component parentComponent)
        {
            Mask newMask = new Mask(MaskID, parentComponent.Beat, parentComponent.Mementor);
            MaskID++;
            return newMask;
        }

        public static Entity CreateEntity(Component parentComponent)
        {
            var newEntity = new Entity(EntityID, parentComponent.Beat, parentComponent.Mementor);
            EntityID++;
            return newEntity;
        }
    }
}