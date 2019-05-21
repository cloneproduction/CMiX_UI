using CMiX.Services;
using System.Collections.Generic;

namespace CMiX.Models
{
    public class CompositionModel
    {
        public CompositionModel()
        {
            LayerNames = new List<string>();
            MasterBeatModel = new MasterBeatModel();
            LayersModel = new List<LayerModel>();
            CameraModel = new CameraModel();
        }

        [OSC]
        public string Name { get; set; }

        [OSC]
        public List<string> LayerNames { get; set; }

        public MasterBeatModel MasterBeatModel { get; set; }
        public CameraModel CameraModel { get; set; }
        public List<LayerModel> LayersModel { get; set; }
    }
}