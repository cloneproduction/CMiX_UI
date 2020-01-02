using System;
using Ceras;
using CMiX.MVVM.Message;
using CMiX.MVVM.Models;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Services;
using System.ComponentModel;

namespace CMiX.Engine.ViewModel
{
    public class Slider : IMessageReceiver
    {
        public Slider(NetMQClient netMQClient, string messageAddress, CerasSerializer serializer)
        {
            MessageAddress = messageAddress;
            Serializer = serializer;
            NetMQClient = netMQClient;
            NetMQClient.ByteMessage.PropertyChanged += OnMessageReceived;
            Amount = 0.0;
        }

        public void OnMessageReceived(object sender, PropertyChangedEventArgs e)
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

        public string MessageAddress { get; set; }
        public NetMQClient NetMQClient { get; set; }
        public CerasSerializer Serializer { get; set; }

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