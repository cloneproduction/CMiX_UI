
using CMiX.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Models
{
    public class CompositionDTO
    {
        public CompositionDTO()
        {
            LayerNames = new List<string>();
            MasterBeatDTO = new MasterBeatDTO();
            LayersDTO = new List<LayerDTO>();

            CameraDTO = new CameraDTO();
        }

        public string Name { get; set; }

        public List<string> LayerNames { get; set; }

        public MasterBeatDTO MasterBeatDTO { get; set; }

        public CameraDTO CameraDTO { get; set; }

        public List<LayerDTO> LayersDTO { get; set; }
    }
}