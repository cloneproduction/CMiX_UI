using CMiX.Core.Presentation.ViewModels;
using System;

namespace CMiX.Core.Models
{
    public class SteadyModel : Model, IAnimModeModel
    {
        public SteadyModel()
        {
            this.ID = Guid.NewGuid();
        }

        public SteadyType SteadyType { get; set; }
        public LinearType LinearType { get; set; }
        public int Seed { get; set; }
    }
}
