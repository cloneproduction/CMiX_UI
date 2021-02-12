using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class Geometry : Sender, ITransform
    {
        public Geometry(string name, IMessageProcessor parentSender, MasterBeat beat) : base (name, parentSender)
        {
            Instancer = new Instancer(nameof(Instancer), this, beat);
            Transform = new Transform(nameof(Transform), this);
            GeometryFX = new GeometryFX(nameof(GeometryFX), this);
            AssetPathSelector = new AssetPathSelector(nameof(AssetPathSelector), this, new AssetGeometry());
        }

        public AssetPathSelector AssetPathSelector { get; set; }
        public Transform Transform { get; set; }
        public Instancer Instancer { get; set; }
        public GeometryFX GeometryFX { get; set; }

        public override void Receive(Message message)
        {
            this.SetViewModel(message.Obj as GeometryModel);
        }
    }
}