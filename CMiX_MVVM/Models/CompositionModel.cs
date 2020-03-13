using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class CompositionModel : IComponentModel
    {
        public CompositionModel()
        {
            ComponentModels = new ObservableCollection<IComponentModel>();
            MasterBeatModel = new MasterBeatModel();
            LayersModel = new List<LayerModel>();
            CameraModel = new CameraModel();
            TransitionModel = new SliderModel();
            SelectedLayer = new LayerModel();
            LayerEditorModel = new LayerEditorModel();
        }

        public List<LayerModel> LayersModel { get; set; }
        public LayerModel SelectedLayer { get; set; }
        public LayerEditorModel LayerEditorModel { get; set; }
        public MasterBeatModel MasterBeatModel { get; set; }
        public CameraModel CameraModel { get; set; }
        public SliderModel TransitionModel { get; set; }

        public string Name { get; set; }
        public int ID { get; set; }
        public string MessageAddress { get; set; }
        public ObservableCollection<IComponentModel> ComponentModels { get; set; }
    }
}