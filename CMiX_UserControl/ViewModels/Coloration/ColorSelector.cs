using CMiX.Services;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

namespace CMiX.ViewModels
{
    public class ColorSelector : ViewModel
    {
        public ColorSelector(string messageaddress, ObservableCollection<OSCValidation> oscvalidation, Mementor mementor) 
            : base(oscvalidation, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(ColorSelector));
            ColorPicker = new ColorPicker.ViewModels.ColorPicker(MessageAddress, oscvalidation, mementor);

            CopyColorSelectorCommand = new RelayCommand(p => CopyColorSelector());
            PasteColorSelectorCommand = new RelayCommand(p => PasteColorSelector());
            ResetColorSelectorCommand = new RelayCommand(p => ResetColorSelector());
        }

        #region PROPERTIES
        public ICommand CopyColorSelectorCommand { get; }
        public ICommand PasteColorSelectorCommand { get; }
        public ICommand ResetColorSelectorCommand { get; }
        public ColorPicker.ViewModels.ColorPicker ColorPicker { get; set; }
        #endregion

        public void CopyColorSelector()
        {
            ColorSelectorModel colorselectormodel = new ColorSelectorModel();
            this.Copy(colorselectormodel);
            IDataObject data = new DataObject();
            data.SetData("ColorSelectorModel", colorselectormodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteColorSelector()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("ColorSelectorModel"))
            {
                Mementor.BeginBatch();
                var colorselectormodel = data.GetData("ColorSelectorModel") as ColorSelectorModel;
                this.Paste(colorselectormodel);
                Mementor.EndBatch();
            }
        }

        public void ResetColorSelector()
        {
            ColorSelectorModel colorselectormodel = new ColorSelectorModel();
            this.Reset();
            this.Copy(colorselectormodel);
            QueueObjects(colorselectormodel);
            SendQueues();
        }

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
