using CMiX.Services;
using System;

namespace CMiX.Models
{
    [Serializable]
    public class CounterModel
    {
        public string MessageAddress { get; set; }

        [OSC]
        public int Count { get; set; }
    }
}