using CMiX.Core.Interfaces;

namespace CMiX.Core.Models
{
    public class BeatModel : Model, IModel
    {
        public BeatModel()
        {

        }

        public double[] Periods { get; set; }
        public double Period { get; set; }
        public double Multiplier { get; set; }
    }
}
