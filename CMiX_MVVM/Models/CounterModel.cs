using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class CounterModel : Model
    {
        public CounterModel()
        {

        }

        public int Count { get; set; }
    }
}