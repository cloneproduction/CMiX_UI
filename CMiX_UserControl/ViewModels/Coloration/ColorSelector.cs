﻿using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;

namespace CMiX.ViewModels
{
    public class ColorSelector : ViewModel
    {
        #region CONSTRUCTORS
        public ColorSelector(string messageaddress, ObservableCollection<OSCValidation> oscvalidation, Mementor mementor) 
            : base(oscvalidation, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(ColorSelector));
            ColorPicker = new ColorPicker.ViewModels.ColorPicker(MessageAddress, oscvalidation, mementor);

            CopyColorSelectorCommand = new RelayCommand(p => CopyColorSelector());
            PasteColorSelectorCommand = new RelayCommand(p => PasteColorSelector());
            ResetColorSelectorCommand = new RelayCommand(p => ResetColorSelector());
        }
        #endregion

        #region PROPERTIES
        public ICommand CopyColorSelectorCommand { get; }
        public ICommand PasteColorSelectorCommand { get; }
        public ICommand ResetColorSelectorCommand { get; }
        public ColorPicker.ViewModels.ColorPicker ColorPicker { get; set; }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;

            ColorPicker.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(ColorPicker)));
        }
        #endregion

        #region COPY/PASTE/RESET
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
                this.Mementor.BeginBatch();
                this.DisabledMessages();

                var colorselectormodel = data.GetData("ColorSelectorModel") as ColorSelectorModel;
                var colorselectormessageaddress = MessageAddress;
                this.Paste(colorselectormodel);
                this.UpdateMessageAddress(colorselectormessageaddress);
                this.Copy(colorselectormodel);
                this.EnabledMessages();
                this.Mementor.EndBatch();

                this.QueueObjects(colorselectormodel);
                this.SendQueues();
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
        #endregion
    }
}
