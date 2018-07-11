
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
            Layers = new List<LayerDTO>();

            //Camera = new Camera();
        }

        public string Name { get; set; }

        public List<string> LayerNames { get; set; }

        //public MasterBeat MasterBeat { get; set; }

        //public Camera Camera { get; set; }

        public List<LayerDTO> Layers { get; set; }
    }
}