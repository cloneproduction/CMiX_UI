// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Presentation.ViewModels;
using System;

namespace CMiX.Core.Models
{
    public class InverterModel : Model
    {
        public InverterModel()
        {
            ID = Guid.NewGuid();
            Invert = new SliderModel();
            InvertMode = new ComboBoxModel<TextureInvertMode>();
        }

        public SliderModel Invert { get; set; }
        public ComboBoxModel<TextureInvertMode> InvertMode { get; set; }
    }
}
