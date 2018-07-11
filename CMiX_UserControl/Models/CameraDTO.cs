using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Models
{
    public class CameraDTO
    {
        public CameraDTO()
        {
            BeatModifierDTO = new BeatModifierDTO();
        }

        public string Rotation { get; set; }
        public string LookAt { get; set; }
        public string View { get; set; }

        public BeatModifierDTO BeatModifierDTO { get; set; }

        public double FOV { get; set; }
        public double Zoom { get; set; }
    }
}
