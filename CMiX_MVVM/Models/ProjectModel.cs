using System.Collections.Generic;

namespace CMiX.MVVM.Models
{
    public class ProjectModel : Model
    {
        public ProjectModel()
        {
            CompositionModel = new List<CompositionModel>();
        }

        public List<CompositionModel> CompositionModel { get; set; }
    }
}