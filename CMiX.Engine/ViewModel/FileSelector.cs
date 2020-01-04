using System;
using CMiX.MVVM;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.Engine.ViewModels
{
    public class FileSelector : ICopyPasteModel, IMessageReceiver
    {
        public FileSelector(Receiver receiver, string messageAddress)
        {
            Receiver = receiver;
            Receiver.MessageReceived += OnMessageReceived;
            MessageAddress = $"{messageAddress}{nameof(FileSelector)}/";
        }

        public string MessageAddress { get; set; }
        public Receiver Receiver { get; set; }

        public void CopyModel(IModel model)
        {
            throw new NotImplementedException();
        }

        public void OnMessageReceived(object sender, EventArgs e)
        {
            Receiver.UpdateViewModel(MessageAddress, this);
        }

        public void PasteModel(IModel model)
        {
            throw new NotImplementedException();
        }
    }
}