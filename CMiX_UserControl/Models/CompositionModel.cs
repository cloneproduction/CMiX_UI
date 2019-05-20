using System.Collections.Generic;

namespace CMiX.Models
{
    public class CompositionModel
    {
        public CompositionModel()
        {
            LayerNames = new List<string>();
            MasterBeatDTO = new MasterBeatModel();
            LayersDTO = new List<LayerModel>();
            CameraModel = new CameraModel();
        }

        public string Name { get; set; }
        public List<string> LayerNames { get; set; }
        public MasterBeatModel MasterBeatDTO { get; set; }
        public CameraModel CameraModel { get; set; }
        public List<LayerModel> LayersDTO { get; set; }
    }
}