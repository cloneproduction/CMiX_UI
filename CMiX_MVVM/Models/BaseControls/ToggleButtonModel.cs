using System;

namespace CMiX.Core.Models
{
    public class ToggleButtonModel : Model
    {
        public ToggleButtonModel()
        {
            this.ID = Guid.NewGuid();
        }

        public bool IsChecked { get; set; }
    }
}
