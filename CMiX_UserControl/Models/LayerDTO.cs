using CMiX.Controls;
using CMiX.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        public string MessageAddress { get; set; }

        public string LayerName { get; set; }

        public bool Enabled { get; set; }

        public bool Out { get; set; }

        public int Index { get; set; }

        public double Fade { get; set; }

        public string BlendMode { get; set; }

        public BeatModifierDTO BeatModifierDTO { get; set; }

        public ContentDTO ContentDTO { get; set; }

        public MaskDTO MaskDTO { get; set; }

        public ColorationDTO ColorationDTO { get; set; }

        public LayerFXDTO LayerFXDTO{ get; set; }
    }
}
