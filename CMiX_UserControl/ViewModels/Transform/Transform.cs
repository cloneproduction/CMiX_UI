using System;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using Memento;

namespace CMiX.ViewModels
{
    public class Transform : ViewModel
    {
        public Transform(string messageaddress, ObservableCollection<OSCValidation> oscvalidation, Mementor mementor)
            : base(oscvalidation, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Transform));
            Translate = new Translate(MessageAddress, oscvalidation, mementor);
            Scale = new Scale(MessageAddress, oscvalidation, mementor);
            Rotation = new Rotation(MessageAddress, oscvalidation, mementor);
        }

        public Translate Translate { get; }
        public Scale Scale { get; }
        public Rotation Rotation { get; }
    }
}