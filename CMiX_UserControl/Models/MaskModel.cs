using System;

namespace CMiX.Models
{
    [Serializable]
    public class MaskModel
    {
        public MaskModel()
        {
            BeatModifierModel = new BeatModifierModel();
            TextureDTO = new TextureModel();
            GeometryModel = new GeometryModel();
            PostFXDTO = new PostFXModel();
        }

        public string MessageAddress { get; set; }
        public bool Enable { get; set; }
        public bool KeepOriginal { get; set; }
        public BeatModifierModel BeatModifierModel { get; set; }
        public GeometryModel GeometryModel { get; set; }
        public TextureModel TextureDTO { get; set; }
        public PostFXModel PostFXDTO { get; set; }
    }
}