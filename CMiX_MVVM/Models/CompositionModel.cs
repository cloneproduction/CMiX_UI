//using CMiX.Services;
using CMiX.MVVM.Models;
using System;
using System.Collections.Generic;

namespace CMiX.MVVM.Models
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

        public string ContentFolderName { get; set; }
        public string GeometryFolderName { get; set; }
        public string VideoFolderName { get; set; }

        //[OSC]
        public List<string> LayerNames { get; set; }

        //[OSC]
        public List<int> LayerIndex { get; set; }

        public List<Model> LayersModel { get; set; }

        public MasterBeatModel MasterBeatModel { get; set; }
        public CameraModel CameraModel { get; set; }
    }
}