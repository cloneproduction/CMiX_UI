using CMiX.MVVM;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System;

namespace CMiX.Engine.ViewModels
{
    public class Entity : IMessageReceiver, ICopyPasteModel
    {
        public Entity(Receiver receiver, string messageAddress)
        {
            Receiver = receiver;
            Receiver.MessageReceived += OnMessageReceived;
            MessageAddress = $"{messageAddress}/";
            
            Geometry = new Geometry(receiver, MessageAddress);
            Texture = new Texture(receiver, MessageAddress);
            Coloration = new Coloration(receiver, MessageAddress);
        }

        public void OnMessageReceived(object sender, EventArgs e)
        {
            Receiver.UpdateViewModel(MessageAddress, this);
        }

        public Receiver Receiver { get; set; }
        public string MessageAddress { get; set; }
        public string Name { get; set; }
        public Geometry Geometry { get; set; }
        public Texture Texture { get; set; }
        public Coloration Coloration { get; set; }
        
        public void PasteModel(IModel model)
        {
            EntityModel entityModel = model as EntityModel;
            MessageAddress = entityModel.MessageAddress;
            this.Name = entityModel.Name;
            Geometry.PasteModel(entityModel.GeometryModel);
            Texture.PasteModel(entityModel.TextureModel);
            Coloration.PasteModel(entityModel.ColorationModel);
        }

        public void CopyModel(IModel model)
        {
            throw new NotImplementedException();
        }
    }
}