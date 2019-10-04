using System;
using System.Collections.Generic;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class CompositionModel : Model
    {
        public CompositionModel()
        {
            MasterBeatModel = new MasterBeatModel();
            LayersModel = new List<Model>();
            CameraModel = new CameraModel();
            TransitionModel = new SliderModel();
            SelectedLayer = new LayerModel();
        }

        public CompositionModel(string messageaddress)
        {
            MessageAddress = "/Layer";
        }

        public string Name { get; set; }
        public int layerID { get; set; }
        public int layerNameID { get; set; }

        public string ContentFolderName { get; set; }
        public string GeometryFolderName { get; set; }
        public string VideoFolderName { get; set; }

        public List<Model> LayersModel { get; set; }
        public LayerModel SelectedLayer { get; set; }
        public List<int> LayerID { get; set; }
        public List<string> LayerNames { get; set; }
        public MasterBeatModel MasterBeatModel { get; set; }
        public CameraModel CameraModel { get; set; }
        public SliderModel TransitionModel { get; set; }
    }
}