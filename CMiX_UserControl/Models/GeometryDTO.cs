
using CMiX.Controls;
using System;
using System.Collections.Generic;

namespace CMiX.Models
{
    [Serializable]
    public class GeometryDTO
    {
        public GeometryDTO()
        {
            GeometryTranslate = new GeometryTranslateDTO();
            GeometryScale = new GeometryScaleDTO();
            GeometryRotation = new GeometryRotationDTO();
            GeometryPaths = new List<ListBoxFileName>();
            GeometryFX = new GeometryFXDTO();
        }

        public string MessageAddress { get; set; }

        public List<ListBoxFileName> GeometryPaths { get; set; }

        public int Count { get; set; }

        public double TranslateAmount { get; set; }

        public double ScaleAmount { get; set; }

        public double RotationAmount { get; set; }

        public bool Is3D { get; set; }

        public bool KeepAspectRatio { get; set; }

        public GeometryTranslateDTO GeometryTranslate { get; set; }

        public GeometryScaleDTO GeometryScale { get; set; }

        public GeometryRotationDTO GeometryRotation { get; set; }

        public GeometryFXDTO GeometryFX { get; set; }
    }
}
