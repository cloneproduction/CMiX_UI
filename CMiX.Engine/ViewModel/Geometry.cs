using CMiX.MVVM;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System;

namespace CMiX.Engine.ViewModels
{
    public class Geometry : IMessageReceiver, ICopyPasteModel
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
