using System;
using Ceras;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Message;
using CMiX.MVVM.Models;

namespace CMiX.Engine.ViewModel
{
    public class Layer : ViewModel
    {
        public Layer(NetMQClient netMQClient, string messageAddress, CerasSerializer serializer)
            : base(netMQClient, messageAddress, serializer)
        {
            Fade = new Slider(this.NetMQClient, this.MessageAddress + nameof(Fade), this.Serializer);
            BlendMode = new BlendMode(this.NetMQClient, this.MessageAddress + nameof(BlendMode), this.Serializer);
            Content = new Content(this.NetMQClient, this.MessageAddress + nameof(Content), this.Serializer);
        }

        public Slider Fade { get; set; }
        public BlendMode BlendMode { get; set; }
        public Content Content { get; set; }
        
        public string DisplayName { get; set; }

        public int ID { get; set; }

        public override void ByteReceived()
        {
            string receivedAddress = NetMQClient.ByteMessage.MessageAddress;

            if (receivedAddress == this.MessageAddress)
            {
                MessageCommand command = NetMQClient.ByteMessage.Command;
                switch (command)
                {
                    case MessageCommand.LAYER_UPDATE_BLENDMODE:
                        if (NetMQClient.ByteMessage.Payload != null)
                        {
                            LayerModel layerModel = NetMQClient.ByteMessage.Payload as LayerModel;
                            this.PasteData(layerModel);
                            Console.WriteLine($"{MessageAddress} {MessageCommand.LAYER_UPDATE_BLENDMODE.ToString()}");
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