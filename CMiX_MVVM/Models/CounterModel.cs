//using CMiX.Services;
using CMiX.MVVM.Models;
using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class CounterModel : Model
    {
        public CounterModel()
        {
           
        }

        public CounterModel(string messageaddress)
            : this()
        {
            Count = 1;
            MessageAddress = String.Format("{0}{1}/", messageaddress, "Content");
        }

        //[OSC]
        public int Count { get; set; }
    }
}