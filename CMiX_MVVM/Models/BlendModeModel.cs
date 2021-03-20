using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels;
using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class BlendModeModel : IModel
    {
        public BlendModeModel()
        {
            Mode = ((BlendModeEnum)0).ToString();
        }

        public string Mode { get; set; }
        public bool Enabled { get; set; }
    }
}
