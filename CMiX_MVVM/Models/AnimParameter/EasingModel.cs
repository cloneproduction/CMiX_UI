using CMiX.MVVM.ViewModels;


namespace CMiX.MVVM.Models
{
    public class EasingModel : Model
    {
        public EasingModel()
        {

        }

        public EasingFunction EasingFunction { get; set; }
        public EasingMode EasingMode { get; set; }
    }
}
