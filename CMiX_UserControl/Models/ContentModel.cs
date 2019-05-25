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
            TextureModel = new TextureModel();
            GeometryModel = new GeometryModel();
            PostFXModel = new PostFXModel();
        }

        public ContentModel(string messageaddress) 
            : this()
        {
            MessageAddress = messageaddress;
            TextureModel = new TextureModel(messageaddress);
        }

        public string MessageAddress { get; set; }

        [OSC]
        public bool Enable { get; set; }
        
        public BeatModifierModel BeatModifierModel { get; set; }
        public TextureModel TextureModel { get; set; }
        public GeometryModel GeometryModel { get; set; }
        public PostFXModel PostFXModel { get; set; }
    }
}