// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Models;
using System;

namespace CMiX.Core.Models
{
    [Serializable]
    public class SliderModel : Model, IModel
    {
        public SliderModel()
        {
            ID = Guid.NewGuid();
            Amount = 0.0;
            Enabled = true;
        }

        public double Amount { get; set; }
        public string Address { get; set; }
    }
}