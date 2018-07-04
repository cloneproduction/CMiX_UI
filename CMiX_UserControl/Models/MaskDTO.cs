
namespace CMiX.Models
{
    public class MaskDTO
    {
        public MaskDTO()
        {
            BeatModifierDTO = new BeatModifierDTO();
        }

        public bool Enable { get; set; }

        public BeatModifierDTO BeatModifierDTO { get; set; }
    }
}