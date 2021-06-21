// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

namespace CMiX.Core.Models
{
    public class RangeControlModel : IModel
    {
        public RangeControlModel()
        {
            Range = new SliderModel();
            //Modifier = ((RangeModifier)0).ToString();
        }

        public SliderModel Range { get; set; }
        public string Modifier { get; set; }
        public bool Enabled { get; set; }
    }
}