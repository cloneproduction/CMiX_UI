using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class Composition : Component
    {
        public Composition(int id, Beat beat) : base (id, beat)
        {
            Transition = new Slider("/Transition");
            Camera = new Camera(Beat);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as CompositionModel); 
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        public Camera Camera { get; set; }
        public Slider Transition { get; set; }
    }
}