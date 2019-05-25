using CMiX.Services;
using System;

namespace CMiX.Models
{
    [Serializable]
    public class CounterModel
    {
        public CounterModel()
        {
            Count = 1;
        }

        public string MessageAddress { get; set; }

        [OSC]
        public int Count { get; set; }
    }
}