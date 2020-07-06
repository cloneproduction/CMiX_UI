using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CMiX.MVVM.Interfaces;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class CompositionModel : IComponentModel
    {
        public CompositionModel()
        {
            ComponentModels = new ObservableCollection<IComponentModel>();
            BeatModel = new BeatModel();
            CameraModel = new CameraModel();
            TransitionModel = new SliderModel();
        }


        public BeatModel BeatModel { get; set; }
        public CameraModel CameraModel { get; set; }
        public SliderModel TransitionModel { get; set; }

        public bool Enabled { get; set; }
        public string Name { get; set; }
        public int ID { get; set; }
        public bool IsVisible { get; set; }
        public ObservableCollection<IComponentModel> ComponentModels { get; set; }
    }
}