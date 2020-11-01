using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class ColorSelector : Sender
    {
        public ColorSelector() 
        {
            ColorPicker = new ColorPicker(this);
        }

        public ColorSelector(Sender parentSender) : this()
        {
            SubscribeToEvent(parentSender);
        }


        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as ColorSelectorModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        public ColorPicker ColorPicker { get; set; }
    }
}
