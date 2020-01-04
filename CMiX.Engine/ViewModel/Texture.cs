using CMiX.MVVM;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System;

namespace CMiX.Engine.ViewModel
{
    public class Texture : IMessageReceiver, ICopyPasteModel
    {
        public Texture(Receiver receiver, string messageAddress)
        {

        }

        public string MessageAddress { get; set; }
        public Receiver Receiver { get; set; }

        public void OnMessageReceived(object sender, EventArgs e)
        {

        }

        public void PasteModel(IModel model)
        {
            TextureModel textureModel = model as TextureModel;
            MessageAddress = textureModel.MessageAddress;
        }

        public void CopyModel(IModel model)
        {
            throw new NotImplementedException();
        }
    }
}