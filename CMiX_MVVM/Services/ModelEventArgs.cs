using CMiX.MVVM.Interfaces;
using System;

namespace CMiX.MVVM.Services
{
    public class ModelEventArgs : EventArgs
    {
        public ModelEventArgs(IModel model, string messageAddress, string parentMessageAddress)
        {
            Model = model;
            MessageAddress = messageAddress;
            ParentMessageAddress = parentMessageAddress;
        }

        public string ParentMessageAddress { get; set; }
        public string MessageAddress { get; set; }
        public IModel Model { get; set; }
    }
}
