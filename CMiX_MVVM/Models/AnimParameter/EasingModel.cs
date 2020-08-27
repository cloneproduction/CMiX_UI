using CMiX.MVVM.Resources;
using CMiX.MVVM.ViewModels;


namespace CMiX.MVVM.Models
{
    public class EasingModel : Model
    {
        public EasingModel()
        {

        }

        public Easings.Functions SelectedEasing { get; set; }
        public EasingFunction EasingFunction { get; set; }
        public EasingMode EasingMode { get; set; }
    }
}
