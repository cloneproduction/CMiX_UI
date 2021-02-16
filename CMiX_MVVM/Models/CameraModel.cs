using CMiX.MVVM.Interfaces;

namespace CMiX.MVVM.Models
{
    public class CameraModel : IModel
    {
        public CameraModel()
        {
            BeatModifierModel = new BeatModifierModel();
            FOV = new SliderModel();
            Zoom = new SliderModel();
        }

        public string Rotation { get; set; }
        public string LookAt { get; set; }
        public string View { get; set; }

        public BeatModifierModel BeatModifierModel { get; set; }
        public SliderModel FOV { get; set; }
        public SliderModel Zoom { get; set; }
        public bool Enabled { get; set; }
    }
}