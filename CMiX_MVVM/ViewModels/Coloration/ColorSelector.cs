using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;
using System.Windows.Media;

namespace CMiX.MVVM.ViewModels
{
    public class ColorSelector : Sender
    {
        public ColorSelector(string name, IColleague parentSender) :base(name, parentSender) 
        {
            ColorPicker = new ColorPicker(nameof(ColorPicker), this);
            this.SelectedColor = Colors.Red;
        }

        public override void Receive(Message message)
        {
            this.SetViewModel(message.Obj as ColorSelectorModel);
        }

        public ColorPicker ColorPicker { get; set; }

        private Color _selectedColor;
        public Color SelectedColor
        {
            get => _selectedColor;
            set
            {
                SetAndNotify(ref _selectedColor, value);
                this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.Address, this.GetModel()));
            }
        }
    }
}
