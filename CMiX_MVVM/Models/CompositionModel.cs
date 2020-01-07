using System;
using System.Collections.Generic;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class CompositionModel
    {
        public CompositionModel()
        {
            MasterBeatModel = new MasterBeatModel();
            LayersModel = new List<LayerModel>();
            CameraModel = new CameraModel();
            TransitionModel = new SliderModel();
            SelectedLayer = new LayerModel();
        }

        public string Name { get; set; }

        public string ContentFolderName { get; set; }
        public string GeometryFolderName { get; set; }
        public string VideoFolderName { get; set; }

        public List<LayerModel> LayersModel { get; set; }
        public LayerModel SelectedLayer { get; set; }
        //public List<int> LayerID { get; set; }
        //public List<string> LayerNames { get; set; }
        public MasterBeatModel MasterBeatModel { get; set; }
        public CameraModel CameraModel { get; set; }
        public SliderModel TransitionModel { get; set; }
    }
}