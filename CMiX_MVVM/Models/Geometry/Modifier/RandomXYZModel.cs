using CMiX.MVVM.ViewModels;
using System;

namespace CMiX.MVVM.Models
{
    public class RandomXYZModel : Model, ITransformModifierModel
    {
        public RandomXYZModel()
        {
            this.ID = Guid.NewGuid();
            Name = TransformModifierNames.Randomized;


            BeatModifierModel = new BeatModifierModel();
            CounterModel = new CounterModel();
            EasingModel = new EasingModel();

            RandomizeLocation = new ToggleButtonModel();
            LocationX = new SliderModel();
            LocationY = new SliderModel();
            LocationZ = new SliderModel();

            RandomizeScale = new ToggleButtonModel();
            ScaleX = new SliderModel();
            ScaleY = new SliderModel();
            ScaleZ = new SliderModel();

            RandomizeRotation = new ToggleButtonModel();
            RotationX = new SliderModel();
            RotationY = new SliderModel();
            RotationZ = new SliderModel();
        }

        public EasingModel EasingModel { get; set; }
        public CounterModel CounterModel { get; set; }

        public ToggleButtonModel RandomizeLocation { get; set; }
        public SliderModel LocationX { get; set; }
        public SliderModel LocationY { get; set; }
        public SliderModel LocationZ { get; set; }

        public ToggleButtonModel RandomizeScale { get; set; }
        public SliderModel ScaleX { get; set; }
        public SliderModel ScaleY { get; set; }
        public SliderModel ScaleZ { get; set; }

        public ToggleButtonModel RandomizeRotation { get; set; }
        public SliderModel RotationX { get; set; }
        public SliderModel RotationY { get; set; }
        public SliderModel RotationZ { get; set; }


        public BeatModifierModel BeatModifierModel { get; set; }
        public TransformModifierNames Name { get; set; }
        public int Count { get; set; }
    }
}
