using CMiX.MVVM;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System;

namespace CMiX.Engine.ViewModels
{
    public class Geometry : IMessageReceiver, ICopyPasteModel<GeometryModel>
    {
        public Geometry(Receiver receiver, string messageAddress)
        {
            MessageAddress = $"{messageAddress}{nameof(Geometry)}/";
            Receiver = receiver;
            Receiver.MessageReceived += OnMessageReceived;
        }

        public string MessageAddress { get; set; }
        public Receiver Receiver { get; set; }

        public void OnMessageReceived(object sender, EventArgs e)
        {
            Receiver.UpdateViewModel(MessageAddress, this);
        }

        public void PasteModel(GeometryModel geometryModel)
        {

        }

        public void CopyModel(GeometryModel geometryModel)
        {
            throw new NotImplementedException();
        }
    }
}
