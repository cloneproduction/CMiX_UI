using System;

namespace CMiX.MVVM.Models
{
    public class RandomXYZModel : ITransformModifierModel
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public int Count { get; set; }
        public bool Enabled { get; set; }
    }
}
