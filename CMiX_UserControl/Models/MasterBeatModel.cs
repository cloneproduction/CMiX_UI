using CMiX.Services;

namespace CMiX.Models
{
    public class MasterBeatModel
    {
        public string MessageAddress { get; set; }

        [OSC]
        public double Period { get; set; }
    }
}
