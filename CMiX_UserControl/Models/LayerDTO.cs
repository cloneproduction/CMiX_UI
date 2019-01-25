using System;

namespace CMiX.Models
{
    [Serializable]
    public class LayerDTO
    {
        public LayerDTO()
        {
            BeatModifierDTO = new BeatModifierDTO();
            ContentDTO = new ContentDTO();
            MaskDTO = new MaskDTO();
            ColorationDTO = new ColorationDTO();
            LayerFXDTO = new LayerFXDTO();
            Fade = new SliderDTO();
        }

        public string MessageAddress { get; set; }
        public string LayerName { get; set; }
        public bool Enabled { get; set; }
        public bool Out { get; set; }
        public int Index { get; set; }
        public SliderDTO Fade { get; set; }
        public string BlendMode { get; set; }
        public BeatModifierDTO BeatModifierDTO { get; set; }
        public ContentDTO ContentDTO { get; set; }
        public MaskDTO MaskDTO { get; set; }
        public ColorationDTO ColorationDTO { get; set; }
        public LayerFXDTO LayerFXDTO{ get; set; }
    }
}