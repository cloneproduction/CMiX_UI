﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Models;
using System;

namespace CMiX.Core.Models
{
    [Serializable]
    public class GeometryModel : Model, IModel
    {
        public GeometryModel()
        {
            this.ID = Guid.NewGuid();
            GeometryFXModel = new GeometryFXModel();
            TransformModel = new TransformModel();
            InstancerModel = new InstancerModel();
            AssetPathSelectorModel = new AssetPathSelectorModel();
        }

        public TransformModel TransformModel { get; set; }
        public InstancerModel InstancerModel { get; set; }
        public GeometryFXModel GeometryFXModel{ get; set; }
        public AssetPathSelectorModel AssetPathSelectorModel { get; set; }
    }
}