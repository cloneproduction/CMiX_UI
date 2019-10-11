using System;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using Memento;

namespace CMiX.ViewModels
{
    public class Translate : ViewModel
    {
        public Translate(string messageaddress, ObservableCollection<OSCValidation> oscvalidation, Mementor mementor)
            : base(oscvalidation, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Translate));
            TranslateX = new Slider(MessageAddress + nameof(TranslateX), oscvalidation, mementor);
            TranslateY = new Slider(MessageAddress + nameof(TranslateY), oscvalidation, mementor);
            TranslateZ = new Slider(MessageAddress + nameof(TranslateZ), oscvalidation, mementor);
        }

        public Slider TranslateX { get; set; }
        public Slider TranslateY { get; set; }
        public Slider TranslateZ { get; set; }
    }
}
