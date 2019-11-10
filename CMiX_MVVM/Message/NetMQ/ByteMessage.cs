﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMQ;
using Ceras;

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
            get { return netMQMessage; }
            set
            {
                netMQMessage = value;
                OnPropertyChanged(nameof(NetMQMessage));
            }
        }

        public string Topic
        {
            get
            {
                return NetMQMessage[0].ConvertToString();
            }
        }

        public string Command
        {
            get
            {
                return NetMQMessage[1].ConvertToString();
            }
        }

        public object Parameter
        {
            get
            {
                if (NetMQMessage[2].Buffer != null)
                {
                    return Serializer.Deserialize<object>(NetMQMessage[0].Buffer);
                }
                else
                    return null;
            }
        }

        public object Payload
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
