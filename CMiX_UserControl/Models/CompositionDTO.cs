
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
            //MasterBeat = new MasterBeat();
<<<<<<< HEAD
            LayersDTO = new List<LayerDTO>();
=======
            Layers = new List<LayerDTO>();
>>>>>>> bcb820bfbcdfc939e734c5f5b2d362b663e4d49a

            CameraDTO = new CameraDTO();
        }

        public string Name { get; set; }

        public List<string> LayerNames { get; set; }

        //public MasterBeat MasterBeat { get; set; }

        public CameraDTO CameraDTO { get; set; }

<<<<<<< HEAD
        public List<LayerDTO> LayersDTO { get; set; }
=======
        public List<LayerDTO> Layers { get; set; }
>>>>>>> bcb820bfbcdfc939e734c5f5b2d362b663e4d49a
    }
}