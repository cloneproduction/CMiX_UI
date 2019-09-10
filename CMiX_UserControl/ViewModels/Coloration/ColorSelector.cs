using CMiX.Services;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
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

        public ColorPicker.ViewModels.ColorPicker ColorPicker { get; set; }


        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;

            ColorPicker.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(ColorPicker)));
        }


        public void Copy(ColorSelectorModel colorselectormodel)
        {
            colorselectormodel.MessageAddress = MessageAddress;
            ColorPicker.Copy(colorselectormodel.ColorPickerModel);
        }

        public void Paste(ColorSelectorModel colorselectormodel)
        {
            DisabledMessages();

            MessageAddress = colorselectormodel.MessageAddress;
            ColorPicker.Paste(colorselectormodel.ColorPickerModel);

            EnabledMessages();
        }

        public void Reset()
        {
            DisabledMessages();

            ColorPicker.Reset();

            EnabledMessages();
        }
    }
}
