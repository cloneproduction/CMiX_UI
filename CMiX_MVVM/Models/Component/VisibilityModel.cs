using CMiX.MVVM.Interfaces;

namespace CMiX.MVVM.Models.Component
{
    public class VisibilityModel : Model, IModel
    {
        public VisibilityModel()
        {

        }

        public bool IsVisible { get; set; }
    }
}
