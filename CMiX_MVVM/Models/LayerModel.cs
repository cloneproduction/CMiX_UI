using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class LayerModel
    {
        public LayerModel()
        {
            BlendMode = new BlendModeModel();
            MaskModel = new MaskModel();
            PostFXModel = new PostFXModel();
            Fade = new SliderModel();
            ContentModel = new ContentModel();
            EntityEditorModel = new EntityEditorModel();
        }

        public bool Enabled { get; set; }
        public string Name { get; set; }
        public int ID { get; set; }
        public bool Out { get; set; }

        public BlendModeModel BlendMode { get; set; }
        public SliderModel Fade { get; set; }
        public ContentModel ContentModel { get; set; }
        public MaskModel MaskModel { get; set; }
        public PostFXModel PostFXModel{ get; set; }
        public EntityEditorModel EntityEditorModel { get; set; }
    }
}