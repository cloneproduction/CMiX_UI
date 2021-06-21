﻿// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

namespace CMiX.Core.Models
{
    public class RangeModel : Model, IRangeModel
    {
        public RangeModel()
        {
            this.ID = Guid.NewGuid();
            Minimum = 0.0;
            Maximum = 1.0;
        }

        public double Minimum { get; set; }
        public double Maximum { get; set; }
    }
}
