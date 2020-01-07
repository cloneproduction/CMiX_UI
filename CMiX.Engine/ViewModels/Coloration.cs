using System;
using CMiX.MVVM;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.Engine.ViewModels
{
    public class Coloration : ICopyPasteModel<ColorationModel>, IMessageReceiver
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

        public void PasteModel(ColorationModel colorationModel)
        {
            ColorSelector.PasteModel(colorationModel.ColorSelectorModel);
        }

        public void CopyModel(ColorationModel colorationModel)
        {
            throw new NotImplementedException();
        }
    }
}
