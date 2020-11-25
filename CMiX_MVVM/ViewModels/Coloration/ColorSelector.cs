using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;

namespace CMiX.MVVM.ViewModels
{
    public class ColorSelector : Sender
    {
        public ColorSelector(string name, IColleague parentSender) :base(name, parentSender) 
        {
            ColorPicker = new ColorPicker(nameof(ColorPicker), this);
        }

        public override void Receive(Message message)
        {
            this.SetViewModel(message.Obj as ColorSelectorModel);
        }

        public ColorPicker ColorPicker { get; set; }
    }
}
