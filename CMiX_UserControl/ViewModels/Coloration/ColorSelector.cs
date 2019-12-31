using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Services;

namespace CMiX.ViewModels
{
    public class ColorSelector : ViewModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public ColorSelector(string messageaddress, Messenger messenger, Mementor mementor) 
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(ColorSelector));
            Messenger = messenger;
            Mementor = mementor;
            ColorPicker = new ColorPicker.ViewModels.ColorPicker(MessageAddress, messenger, mementor);

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
        public string MessageAddress { get; set; }
        public Messenger Messenger { get; set; }
        public Mementor Mementor { get; set; }
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
                Messenger.Disable();

                var colorselectormodel = data.GetData("ColorSelectorModel") as ColorSelectorModel;
                var colorselectormessageaddress = MessageAddress;
                this.Paste(colorselectormodel);
                this.UpdateMessageAddress(colorselectormessageaddress);
                this.Copy(colorselectormodel);

                Messenger.Enable();
                this.Mementor.EndBatch();
                //SendMessages(MessageAddress, colorselectormodel);
            }
        }

        public void ResetColorSelector()
        {
            ColorSelectorModel colorselectormodel = new ColorSelectorModel();
            this.Reset();
            this.Copy(colorselectormodel);
            //SendMessages(MessageAddress, colorselectormodel);
        }

        public void Copy(ColorSelectorModel colorselectormodel)
        {
            colorselectormodel.MessageAddress = MessageAddress;
            ColorPicker.Copy(colorselectormodel.ColorPickerModel);
        }

        public void Paste(ColorSelectorModel colorselectormodel)
        {
            Messenger.Disable();

            MessageAddress = colorselectormodel.MessageAddress;
            ColorPicker.Paste(colorselectormodel.ColorPickerModel);

            Messenger.Enable();
        }

        public void Reset()
        {
            Messenger.Disable();

            ColorPicker.Reset();

            Messenger.Enable();
        }
        #endregion
    }
}
