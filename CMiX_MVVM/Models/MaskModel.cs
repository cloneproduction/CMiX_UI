using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class MaskModel
    {
        public MaskModel()
        {
            BeatModifierModel = new BeatModifierModel();
            TextureModel = new TextureModel();
            GeometryModel = new GeometryModel();
            PostFXModel = new PostFXModel();
        }

        public bool Enable { get; set; }
        public bool KeepOriginal { get; set; }
        public string MaskType { get; set; }
        public string MaskControlType { get; set; }

        public BeatModifierModel BeatModifierModel { get; set; }
        public GeometryModel GeometryModel { get; set; }
        public TextureModel TextureModel { get; set; }
        public PostFXModel PostFXModel { get; set; }
    }
}