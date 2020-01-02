using System.ComponentModel;
using NetMQ;
using Ceras;
using CMiX.MVVM.Commands;

namespace CMiX.MVVM.Message
{
    public class ByteMessage : INotifyPropertyChanged
    {
        public ByteMessage()
        {
            Serializer = new CerasSerializer();
        }

        public CerasSerializer Serializer { get; set; }

        private NetMQMessage netMQMessage;
        public NetMQMessage NetMQMessage
        {
            get => netMQMessage;
            set
            {
                netMQMessage = value;
                OnPropertyChanged(nameof(NetMQMessage));
            }
        }

        public string Topic
        {
            get => NetMQMessage[0].ConvertToString();
        }

        public string MessageAddress
        {
            get => NetMQMessage[1].ConvertToString();
        }

        public MessageCommand Command
        {
            get => Serializer.Deserialize<MessageCommand>(NetMQMessage[2].Buffer);
        }

        public object Parameter
        {
            get
            {
                if (NetMQMessage[3].Buffer != null)
                {
                    return Serializer.Deserialize<object>(NetMQMessage[3].Buffer);
                }
                else
                    return null;
            }
        }

        public object Payload
        {
            get
            {
                if (NetMQMessage[4].Buffer != null)
                {
                    return Serializer.Deserialize<object>(NetMQMessage[4].Buffer);
                }
                else
                    return null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
