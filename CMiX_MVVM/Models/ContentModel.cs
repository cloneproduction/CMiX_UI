//using CMiX.Services;
using CMiX.MVVM.Models;
using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class ContentModel : Model
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
            MessageAddress = String.Format("{0}{1}/", messageaddress, "Content");
            BeatModifierModel = new BeatModifierModel(messageaddress);
            TextureModel = new TextureModel(messageaddress);
            GeometryModel = new GeometryModel(messageaddress);
        }

        //[OSC]
        public bool Enable { get; set; }
        
        public BeatModifierModel BeatModifierModel { get; set; }
        public TextureModel TextureModel { get; set; }
        public GeometryModel GeometryModel { get; set; }
        public PostFXModel PostFXModel { get; set; }
    }
}