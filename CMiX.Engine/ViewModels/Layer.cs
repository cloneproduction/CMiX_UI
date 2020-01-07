using System;
using CMiX.MVVM;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.Engine.ViewModels
{
    public class Layer : ICopyPasteModel<LayerModel>, IMessageReceiver
    {
        public Layer(Receiver receiver, string messageAddress)
        {
            MessageAddress = messageAddress;
            Receiver = receiver;
            Receiver.MessageReceived += OnMessageReceived;

            Fade = new Slider(receiver, MessageAddress + nameof(Fade));
            BlendMode = new BlendMode(receiver, MessageAddress);
            Content = new Content(receiver, MessageAddress);
        }

        public string MessageAddress { get; set; }
        public Receiver Receiver { get; set; }

        public Slider Fade { get; set; }
        public BlendMode BlendMode { get; set; }
        public Content Content { get; set; }
        public string Name { get; set; }
        public int ID { get; set; }

        public void OnMessageReceived(object sender, EventArgs e)
        {
            Receiver.UpdateViewModel(MessageAddress, this);
        }

        public void PasteModel(LayerModel layerModel)
        {
            this.ID = layerModel.ID;
            this.Name = layerModel.Name;

            this.Content.PasteModel(layerModel.ContentModel);
            this.BlendMode.PasteModel(layerModel.BlendMode);
            this.Fade.PasteModel(layerModel.Fade);
        }

        public void CopyModel(LayerModel layerModel)
        {
            throw new NotImplementedException();
        }
    }
}