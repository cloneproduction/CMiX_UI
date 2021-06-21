// Copyright (c) CloneProduction Shanghai Company Limited (https://cloneproduction.net/)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using CMiX.Core.Presentation.ViewModels;
using System;

namespace CMiX.Core.Models
{
    [Serializable]
    public class MaskModel : Model
    {
        public MaskModel()
        {
            KeepOriginal = true;
            MaskType = ((MaskType)2).ToString();
            MaskControlType = ((MaskControlType)1).ToString();
            Enabled = false;
        }
        public bool IsMask { get; set; }
        public bool KeepOriginal { get; set; }
        public string MaskType { get; set; }
        public string MaskControlType { get; set; }
    }
}