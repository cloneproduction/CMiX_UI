using Ceras;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Message;
using CMiX.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.Engine.ViewModel
{
    public class Geometry : ViewModel
    {
        public Geometry(NetMQClient netMQClient, string messageAddress, CerasSerializer serializer)
        : base(netMQClient, messageAddress, serializer)
        {
            //Geometry = new 
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
                            GeometryModel geometryModel = NetMQClient.ByteMessage.Payload as GeometryModel;
                            this.PasteData(geometryModel);
                            //System.Console.WriteLine(MessageAddress + " " + Mode);
                        }
                        break;
                }
            }
        }

        public void PasteData(GeometryModel geometryModel)
        {
            MessageAddress = geometryModel.MessageAddress;
        }
    }
}
