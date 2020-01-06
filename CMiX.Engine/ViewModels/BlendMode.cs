using System;
using CMiX.MVVM;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.Engine.ViewModels
{
    public class BlendMode : IMessageReceiver, ICopyPasteModel
    {
        public BlendMode(Receiver receiver, string messageAddress)
        {
            MessageAddress = $"{messageAddress}{nameof(BlendMode)}";
            Receiver = receiver;
            Receiver.MessageReceived += OnMessageReceived;
            Mode = ((MVVM.ViewModels.BlendMode)0).ToString();
        }

        public void OnMessageReceived(object sender, EventArgs e)
        {
            Receiver.UpdateViewModel(MessageAddress, this);
        }

        public string MessageAddress { get; set; }
        public Receiver Receiver { get; set; }
        public string Mode { get; set; }

        public void PasteModel(IModel model)
        {
            BlendModeModel blendModeModel = model as BlendModeModel;
            MessageAddress = blendModeModel.MessageAddress;
            Mode = blendModeModel.Mode;
            Console.WriteLine(MessageAddress + " " + Mode);
        }

        public void CopyModel(IModel model)
        {
            throw new NotImplementedException();
        }
    }
}