using System;

namespace CMiX.MVVM.Models
{
    [Serializable]
    public class CounterModel : IModel
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

        public int Count { get; set; }
        public string MessageAddress { get; set; }
    }
}