using System;
using CMiX.MVVM;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.Engine.ViewModels
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
            Receiver.UpdateViewModel(MessageAddress, this);
        }

        public Slider FOV { get; set; }
        public Slider Zoom { get; set; }
        public string MessageAddress { get; set; }
        public Receiver Receiver { get; set; }

        public void PasteModel(CameraModel cameraModel)
        {
            FOV.PasteModel(cameraModel.FOV);
            Zoom.PasteModel(cameraModel.Zoom);
        }

        public void CopyModel(CameraModel cameraModel)
        {
            throw new NotImplementedException();
        }
    }
}