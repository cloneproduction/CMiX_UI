using System;

namespace CMiX.MVVM.Models
{
    public class RandomXYZModel : Model, ITransformModifierModel
    {
        public RandomXYZModel()
        {
            BeatModifierModel = new BeatModifierModel();
            CounterModel = new CounterModel();
            EasingModel = new EasingModel();
        }

        public EasingModel EasingModel { get; set; }
        public CounterModel CounterModel { get; set; }

        public SliderModel LocationX { get; set; }
        public SliderModel LocationY { get; set; }
        public SliderModel LocationZ { get; set; }

        public SliderModel ScaleX { get; set; }
        public SliderModel ScaleY { get; set; }
        public SliderModel ScaleZ { get; set; }

        public SliderModel RotationX { get; set; }
        public SliderModel RotationY { get; set; }
        public SliderModel RotationZ { get; set; }


        public BeatModifierModel BeatModifierModel { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
