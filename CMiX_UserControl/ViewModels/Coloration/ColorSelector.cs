using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Services;
using CMiX.MVVM;

namespace CMiX.Studio.ViewModels
{
    public class ColorSelector : ViewModel, ICopyPasteModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public ColorSelector(string messageaddress, Sender sender, Mementor mementor) 
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(ColorSelector));
            Sender = sender;
            Mementor = mementor;
            ColorPicker = new ColorPicker.ViewModels.ColorPicker(MessageAddress, sender, mementor);

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
        public Sender Sender { get; set; }
        public Mementor Mementor { get; set; }
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
                Sender.Disable();

                var colorselectormodel = data.GetData("ColorSelectorModel") as ColorSelectorModel;
                var colorselectormessageaddress = MessageAddress;
                this.PasteModel(colorselectormodel);
                this.CopyModel(colorselectormodel);

                Sender.Enable();
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

        public void CopyModel(ColorSelectorModel colorSelectorModel)
        {
            ColorPicker.Copy(colorSelectorModel.ColorPickerModel);
        }

        public void PasteModel(ColorSelectorModel colorSelectorModel)
        {
            Sender.Disable();
            ColorPicker.Paste(colorSelectorModel.ColorPickerModel);
            Sender.Enable();
        }

        public void Reset()
        {
            Sender.Disable();

            ColorPicker.Reset();

            Sender.Enable();
        }
        #endregion
    }
}
