using CMiX.Services;

namespace CMiX.Models
{
    public class CameraModel : Model
    {
        public CameraModel()
        {
            BeatModifierModel = new BeatModifierModel();
            FOV = new SliderModel();
            Zoom = new SliderModel();
        }

        [OSC]
        public string Rotation { get; set; }

        [OSC]
        public string LookAt { get; set; }

        [OSC]
        public string View { get; set; }

        public BeatModifierModel BeatModifierModel { get; set; }
        public SliderModel FOV { get; set; }
        public SliderModel Zoom { get; set; }
    }
}