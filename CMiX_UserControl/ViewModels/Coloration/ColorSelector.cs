using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Services;
using CMiX.MVVM;

namespace CMiX.ViewModels
{
    public class ColorSelector : ViewModel, ICopyPasteModel, ISendable, IUndoable
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
            this.CopyModel(colorselectormodel);
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
                this.PasteModel(colorselectormodel);
                this.UpdateMessageAddress(colorselectormessageaddress);
                this.CopyModel(colorselectormodel);

                Messenger.Enable();
                this.Mementor.EndBatch();
                //SendMessages(MessageAddress, colorselectormodel);
            }
        }

        public void ResetColorSelector()
        {
            ColorSelectorModel colorselectormodel = new ColorSelectorModel();
            this.Reset();
            this.CopyModel(colorselectormodel);
            //SendMessages(MessageAddress, colorselectormodel);
        }

        public void CopyModel(IModel model)
        {
            ColorSelectorModel colorSelectorModel = model as ColorSelectorModel;
            colorSelectorModel.MessageAddress = MessageAddress;
            ColorPicker.Copy(colorSelectorModel.ColorPickerModel);
        }

        public void PasteModel(IModel model)
        {
            Messenger.Disable();

            ColorSelectorModel colorSelectorModel = model as ColorSelectorModel;
            MessageAddress = colorSelectorModel.MessageAddress;
            ColorPicker.Paste(colorSelectorModel.ColorPickerModel);

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
