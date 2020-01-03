using CMiX.MVVM.Message;
using System;

namespace CMiX.MVVM.Services
{
    public interface IMessageReceiver
    {
        string MessageAddress { get; set; }
        void OnMessageReceived(object sender, EventArgs e);
        Receiver Receiver { get; set; }
    }
}