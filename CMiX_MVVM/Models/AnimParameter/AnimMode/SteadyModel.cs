using CMiX.MVVM.ViewModels;

namespace CMiX.MVVM.Models
{
    public class SteadyModel : Model, IAnimModeModel
    {
        public SteadyModel()
        {

        }

        public SteadyType SteadyType { get; set; }
        public LinearType LinearType { get; set; }
        public int Seed { get; set; }
    }
}
