using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class BlendModeModel : IModel
    {
        public BlendModeModel()
        {

        }

        public string Mode { get; set; }
        public string MessageAddress { get; set; }
    }
}
