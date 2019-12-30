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
        public ColorSelector(string messageaddress, MessageService messageService, Mementor mementor) 
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(ColorSelector));
            MessageService = messageService;
            ColorPicker = new ColorPicker.ViewModels.ColorPicker(MessageAddress, messageService, mementor);

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
        public string MessageAddress { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public MessageService MessageService { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Mementor Mementor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
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
                MessageService.DisabledMessages();

                var colorselectormodel = data.GetData("ColorSelectorModel") as ColorSelectorModel;
                var colorselectormessageaddress = MessageAddress;
                this.Paste(colorselectormodel);
                this.UpdateMessageAddress(colorselectormessageaddress);
                this.Copy(colorselectormodel);

                MessageService.EnabledMessages();
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
            MessageService.DisabledMessages();

            MessageAddress = colorselectormodel.MessageAddress;
            ColorPicker.Paste(colorselectormodel.ColorPickerModel);

            MessageService.EnabledMessages();
        }

        public void Reset()
        {
            MessageService.DisabledMessages();

            ColorPicker.Reset();

            MessageService.EnabledMessages();
        }
        #endregion
    }
}
