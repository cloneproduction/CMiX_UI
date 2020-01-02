using System;
using System.ComponentModel;
using Ceras;
using CMiX.MVVM.Message;

namespace CMiX.Engine.ViewModel
{
    public class ViewModel
    {
        public ViewModel(NetMQClient netMQClient, string messageAddress, CerasSerializer serializer)
        {
            MessageAddress = messageAddress;
            Serializer = serializer;
            NetMQClient = netMQClient;
            NetMQClient.ByteMessage.PropertyChanged += OnByteReceived;
        }

        private void OnByteReceived(object sender, PropertyChangedEventArgs e)
        {
            ByteReceived();
        }

        public virtual void ByteReceived()
        {

        }

        public string MessageAddress { get; set; }
        public NetMQClient NetMQClient { get; set; }
        public CerasSerializer Serializer { get; set; }
    }
}