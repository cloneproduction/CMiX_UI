using System;

namespace CMiX.MVVM.Models
{
    public class StepperModel : Model, IAnimModeModel
    {
        public StepperModel()
        {
            this.ID = Guid.NewGuid();
        }

        public double Width { get; set; }
        public int StepCount { get; set; }
    }
}
