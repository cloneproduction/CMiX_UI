using System;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using Memento;

namespace CMiX.ViewModels
{
    public class Scale : ViewModel
    {
        public Scale(string messageaddress, ObservableCollection<OSCValidation> oscvalidation, Mementor mementor) 
            : base (oscvalidation, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Scale));
            ScaleX = new Slider(MessageAddress + nameof(ScaleX), oscvalidation, mementor);
            ScaleY = new Slider(MessageAddress + nameof(ScaleY), oscvalidation, mementor);
            ScaleZ = new Slider(MessageAddress + nameof(ScaleZ), oscvalidation, mementor);
        }

        public Slider ScaleX { get; set; }
        public Slider ScaleY { get; set; }
        public Slider ScaleZ { get; set; }
    }
}
