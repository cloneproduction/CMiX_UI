// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;

namespace CMiX.Core.Models
{
    [Serializable]
    public class TranslateModifierModel
    {
        public TranslateModifierModel()
        {
            Translate = new AnimParameterModel();
            TranslateX = new AnimParameterModel();
            TranslateY = new AnimParameterModel();
            TranslateZ = new AnimParameterModel();
        }

        public AnimParameterModel Translate { get; set; }
        public AnimParameterModel TranslateX { get; set; }
        public AnimParameterModel TranslateY { get; set; }
        public AnimParameterModel TranslateZ { get; set; }
    }
}