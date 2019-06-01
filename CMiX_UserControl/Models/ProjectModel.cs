using CMiX.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Models
{
    public class ProjectModel : Model
    {
        public ProjectModel()
        {
            CompositionModel = new List<CompositionModel>();
        }

        [OSC]
        public List<CompositionModel> CompositionModel { get; set; }
    }
}
