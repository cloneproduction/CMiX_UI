using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class SceneModel : IComponentModel
    {
        public SceneModel()
        {
            EntityModels = new List<EntityModel>();
            SelectedEntityModel = new EntityModel();
            BeatModifierModel = new BeatModifierModel();
            TextureModel = new TextureModel();
            GeometryModel = new GeometryModel();
            PostFXModel = new PostFXModel();
        }

        public bool Enabled { get; set; }

        public List<EntityModel> EntityModels { get; set; }
        public EntityModel SelectedEntityModel { get; set; }

        public EntityEditorModel EntityEditorModel { get; set; }
        public BeatModifierModel BeatModifierModel { get; set; }
        public TextureModel TextureModel { get; set; }
        public GeometryModel GeometryModel { get; set; }
        public PostFXModel PostFXModel { get; set; }

        public int ID { get; set; }
        public string Name { get; set; }
        public string MessageAddress { get; set; }
        public ObservableCollection<IComponentModel> ComponentModels { get; set; }
    }
}