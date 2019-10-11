using System;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using Memento;

namespace CMiX.ViewModels
{
    public class Rotation : ViewModel
    {
        public Rotation(string messageaddress, ObservableCollection<OSCValidation> oscvalidation, Mementor mementor)
            : base(oscvalidation, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Rotation));
            RotationX = new Slider(MessageAddress + nameof(RotationX), oscvalidation, mementor);
            RotationY = new Slider(MessageAddress + nameof(RotationY), oscvalidation, mementor);
            RotationZ = new Slider(MessageAddress + nameof(RotationZ), oscvalidation, mementor);
        }

        public Slider RotationX { get; set; }
        public Slider RotationY { get; set; }
        public Slider RotationZ { get; set; }
    }
}