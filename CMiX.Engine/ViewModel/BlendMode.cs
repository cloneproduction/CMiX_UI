using System;
using System.ComponentModel;
using Ceras;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Message;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.Engine.ViewModel
{
    public class BlendMode : IMessageReceiver
    {
        public BlendMode(NetMQClient netMQClient, string messageAddress, CerasSerializer serializer)
        {
            MessageAddress = $"{messageAddress}{nameof(BlendMode)}";
            Serializer = serializer;
            NetMQClient = netMQClient;
            NetMQClient.ByteMessage.PropertyChanged += OnMessageReceived;
            Mode = ((MVVM.ViewModels.BlendMode)0).ToString();
        }

        public void OnMessageReceived(object sender, PropertyChangedEventArgs e)
        {
            string receivedAddress = NetMQClient.ByteMessage.MessageAddress;
            if (receivedAddress == this.MessageAddress)
            {
                MessageCommand command = NetMQClient.ByteMessage.Command;
                switch (command)
                {
                    case MessageCommand.VIEWMODEL_UPDATE:
                        if (NetMQClient.ByteMessage.Payload != null)
                        {
                            BlendModeModel blendModeModel = NetMQClient.ByteMessage.Payload as BlendModeModel;
                            this.PasteData(blendModeModel);
                            Console.WriteLine($"{MessageAddress} {Mode}");
                        }
                        break;
                }
            }
        }

        public string Mode { get; set; }
        public string MessageAddress { get; set; }
        public NetMQClient NetMQClient { get; set; }
        public CerasSerializer Serializer { get; set; }

        public void PasteData(BlendModeModel blendModeModel)
        {
            MessageAddress = blendModeModel.MessageAddress;
            Mode = blendModeModel.Mode;
        }
    }
}