using System;

namespace CMiX.Core.Models
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