using System;
namespace CMiX.Models
{
    [Serializable]
    public class ContentModel
    {
        public ContentModel()
        {
            BeatModifierModel = new BeatModifierModel();
            TextureDTO = new TextureModel();
            GeometryModel = new GeometryModel();
            PostFXDTO = new PostFXModel();
        }

        public bool Enable { get; set; }
        public string MessageAddress { get; set; }
        public BeatModifierModel BeatModifierModel { get; set; }
        public TextureModel TextureDTO { get; set; }
        public GeometryModel GeometryModel { get; set; }
        public PostFXModel PostFXDTO { get; set; }
    }
}