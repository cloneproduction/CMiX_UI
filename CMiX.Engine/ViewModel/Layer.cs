using System;
using System.ComponentModel;
using Ceras;
using CMiX.MVVM;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Message;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.Engine.ViewModel
{
    public class Layer : ICopyPasteModel, IMessageReceiver
    {
        public Layer(Receiver receiver, string messageAddress)
        {
            MessageAddress = messageAddress;
            Receiver = receiver;
            Receiver.MessageReceived += OnMessageReceived;

            Fade = new Slider(receiver, MessageAddress + nameof(Fade));
            BlendMode = new BlendMode(receiver, MessageAddress);
            Content = new Content(receiver, MessageAddress + nameof(Content));
        }

        public string MessageAddress { get; set; }
        public Receiver Receiver { get; set; }

        public Slider Fade { get; set; }
        public BlendMode BlendMode { get; set; }
        public Content Content { get; set; }
        public string DisplayName { get; set; }
        public int ID { get; set; }

        public void OnMessageReceived(object sender, EventArgs e)
        {
            if (MessageAddress == Receiver.ReceivedAddress && Receiver.ReceivedData != null)
            {
                object data = Receiver.ReceivedData;
                if (MessageCommand.VIEWMODEL_UPDATE == Receiver.ReceivedCommand)
                {
                    this.PasteModel(data as IModel);
                }
            }
        }

        public void PasteModel(IModel model)
        {
            LayerModel layerModel = model as LayerModel;
            this.DisplayName = layerModel.DisplayName;
            this.ID = layerModel.ID;
            this.MessageAddress = layerModel.MessageAddress;

            this.Content.PasteModel(layerModel.ContentModel);
            this.BlendMode.PasteModel(layerModel.BlendMode);
            this.Fade.PasteModel(layerModel.Fade);
        }

        public void CopyModel(IModel model)
        {
            throw new NotImplementedException();
        }
    }
}