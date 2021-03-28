using CMiX.MVVM.Interfaces;

namespace CMiX.MVVM.Models
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
