using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models.Beat;
using CMiX.MVVM.Models.Component;
using System;
using System.Collections.ObjectModel;

namespace CMiX.MVVM.Models
{
    public class CompositionModel : IComponentModel
    {
        public CompositionModel()
        {

        }

        public CompositionModel(Guid id)
        {
            ID = id;
            ComponentModels = new ObservableCollection<IComponentModel>();
            MasterBeatModel = new MasterBeatModel();
            CameraModel = new CameraModel();
            TransitionModel = new SliderModel();
            VisibilityModel = new VisibilityModel();
        }

        public VisibilityModel VisibilityModel { get; set; }
        public MasterBeatModel MasterBeatModel { get; set; }
        public CameraModel CameraModel { get; set; }
        public SliderModel TransitionModel { get; set; }

        public bool Enabled { get; set; }
        public string Name { get; set; }
        public Guid ID { get; set; }
        public bool IsVisible { get; set; }
        public string Address { get; set; }
        public ObservableCollection<IComponentModel> ComponentModels { get; set; }
    }
}