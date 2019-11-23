using System;
using CMiX.MVVM.Message;
using Ceras;
using CMiX.MVVM.Models;
using CMiX.MVVM.Commands;

namespace CMiX.Engine.ViewModel
{
    public class Slider : ViewModel
    {
        public Slider(NetMQClient netMQClient, string messageAddress, CerasSerializer serializer)
            : base(netMQClient, messageAddress, serializer)
        {
            Amount = 0.0;
        }

        public override void ByteReceived()
        {
            string receivedAddress = NetMQClient.ByteMessage.MessageAddress;

            if (receivedAddress == this.MessageAddress)
            {
                MessageCommand command = NetMQClient.ByteMessage.Command;
                switch (command)
                {
                    case MessageCommand.VIEWMODEL_UPDATE:
                        if (NetMQClient.ByteMessage.Payload != null)
                        {
                            SliderModel sliderModel = NetMQClient.ByteMessage.Payload as SliderModel;
                            this.PasteData(sliderModel);
                        }
                        break;
                }
            }
        }

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

        public void PasteData(SliderModel sliderModel)
        {
            this.MessageAddress = sliderModel.MessageAddress;
            this.Amount = sliderModel.Amount;
        }
    }
}