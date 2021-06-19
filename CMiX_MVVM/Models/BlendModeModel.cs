using CMiX.Core.Interfaces;
using CMiX.Core.Presentation.ViewModels;
using System;

namespace CMiX.Core.Models
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
