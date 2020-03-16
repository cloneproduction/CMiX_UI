using System;
using System.Collections.ObjectModel;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class EntityModel : IComponentModel
    {
        public EntityModel()
        {
            BeatModifierModel = new BeatModifierModel();
            GeometryModel = new GeometryModel();
            TextureModel = new TextureModel();
            ColorationModel = new ColorationModel();
        }

        public bool Enabled { get; set; }
        public string Name { get; set; }
        public int ID { get; set; }

        public BeatModifierModel BeatModifierModel { get; set; }
        public GeometryModel GeometryModel { get; set; }
        public TextureModel TextureModel { get; set; }
        public ColorationModel ColorationModel { get; set; }

        public string MessageAddress { get; set; }
        public bool IsVisible { get; set; }

        public ObservableCollection<IComponentModel> ComponentModels { get; set; }
    }
}