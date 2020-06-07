using System;
using System.Collections.ObjectModel;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class MaskModel : Model
    {
        public MaskModel()
        {

        }
        public bool IsMask { get; set; }
        public bool KeepOriginal { get; set; }
        public string MaskType { get; set; }
        public string MaskControlType { get; set; }
    }
}