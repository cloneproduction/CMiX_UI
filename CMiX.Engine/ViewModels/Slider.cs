using System;
using CMiX.MVVM.Models;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Services;
using CMiX.MVVM;

namespace CMiX.Engine.ViewModels
{
    public class Slider : ICopyPasteModel, IMessageReceiver
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
            Receiver.UpdateViewModel(MessageAddress, this);
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

        public void PasteModel(SliderModel sliderModel)
        {
            this.Amount = sliderModel.Amount;
        }

        public void CopyModel(SliderModel sliderModel)
        {
            throw new NotImplementedException();
        }
    }
}