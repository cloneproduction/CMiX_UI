using System;

namespace CMiX.MVVM.Models
{
    public class RandomXYZModel : ITransformModifierModel
    {
        public RandomXYZModel()
        {
            BeatModifierModel = new BeatModifierModel();
        }

        public BeatModifierModel BeatModifierModel { get; set; }
        public string Name { get; set; }
        public int ID { get; set; }
        public int Count { get; set; }
        public bool Enabled { get; set; }
    }
}
