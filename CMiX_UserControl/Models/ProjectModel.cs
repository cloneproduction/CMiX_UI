using CMiX.Services;
using CMiX.MVVM.Models;
using System.Collections.Generic;

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
