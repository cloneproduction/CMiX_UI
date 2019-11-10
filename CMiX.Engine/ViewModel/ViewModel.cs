using System;
using System.ComponentModel;
using Ceras;
using CMiX.MVVM.Message;

namespace CMiX.Engine.ViewModel
{
    public class ViewModel
    {
        public ViewModel(NetMQClient netMQClient, string topic, CerasSerializer serializer)
        {
            MessageAddress = topic;
            Serializer = serializer;
            NetMQClient = netMQClient;
            NetMQClient.ByteMessage.PropertyChanged += OnByteReceived;
        }

        public string MessageAddress { get; set; }

        private void OnByteReceived(object sender, PropertyChangedEventArgs e)
        {
            ByteReceived();
        }

        public virtual void ByteReceived()
        {

        }

        public NetMQClient NetMQClient { get; set; }
        public CerasSerializer Serializer { get; set; }
    }
}