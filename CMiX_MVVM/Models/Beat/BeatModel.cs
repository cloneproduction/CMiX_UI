using CMiX.MVVM.Interfaces;

namespace CMiX.MVVM.Models
{
    public class BeatModel : IModel
    {
        public BeatModel()
        {

        }

        public double[] Periods { get; set; }
        public double Period { get; set; }
        public double Multiplier { get; set; }
        public bool Enabled { get; set; }
    }
}
