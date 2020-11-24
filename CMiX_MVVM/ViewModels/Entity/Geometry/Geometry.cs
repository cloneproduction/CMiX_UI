using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class Geometry : Sender, IColleague, ITransform
    {
        public Geometry(string name, IColleague parentSender, MasterBeat beat) 
        {
            this.Address = $"{parentSender.Address}{name}/";
            this.MessageMediator = parentSender.MessageMediator;
            this.MessageMediator.RegisterColleague(this);

            Instancer = new Instancer(nameof(Instancer), this, beat);
            Transform = new Transform(nameof(Transform), this);
            GeometryFX = new GeometryFX();
            AssetPathSelector = new AssetPathSelector(new AssetGeometry(), this);
        }

        public AssetPathSelector AssetPathSelector { get; set; }
        public Transform Transform { get; set; }
        public Instancer Instancer { get; set; }
        public GeometryFX GeometryFX { get; set; }
        public MessageMediator MessageMediator { get; set; }

        public void Send(Message message)
        {
            MessageMediator?.Notify(MessageDirection.OUT, message);
        }

        public void Receive(Message message)
        {
            this.SetViewModel(message.Obj as GeometryModel);
            System.Console.WriteLine("POUETPOUET " + this.Address + "Geometry received " + message.Address);
        }
    }
}