using CMiX.MVVM;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System;

namespace CMiX.Engine.ViewModel
{
    public class Geometry : IMessageReceiver, ICopyPasteModel
    {
        public Geometry(Receiver receiver, string messageAddress)
        {
            MessageAddress = $"{messageAddress}{nameof(Geometry)}/";
            Receiver = receiver;
        }

        public string MessageAddress { get; set; }
        public Receiver Receiver { get; set; }

        public void OnMessageReceived(object sender, EventArgs e)
        {
            //string receivedAddress = NetMQClient.ByteMessage.MessageAddress;

            //if (receivedAddress == this.MessageAddress)
            //{
            //    MessageCommand command = NetMQClient.ByteMessage.Command;
            //    switch (command)
            //    {
            //        case MessageCommand.VIEWMODEL_UPDATE:
            //            if (NetMQClient.ByteMessage.Payload != null)
            //            {
            //                GeometryModel geometryModel = NetMQClient.ByteMessage.Payload as GeometryModel;
            //                this.PasteData(geometryModel);
            //                //System.Console.WriteLine(MessageAddress + " " + Mode);
            //            }
            //            break;
            //    }
            //}
        }

        public void PasteModel(IModel model)
        {
            GeometryModel geometryModel = model as GeometryModel;
            MessageAddress = geometryModel.MessageAddress;
        }

        public void CopyModel(IModel model)
        {
            throw new NotImplementedException();
        }
    }
}
