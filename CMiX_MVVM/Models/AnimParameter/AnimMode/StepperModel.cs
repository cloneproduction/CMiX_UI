// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

namespace CMiX.Core.Models
{
    public class StepperModel : Model, IAnimModeModel
    {
        public StepperModel()
        {
            this.ID = Guid.NewGuid();
        }

        public double Width { get; set; }
        public int StepCount { get; set; }
    }
}
