using CMiX.Services;

namespace CMiX.Models
{
    public class MasterBeatModel : Model
    {

        [OSC]
        public double Period { get; set; }
    }
}
