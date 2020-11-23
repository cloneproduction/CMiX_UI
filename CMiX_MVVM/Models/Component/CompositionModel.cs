using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models.Beat;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class CompositionModel : IComponentModel
    {
        public CompositionModel()
        {
            ComponentModels = new ObservableCollection<IComponentModel>();
            MasterBeatModel = new MasterBeatModel();
            CameraModel = new CameraModel();
            TransitionModel = new SliderModel();
        }


        public MasterBeatModel MasterBeatModel { get; set; }
        public CameraModel CameraModel { get; set; }
        public SliderModel TransitionModel { get; set; }

        public bool Enabled { get; set; }
        public string Name { get; set; }
        public int ID { get; set; }
        public bool IsVisible { get; set; }
        public string Address { get; set; }
        public ObservableCollection<IComponentModel> ComponentModels { get; set; }
    }
}