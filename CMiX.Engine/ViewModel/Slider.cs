using System;
using CMiX.MVVM.Message;
using Ceras;
using CMiX.MVVM.Models;

namespace CMiX.Engine.ViewModel
{
    public class Slider : ViewModel
    {
        public Slider(NetMQClient netMQClient, string topic, CerasSerializer serializer)
            : base(netMQClient, topic, serializer)
        {
            Amount = 0.0;
        }

        public override void ByteReceived()
        {
            base.ByteReceived();
            var slider = Serializer.Deserialize<SliderModel>(NetMQClient.ByteMessage.Message);
            this.Amount = slider.Amount;
        }

        private double _amount;
        public double Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
    }
}