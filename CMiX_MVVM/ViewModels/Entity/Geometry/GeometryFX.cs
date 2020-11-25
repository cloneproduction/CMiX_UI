using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class GeometryFX : Sender
    {
        public GeometryFX(string name, IColleague parentSender) : base (name, parentSender)
        {
            Explode = new Slider(nameof(Explode), this);
        }

        public override void Receive(Message message)
        {
            this.SetViewModel(message.Obj as GeometryFXModel);
        }
        public Slider Explode { get; set; }
    }
}