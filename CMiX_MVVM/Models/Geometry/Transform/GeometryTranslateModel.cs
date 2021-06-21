﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using CMiX.Core.Presentation.ViewModels;

namespace CMiX.Core.Models
{
    [Serializable]
    public class GeometryTranslateModel
    {
        public GeometryTranslateModel()
        {

        }

        public GeometryTranslateMode Mode { get; set; }
    }
}