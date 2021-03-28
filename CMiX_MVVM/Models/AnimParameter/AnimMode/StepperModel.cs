namespace CMiX.MVVM.Models
{
    public class StepperModel : Model, IAnimModeModel
    {
        public StepperModel()
        {

        }

        public double Width { get; set; }
        public int StepCount { get; set; }
    }
}
