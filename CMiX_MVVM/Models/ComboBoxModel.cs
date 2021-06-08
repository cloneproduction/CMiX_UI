using System;

namespace CMiX.MVVM.Models
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