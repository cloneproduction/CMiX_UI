using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.Services.Message;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels
{
    public class Geometry : Sender, ITransform, IHandler
    {
        public Geometry(MasterBeat beat) 
        {
            Handlers = new List<IHandler>();
            Instancer = new Instancer(beat, this);
            Transform = new Transform(this);
            GeometryFX = new GeometryFX();
            AssetPathSelector = new AssetPathSelector(new AssetGeometry(), this);
        }

        public Geometry(MasterBeat beat, Sender parent) : this(beat)
        {
            //((IHandler)parent).Handlers.Add(this);
            SubscribeToEvent(parent);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            System.Console.WriteLine("Geometry Received Parent Change");
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as GeometryModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        public void HandleMessage(MessageReceived message, string parentMessageAddress)
        {
            System.Console.WriteLine("Geometry Handle Message");
        }

        public AssetPathSelector AssetPathSelector { get; set; }
        public Transform Transform { get; set; }
        public Instancer Instancer { get; set; }
        public GeometryFX GeometryFX { get; set; }
        public List<IHandler> Handlers { get; set; }
    }
}