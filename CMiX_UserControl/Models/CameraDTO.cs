namespace CMiX.Models
{
    public class CameraDTO
    {
        public CameraDTO()
        {
            BeatModifierDTO = new BeatModifierDTO();
            FOV = new SliderDTO();
            Zoom = new SliderDTO();
        }

        public string Rotation { get; set; }
        public string LookAt { get; set; }
        public string View { get; set; }
        public BeatModifierDTO BeatModifierDTO { get; set; }
        public SliderDTO FOV { get; set; }
        public SliderDTO Zoom { get; set; }
    }
}