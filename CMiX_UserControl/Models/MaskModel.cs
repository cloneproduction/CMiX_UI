using CMiX.Services;
using System;

namespace CMiX.Models
{
    [Serializable]
    public class MaskModel : Model
    {
        public MaskModel()
        {
            BeatModifierModel = new BeatModifierModel();
            texturemodel = new TextureModel();
            GeometryModel = new GeometryModel();
            PostFXModel = new PostFXModel();
        }

        [OSC]
        public bool Enable { get; set; }

        [OSC]
        public bool KeepOriginal { get; set; }

        public BeatModifierModel BeatModifierModel { get; set; }
        public GeometryModel GeometryModel { get; set; }
        public TextureModel texturemodel { get; set; }
        public PostFXModel PostFXModel { get; set; }
    }
}