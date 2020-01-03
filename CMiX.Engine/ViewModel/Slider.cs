using System;
using Ceras;
using CMiX.MVVM.Message;
using CMiX.MVVM.Models;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Services;
using System.ComponentModel;

namespace CMiX.Engine.ViewModel
{
    public class Slider : IEngineViewModel
    {
        public Slider(Receiver receiver, string messageAddress)
        {
            MessageAddress = messageAddress;
            Receiver = receiver;
            Receiver.MessageReceived += OnMessageReceived;
            Amount = 0.0;
        }

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

        public string MessageAddress { get; set; }
        public Receiver Receiver { get; set; }

        private double _amount;
        public double Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                Console.WriteLine(this.MessageAddress + " : " + Amount);
            }
        }

        public void PasteModel(IModel model)
        {
            SliderModel sliderModel = model as SliderModel;
            this.MessageAddress = sliderModel.MessageAddress;
            this.Amount = sliderModel.Amount;
        }

        public void CopyModel(IModel model)
        {
            throw new NotImplementedException();
        }
    }
}