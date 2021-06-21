using CMiX.Core.Mathematics;
using CMiX.Core.Presentation.ViewModels;
using System;

namespace CMiX.Core.Models
{
    public class EasingModel : Model
    {
        public EasingModel()
        {
            this.ID = Guid.NewGuid();
        }

        public bool IsEnabled { get; set; }
        public Easings.Functions SelectedEasing { get; set; }
        public EasingFunction EasingFunction { get; set; }
        public EasingMode EasingMode { get; set; }
    }
}
