using CMiX.MVVM.Models;
using System;

namespace CMiX.MVVM.Services
{
    public class ModelEventArgs : EventArgs
    {
        public ModelEventArgs(IModel model, string messageAddress)
        {
            Model = model;
            MessageAddress = messageAddress;
        }

        public string MessageAddress { get; set; }
        public IModel Model { get; set; }
    }
}
