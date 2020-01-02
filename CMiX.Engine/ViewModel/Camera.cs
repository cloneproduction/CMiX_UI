using System;
using Ceras;
using CMiX.Engine.ViewModel;
using CMiX.MVVM.Commands;
using CMiX.MVVM.Message;
using CMiX.MVVM.Models;

namespace CMiX.Engine.ViewModel
{
    public class Camera : ViewModel
    {
        public Camera(NetMQClient netMQClient, string messageAddress, CerasSerializer serializer)
        : base(netMQClient, messageAddress, serializer)
        {
            MessageAddress = $"{messageAddress}{nameof(Camera)}/";
            FOV = new Slider(this.NetMQClient, MessageAddress + nameof(FOV), this.Serializer);
            Zoom = new Slider(this.NetMQClient, MessageAddress + nameof(Zoom), this.Serializer);
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
                            CameraModel cameraModel = NetMQClient.ByteMessage.Payload as CameraModel;
                            this.PasteData(cameraModel);
                        }
                        break;
                }
            }
        }

        public Slider FOV { get; set; }
        public Slider Zoom { get; set; }

        public void PasteData(CameraModel cameraModel)
        {
            FOV.PasteData(cameraModel.FOV);
            Zoom.PasteData(cameraModel.Zoom);
        }
    }
}
