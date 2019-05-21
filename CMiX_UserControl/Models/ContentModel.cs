using CMiX.Services;
using System;
namespace CMiX.Models
{
    [Serializable]
    public class ContentModel
    {
        public ContentModel()
        {
            BeatModifierModel = new BeatModifierModel();
            texturemodel = new TextureModel();
            GeometryModel = new GeometryModel();
            PostFXModel = new PostFXModel();
        }
        public string MessageAddress { get; set; }

        [OSC]
        public bool Enable { get; set; }
        
        public BeatModifierModel BeatModifierModel { get; set; }
        public TextureModel texturemodel { get; set; }
        public GeometryModel GeometryModel { get; set; }
        public PostFXModel PostFXModel { get; set; }
    }
}