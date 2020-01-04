using System;
using System.ComponentModel;
using Ceras;
using CMiX.Engine.ViewModel;
using CMiX.MVVM;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Message;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.Engine.ViewModel
{
    public class Camera : ICopyPasteModel, IMessageReceiver
    {
        public Camera(Receiver receiver, string messageAddress)
        {
            MessageAddress = $"{messageAddress}{nameof(Camera)}/";
            FOV = new Slider(receiver, MessageAddress + nameof(FOV));
            Zoom = new Slider(receiver, MessageAddress + nameof(Zoom));
        }

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
            //                CameraModel cameraModel = NetMQClient.ByteMessage.Payload as CameraModel;
            //                this.PasteData(cameraModel);
            //            }
            //            break;
            //    }
            //}
        }

        public Slider FOV { get; set; }
        public Slider Zoom { get; set; }
        public string MessageAddress { get; set; }
        public Receiver Receiver { get; set; }

        public void PasteModel(IModel model)
        {
            CameraModel cameraModel = model as CameraModel;
            FOV.PasteModel(cameraModel.FOV);
            Zoom.PasteModel(cameraModel.Zoom);
        }

        public void CopyModel(IModel model)
        {
            throw new NotImplementedException();
        }
    }
}
