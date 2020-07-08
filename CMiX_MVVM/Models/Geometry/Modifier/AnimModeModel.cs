using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels;
using CMiX.Studio.ViewModels;

namespace CMiX.MVVM.Models
{
    public class AnimModeModel : IModel
    {
        public AnimModeModel()
        {

        }

        public ModeType ModeType { get; set; }
        public bool Enabled { get; set; }
    }
}
