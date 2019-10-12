using System;

namespace CMiX.MVVM.Models
{
    public class ModifierModel : Model
    {
        public ModifierModel()
        {
            GeometryTranslate = new GeometryTranslateModel();
            GeometryScale = new GeometryScaleModel();
            GeometryRotation = new GeometryRotationModel();
            Translate = new SliderModel();
            Scale = new SliderModel();
            Rotation = new SliderModel();

            TranslateModifierModel = new BeatModifierModel();
            ScaleModifierModel = new BeatModifierModel();
            RotationModifierModel = new BeatModifierModel();
            Counter = new CounterModel();
        }

        public ModifierModel(string messageaddress) : this()
        {
            MessageAddress = messageaddress;

            Counter = new CounterModel(messageaddress);
            TranslateModifierModel = new BeatModifierModel(messageaddress);
            ScaleModifierModel = new BeatModifierModel(messageaddress);
            RotationModifierModel = new BeatModifierModel(messageaddress);
        }

        public CounterModel Counter { get; set; }

        public SliderModel Translate { get; set; }
        public SliderModel Scale { get; set; }
        public SliderModel Rotation { get; set; }

        public GeometryTranslateModel GeometryTranslate { get; set; }
        public GeometryScaleModel GeometryScale { get; set; }
        public GeometryRotationModel GeometryRotation { get; set; }

        public BeatModifierModel TranslateModifierModel { get; set; }
        public BeatModifierModel ScaleModifierModel { get; set; }
        public BeatModifierModel RotationModifierModel { get; set; }

        public bool KeepAspectRatio { get; set; }
    }
}