using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class TransformModel
    {
        public TransformModel()
        {
            GeometryTranslateModel = new GeometryTranslateModel();
            GeometryScaleModel = new GeometryScaleModel();
            GeometryRotationModel = new GeometryRotationModel();

            TranslateModel = new TranslateModel();
            ScaleModel = new ScaleModel();
            RotationModel = new RotationModel();
        }

        public TranslateModel TranslateModel { get; set; }
        public ScaleModel ScaleModel { get; set; }
        public RotationModel RotationModel { get; set; }

        public GeometryTranslateModel GeometryTranslateModel { get; set; }
        public GeometryScaleModel GeometryScaleModel { get; set; }
        public GeometryRotationModel GeometryRotationModel { get; set; }

        public bool Is3D { get; set; }
    }
}