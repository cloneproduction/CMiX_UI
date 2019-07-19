﻿using System;
using CMiX.MVVM.Models;
using CMiX.MVVM.ViewModels;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class GeometryModel : Model
    {
        public GeometryModel()
        {
            FileSelector = new FileSelectorModel();
            GeometryTranslate = new GeometryTranslateModel();
            GeometryScale = new GeometryScaleModel();
            GeometryRotation = new GeometryRotationModel();
            GeometryFX = new GeometryFXModel();
            Translate = new SliderModel();
            Scale = new SliderModel();
            Rotation = new SliderModel();
            Counter = new CounterModel();
        }

        public GeometryModel(string messageaddress)
            : this()
        {
            MessageAddress = messageaddress;
            FileSelector = new FileSelectorModel(messageaddress);
        }

        public FileSelectorModel FileSelector { get; set; }
        public SliderModel Translate { get; set; }
        public SliderModel Scale { get; set; }
        public SliderModel Rotation { get; set; }
        public CounterModel Counter { get; set; }
        public GeometryTranslateModel GeometryTranslate { get; set; }
        public GeometryScaleModel GeometryScale { get; set; }
        public GeometryRotationModel GeometryRotation { get; set; }
        public GeometryFXModel GeometryFX { get; set; }

        public bool Is3D { get; set; }
        public bool KeepAspectRatio { get; set; }
    }
}