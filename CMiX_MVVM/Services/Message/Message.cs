﻿using System.ComponentModel;
using NetMQ;
using Ceras;
using CMiX.MVVM.Commands;
using System;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.Message
{
    public class Message
    {
        public Message()
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
                OnMessageUpdated(this);
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
                    return Serializer.Deserialize<object>(NetMQMessage[3].Buffer);
                else
                    return null;
            }
        }

        public object Payload
        {
            get
            {
                if (NetMQMessage[4].Buffer != null)
                    return Serializer.Deserialize<object>(NetMQMessage[4].Buffer);
                else
                    return null;
            }
        }

        public event EventHandler<MessageEventArgs> MessageUpdated;
        private void OnMessageUpdated(Message message)
        {
            if (MessageUpdated != null)
                MessageUpdated(this, new MessageEventArgs(message.MessageAddress, message.Command, message.Parameter, message.Payload));
        }
    }
}