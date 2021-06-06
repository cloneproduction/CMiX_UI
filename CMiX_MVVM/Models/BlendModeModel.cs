using CMiX.MVVM.Interfaces;
using CMiX.MVVM.ViewModels;
using System;

namespace CMiX.MVVM.Models
{
    public class BlendModeModel : Model, IModel
    {
        public BlendModeModel()
        {
            this.ID = Guid.NewGuid();
            Mode = ((BlendModeEnum)0).ToString();
        }

        public string Mode { get; set; }
    }
}
