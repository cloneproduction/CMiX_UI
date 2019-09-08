using CMiX.Services;
using CMiX.MVVM.ViewModels;
using Memento;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using CMiX.ColorPicker.ViewModels;

namespace CMiX.ViewModels
{
    public class ColorSelector : ViewModel
    {
        public ColorSelector(string messageaddress, ObservableCollection<OSCValidation> oscvalidation, Mementor mementor) 
            : base(oscvalidation, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(ColorSelector));
            ColorPicker = new ColorPicker.ViewModels.ColorPicker(MessageAddress, oscvalidation, mementor);
        }

        public ColorPicker.ViewModels.ColorPicker ColorPicker { get;  }
    }
}
