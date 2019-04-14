using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Models
{
    public class ProjectDTO
    {
        public ProjectDTO()
        {
            CompositionDTO = new List<CompositionDTO>();
        }
        public List<CompositionDTO> CompositionDTO { get; set; }
    }
}
