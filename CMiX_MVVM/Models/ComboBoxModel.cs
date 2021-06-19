using System;

namespace CMiX.Core.Models
{
    public class ComboBoxModel<T> : Model
    {
        public ComboBoxModel()
        {
            this.ID = Guid.NewGuid();
        }
        public T Selection { get; set; }
    }
}