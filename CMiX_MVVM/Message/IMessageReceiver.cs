using Ceras;
using CMiX.MVVM.Message;
using System.ComponentModel;

namespace CMiX.MVVM.Services
{
    public interface IMessageReceiver
    {
        string MessageAddress { get; set; }
        void OnMessageReceived(object sender, PropertyChangedEventArgs e);
        NetMQClient NetMQClient { get; set; }
        CerasSerializer Serializer { get; set; }
    }
}
