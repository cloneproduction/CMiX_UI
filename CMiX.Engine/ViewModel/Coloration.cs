using System;
using CMiX.MVVM;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.Engine.ViewModel
{
    public class Coloration : ICopyPasteModel, IMessageReceiver
    {
        public Coloration(Receiver receiver, string messageAddress)
        {
            MessageAddress = $"{messageAddress}{nameof(Coloration)}/";
            Receiver = receiver;
            Receiver.MessageReceived += OnMessageReceived;
            ColorSelector = new ColorSelector(receiver, MessageAddress);
        }

        public void OnMessageReceived(object sender, EventArgs e)
        {
            Receiver.UpdateViewModel(MessageAddress, this);
        }

        public ColorSelector ColorSelector { get; set; }
        public string MessageAddress { get; set; }
        public Receiver Receiver { get; set; }

        public void PasteModel(IModel model)
        {
            ColorationModel colorationModel = model as ColorationModel;
            MessageAddress = colorationModel.MessageAddress;
            ColorSelector.PasteModel(colorationModel.ColorSelectorModel);
        }

        public void CopyModel(IModel model)
        {
            throw new NotImplementedException();
        }
    }
}
