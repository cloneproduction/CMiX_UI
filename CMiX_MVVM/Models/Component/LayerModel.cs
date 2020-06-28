using CMiX.MVVM.Interfaces;
using System;
using System.Collections.ObjectModel;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class LayerModel : IComponentModel
    {
        public LayerModel()
        {
            BlendMode = new BlendModeModel();
            PostFXModel = new PostFXModel();
            Fade = new SliderModel();
            ComponentModels = new ObservableCollection<IComponentModel>();
        }

        public bool Enabled { get; set; }
        public string Name { get; set; }
        public int ID { get; set; }
        public bool Out { get; set; }
        public string MessageAddress { get; set; }
        public bool IsVisible { get; set; }

        public BlendModeModel BlendMode { get; set; }
        public SliderModel Fade { get; set; }
        public SceneModel ContentModel { get; set; }
        public MaskModel MaskModel { get; set; }
        public PostFXModel PostFXModel{ get; set; }
        public ObservableCollection<IComponentModel> ComponentModels { get; set; }
    }
}