using System;

namespace CMiX.Models
{
    [Serializable]
    public class LayerModel
    {
        public LayerModel()
        {
            BeatModifierModel = new BeatModifierModel();
            ContentModel = new ContentModel();
            MaskDTO = new MaskModel();
            ColorationModel = new ColorationModel();
            PostFXDTO = new PostFXModel();
            Fade = new SliderModel();
        }

        public string MessageAddress { get; set; }
        public string LayerName { get; set; }
        public bool Enabled { get; set; }
        public bool Out { get; set; }
        public int Index { get; set; }
        public string BlendMode { get; set; }

        public SliderModel Fade { get; set; }
        public BeatModifierModel BeatModifierModel { get; set; }
        public ContentModel ContentModel { get; set; }
        public MaskModel MaskDTO { get; set; }
        public ColorationModel ColorationModel { get; set; }
        public PostFXModel PostFXDTO{ get; set; }
    }
}