using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System.Collections.Generic;

namespace CMiX.MVVM.ViewModels
{
    public class Geometry : Sender, ITransform
    {
        public Geometry(MasterBeat beat) 
        {
            Instancer = new Instancer(beat, this);
            Transform = new Transform(this);
            GeometryFX = new GeometryFX();
            AssetPathSelector = new AssetPathSelector(new AssetGeometry(), this);
        }

        public Geometry(MasterBeat beat, Sender parent) : this(beat)
        {
            SubscribeToEvent(parent);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as GeometryModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }


        public AssetPathSelector AssetPathSelector { get; set; }
        public Transform Transform { get; set; }
        public Instancer Instancer { get; set; }
        public GeometryFX GeometryFX { get; set; }
    }
}