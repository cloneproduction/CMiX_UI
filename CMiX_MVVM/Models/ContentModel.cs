using System;
using System.Collections.Generic;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class ContentModel : Model
    {
        public ContentModel()
        {
            ObjectModels = new List<Model>();
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

        public bool Enable { get; set; }

        public List<Model> ObjectModels { get; set; }
        public BeatModifierModel BeatModifierModel { get; set; }
        public TextureModel TextureModel { get; set; }
        public GeometryModel GeometryModel { get; set; }
        public PostFXModel PostFXModel { get; set; }
    }
}