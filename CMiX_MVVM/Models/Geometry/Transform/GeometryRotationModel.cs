// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Presentation.ViewModels;
using System;

namespace CMiX.Core.Models
{
    [Serializable]
    public class GeometryRotationModel
    {
        public GeometryRotationModel()
        {

        }

        public GeometryRotationMode Mode { get; set; }
        public bool RotationX { get; set; }
        public bool RotationY { get; set; }
        public bool RotationZ { get; set; }
    }
}
