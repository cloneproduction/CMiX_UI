using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class EntityModel
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
    }
}