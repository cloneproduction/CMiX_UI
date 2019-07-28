using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class LayerModel : Model
    {
        public LayerModel()
        {
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

        public string LayerName { get; set; }
        public int ID { get; set; }
        public bool Enabled { get; set; }
        public bool Out { get; set; }
        public string BlendMode { get; set; }

        public SliderModel Fade { get; set; }
        public ContentModel ContentModel { get; set; }
        public MaskModel maskmodel { get; set; }
        public ColorationModel ColorationModel { get; set; }
        public PostFXModel PostFXModel{ get; set; }
    }
}