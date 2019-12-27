using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class TransformModel : IModel
    {
        public TransformModel()
        {
            GeometryTranslate = new GeometryTranslateModel();
            GeometryScale = new GeometryScaleModel();
            GeometryRotation = new GeometryRotationModel();

            Translate = new TranslateModel();
            Scale = new ScaleModel();
            Rotation = new RotationModel();
        }

        public TransformModel(string messageaddress)
            : this()
        {
            MessageAddress = messageaddress;
        }

        public TranslateModel Translate { get; set; }
        public ScaleModel Scale { get; set; }
        public RotationModel Rotation { get; set; }

        public GeometryTranslateModel GeometryTranslate { get; set; }
        public GeometryScaleModel GeometryScale { get; set; }
        public GeometryRotationModel GeometryRotation { get; set; }

        public bool Is3D { get; set; }
        public string MessageAddress { get; set; }
    }
}