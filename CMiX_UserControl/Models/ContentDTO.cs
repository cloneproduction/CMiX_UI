using System;
namespace CMiX.Models
{
    [Serializable]
    public class ContentDTO
    {
        public ContentDTO()
        {
            BeatModifierDTO = new BeatModifierDTO();
            TextureDTO = new TextureDTO();
            GeometryDTO = new GeometryDTO();
            PostFXDTO = new PostFXDTO();
        }

        public bool Enable { get; set; }

        public string MessageAddress { get; set; }

        public BeatModifierDTO BeatModifierDTO { get; set; }

        public TextureDTO TextureDTO { get; set; }

        public GeometryDTO GeometryDTO { get; set; }

        public PostFXDTO PostFXDTO { get; set; }
    }
}