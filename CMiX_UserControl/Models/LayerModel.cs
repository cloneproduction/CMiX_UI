using CMiX.Services;
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
            maskmodel = new MaskModel();
            ColorationModel = new ColorationModel();
            PostFXModel = new PostFXModel();
            Fade = new SliderModel();
        }

        public LayerModel(string layername) 
            : this()
        {
            MessageAddress = layername;
        }

        public string MessageAddress { get; set; }

        [OSC]
        public string LayerName { get; set; }

        [OSC]
        public bool Enabled { get; set; }

        [OSC]
        public bool Out { get; set; }

        [OSC]
        public int Index { get; set; }

        [OSC]
        public string BlendMode { get; set; }

        public SliderModel Fade { get; set; }
        public BeatModifierModel BeatModifierModel { get; set; }
        public ContentModel ContentModel { get; set; }
        public MaskModel maskmodel { get; set; }
        public ColorationModel ColorationModel { get; set; }
        public PostFXModel PostFXModel{ get; set; }
    }
}