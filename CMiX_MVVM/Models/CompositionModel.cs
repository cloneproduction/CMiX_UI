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
            BeatModel = new BeatModel();
            LayersModel = new List<LayerModel>();
            CameraModel = new CameraModel();
            TransitionModel = new SliderModel();
            SelectedLayer = new LayerModel();
            LayerEditorModel = new LayerEditorModel();
        }

        public List<LayerModel> LayersModel { get; set; }
        public LayerModel SelectedLayer { get; set; }
        public LayerEditorModel LayerEditorModel { get; set; }
        public BeatModel BeatModel { get; set; }
        public CameraModel CameraModel { get; set; }
        public SliderModel TransitionModel { get; set; }

        public string Name { get; set; }
        public int ID { get; set; }
        public bool IsVisible { get; set; }
        public string MessageAddress { get; set; }
        public ObservableCollection<IComponentModel> ComponentModels { get; set; }
        public bool Enabled { get; set; }
    }
}