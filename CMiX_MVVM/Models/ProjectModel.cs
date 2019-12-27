using System.Collections.Generic;

namespace CMiX.MVVM.Models
{
    public class ProjectModel : IModel
    {
        public ProjectModel()
        {
            CompositionModel = new List<CompositionModel>();
        }

        public List<CompositionModel> CompositionModel { get; set; }
        public string MessageAddress { get; set; }
    }
}