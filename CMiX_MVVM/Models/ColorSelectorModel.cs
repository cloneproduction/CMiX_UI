// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Models;
using System;

namespace CMiX.Core.Models
{
    [Serializable]
    public class ColorSelectorModel : Model, IModel
    {
        public ColorSelectorModel()
        {
            ID = Guid.NewGuid();
            ColorPickerModel = new ColorPickerModel();
        }

        public ColorPickerModel ColorPickerModel { get; set; }
    }
}