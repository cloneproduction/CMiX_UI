using CMiX.MVVM.Interfaces;

namespace CMiX.MVVM.Models.Component
{
    public class VisibilityModel : IModel
    {
        public VisibilityModel()
        {

        }

        public bool IsVisible { get; set; }
        public bool Enabled { get; set; }
    }
}
