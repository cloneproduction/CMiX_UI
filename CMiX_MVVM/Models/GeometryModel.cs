using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class GeometryModel : Model
    {
        public GeometryModel()
        {
            FileSelector = new FileSelectorModel();

            Counter = new CounterModel();
            GeometryFX = new GeometryFXModel();

            //GeometryTranslate = new GeometryTranslateModel();
            //GeometryScale = new GeometryScaleModel();
            //GeometryRotation = new GeometryRotationModel();

            //Translate = new SliderModel();
            //Scale = new SliderModel();
            //Rotation = new SliderModel();
        }

        public GeometryModel(string messageaddress)
            : this()
        {
            MessageAddress = messageaddress;
            FileSelector = new FileSelectorModel(messageaddress);
        }

        public FileSelectorModel FileSelector { get; set; }
        public CounterModel Counter { get; set; }

        //public SliderModel Translate { get; set; }
        //public SliderModel Scale { get; set; }
        //public SliderModel Rotation { get; set; }

        //public GeometryTranslateModel GeometryTranslate { get; set; }
        //public GeometryScaleModel GeometryScale { get; set; }
        //public GeometryRotationModel GeometryRotation { get; set; }

        public GeometryFXModel GeometryFX { get; set; }

        public bool Is3D { get; set; }
        public bool KeepAspectRatio { get; set; }
    }
}