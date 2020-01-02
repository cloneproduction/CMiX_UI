using System;
using System.ComponentModel;
using Ceras;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Message;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.Engine.ViewModel
{
    public class Layer : IMessageReceiver
    {
        public Layer(NetMQClient netMQClient, string messageAddress, CerasSerializer serializer)
        {
            MessageAddress = messageAddress;
            Serializer = serializer;
            NetMQClient = netMQClient;
            NetMQClient.ByteMessage.PropertyChanged += OnMessageReceived;

            Fade = new Slider(this.NetMQClient, MessageAddress + nameof(Fade), this.Serializer);
            BlendMode = new BlendMode(this.NetMQClient, MessageAddress, this.Serializer);
            Content = new Content(this.NetMQClient, MessageAddress + nameof(Content), this.Serializer);
        }

        public string MessageAddress { get; set; }
        public NetMQClient NetMQClient { get; set; }
        public CerasSerializer Serializer { get; set; }

        public Slider Fade { get; set; }
        public BlendMode BlendMode { get; set; }
        public Content Content { get; set; }
        public string DisplayName { get; set; }
        public int ID { get; set; }


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
                            LayerModel layerModel = NetMQClient.ByteMessage.Payload as LayerModel;
                            this.PasteData(layerModel);
                            Console.WriteLine($"{MessageAddress} {MessageCommand.VIEWMODEL_UPDATE}");
                        }
                        break;
                }
            }
        }

        public void PasteData(LayerModel layerModel)
        {
            this.DisplayName = layerModel.DisplayName;
            this.ID = layerModel.ID;
            this.MessageAddress = layerModel.MessageAddress;

            this.Content.PasteData(layerModel.ContentModel);
            this.BlendMode.PasteData(layerModel.BlendMode);
            this.Fade.PasteData(layerModel.Fade);
        }
    }
}