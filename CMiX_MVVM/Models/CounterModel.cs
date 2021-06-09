using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class CounterModel : Model
    {
        public CounterModel()
        {
            this.ID = Guid.NewGuid();
        }

        public int Count { get; set; }
    }
}