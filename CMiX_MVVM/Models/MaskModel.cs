using System;
using System.Collections.ObjectModel;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class MaskModel : IComponentModel
    {
        public MaskModel()
        {
            ComponentModels = new ObservableCollection<IComponentModel>();
            BeatModifierModel = new BeatModifierModel();
            TextureModel = new TextureModel();
            GeometryModel = new GeometryModel();
            PostFXModel = new PostFXModel();
        }

        public bool Enable { get; set; }
        public bool KeepOriginal { get; set; }
        public string MaskType { get; set; }
        public string MaskControlType { get; set; }

        public BeatModifierModel BeatModifierModel { get; set; }
        public GeometryModel GeometryModel { get; set; }
        public TextureModel TextureModel { get; set; }
        public PostFXModel PostFXModel { get; set; }

        public int ID { get; set; }
        public string Name { get; set; }
        public string MessageAddress { get; set; }
        public bool IsVisible { get; set; }
        public ObservableCollection<IComponentModel> ComponentModels { get; set; }
        public bool Enabled { get; set; }
    }
}