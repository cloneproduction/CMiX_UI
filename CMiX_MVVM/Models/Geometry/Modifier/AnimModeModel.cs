using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels;

namespace CMiX.MVVM.Models
{
    public class AnimModeModel : IModel
    {
        public AnimModeModel()
        {

        }

        public AnimModeEnum Mode { get; set; }
        public bool Enabled { get; set; }
    }
}
