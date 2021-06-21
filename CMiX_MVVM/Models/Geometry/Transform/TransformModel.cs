﻿using CMiX.Core.Models;
using System;

namespace CMiX.Core.Models
{
    public class TransformModel : Model, IModel
    {
        public TransformModel()
        {
            this.ID = Guid.NewGuid();
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