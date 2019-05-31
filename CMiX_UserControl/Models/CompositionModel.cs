using CMiX.Services;
using System.Collections.Generic;

namespace CMiX.Models
{
    public class CompositionModel
    {
        public CompositionModel()
        {
            LayerNames = new List<string>();
            LayerIndex = new List<int>();
            MasterBeatModel = new MasterBeatModel();
            LayersModel = new List<LayerModel>();
            CameraModel = new CameraModel();
        }

        public CompositionModel(string messageaddress)
        {
            MessageAddress = "/Layer";
        }

        public string MessageAddress { get; set; }

        [OSC]
        public string Name { get; set; }

        [OSC]
        public List<string> LayerNames { get; set; }

        [OSC]
        public List<int> LayerIndex { get; set; }

        public MasterBeatModel MasterBeatModel { get; set; }
        public CameraModel CameraModel { get; set; }

        public List<LayerModel> LayersModel { get; set; }
    }
}