using System;
using CMiX.MVVM;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.Engine.ViewModels
{
    public class FileSelector : ICopyPasteModel<FileSelectorModel>, IMessageReceiver
    {
        public FileSelector(Receiver receiver, string messageAddress)
        {
            Receiver = receiver;
            Receiver.MessageReceived += OnMessageReceived;
            MessageAddress = $"{messageAddress}{nameof(FileSelector)}/";
        }

        public string MessageAddress { get; set; }
        public Receiver Receiver { get; set; }

        public void OnMessageReceived(object sender, EventArgs e)
        {
            Receiver.UpdateViewModel(MessageAddress, this);
        }

        public void CopyModel(FileSelectorModel fileSelectorModel)
        {
            throw new NotImplementedException();
        }

        public void PasteModel(FileSelectorModel fileSelectorModel)
        {
            throw new NotImplementedException();
        }
    }
}