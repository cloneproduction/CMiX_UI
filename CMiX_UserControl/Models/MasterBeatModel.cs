using CMiX.Services;
using CMiX.MVVM.Models;

namespace CMiX.Models
{
    public class MasterBeatModel : Model
    {

        [OSC]
        public double Period { get; set; }
    }
}
