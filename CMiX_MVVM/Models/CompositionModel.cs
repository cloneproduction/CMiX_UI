using System;
using System.Collections.Generic;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class CompositionModel : IModel
    {
        public CompositionModel()
        {
            MasterBeatModel = new MasterBeatModel();
            LayersModel = new List<LayerModel>();
            CameraModel = new CameraModel();
            TransitionModel = new SliderModel();
            SelectedLayer = new LayerModel();
            LayerEditorModel = new LayerEditorModel();
        }

        public string Name { get; set; }

        public List<LayerModel> LayersModel { get; set; }
        public LayerModel SelectedLayer { get; set; }
        public LayerEditorModel LayerEditorModel { get; set; }
        public MasterBeatModel MasterBeatModel { get; set; }
        public CameraModel CameraModel { get; set; }
        public SliderModel TransitionModel { get; set; }
    }
}