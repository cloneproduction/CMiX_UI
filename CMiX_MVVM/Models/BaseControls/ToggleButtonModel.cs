using System;

namespace CMiX.MVVM.Models
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
