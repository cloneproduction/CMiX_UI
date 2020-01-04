using System;
using CMiX.MVVM;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.Engine.ViewModel
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
            if (MessageAddress == Receiver.ReceivedAddress && Receiver.ReceivedData != null)
            {
                object data = Receiver.ReceivedData;
                if (MessageCommand.VIEWMODEL_UPDATE == Receiver.ReceivedCommand)
                {
                    this.PasteModel(data as IModel);
                    Console.WriteLine(this.MessageAddress + " : " + Mode);
                }
            }
        }

        public string Mode { get; set; }
        public string MessageAddress { get; set; }
        public Receiver Receiver { get; set; }

        public void PasteModel(IModel model)
        {
            BlendModeModel blendModeModel = model as BlendModeModel;
            MessageAddress = blendModeModel.MessageAddress;
            Mode = blendModeModel.Mode;
        }

        public void CopyModel(IModel model)
        {
            throw new NotImplementedException();
        }
    }
}