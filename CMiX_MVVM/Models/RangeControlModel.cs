namespace CMiX.Core.Models
{
    public class RangeControlModel : IModel
    {
        public RangeControlModel()
        {
            Range = new SliderModel();
            //Modifier = ((RangeModifier)0).ToString();
        }

        public SliderModel Range { get; set; }
        public string Modifier { get; set; }
        public bool Enabled { get; set; }
    }
}