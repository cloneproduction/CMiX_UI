using CMiX.Services;
using System;
using System.Collections.Generic;

namespace CMiX.Models
{
    [Serializable]
    public class CompositionModel : Model
    {
        public CompositionModel()
        {
            LayerNames = new List<string>();
            LayerIndex = new List<int>();
            MasterBeatModel = new MasterBeatModel();
            LayersModel = new List<Model>();
            CameraModel = new CameraModel();
        }

        public CompositionModel(string messageaddress)
        {
            MessageAddress = "/Layer";
        }


        public string Name { get; set; }

        public List<string> LayerNames { get; set; }

        public List<int> LayerIndex { get; set; }

        public List<Model> LayersModel { get; set; }

        public MasterBeatModel MasterBeatModel { get; set; }
        public CameraModel CameraModel { get; set; }
    }
}