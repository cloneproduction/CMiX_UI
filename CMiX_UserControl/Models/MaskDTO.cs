using System;

namespace CMiX.Models
{
    [Serializable]
    public class MaskDTO
    {
        public MaskDTO()
        {
            BeatModifierDTO = new BeatModifierDTO();
            TextureDTO = new TextureDTO();
            GeometryDTO = new GeometryDTO();
            PostFXDTO = new PostFXDTO();
        }

        public string MessageAddress { get; set; }
        public bool Enable { get; set; }
        public bool KeepOriginal { get; set; }
        public BeatModifierDTO BeatModifierDTO { get; set; }
        public GeometryDTO GeometryDTO { get; set; }
        public TextureDTO TextureDTO { get; set; }
        public PostFXDTO PostFXDTO { get; set; }
    }
}