namespace CMiX.MVVM.Models
{
    public class RangeModel : Model, IRangeModel
    {
        public RangeModel()
        {
            Minimum = 0.0;
            Maximum = 1.0;
        }

        public bool Enabled { get; set; }
        public double Minimum { get; set; }
        public double Maximum { get; set; }
    }
}
