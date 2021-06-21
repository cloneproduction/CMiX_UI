// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Models;

namespace CMiX.Core.Models
{
    public class BeatModel : Model, IModel
    {
        public BeatModel()
        {

        }

        public double[] Periods { get; set; }
        public double Period { get; set; }
        public double Multiplier { get; set; }
    }
}
