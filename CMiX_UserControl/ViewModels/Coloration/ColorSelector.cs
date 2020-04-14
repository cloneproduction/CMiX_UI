using System;
using System.Windows.Input;
using System.Windows;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class ColorSelector : ViewModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public ColorSelector(string messageaddress, MessageService messageService, Mementor mementor) 
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(ColorSelector));
            MessageService = messageService;
            Mementor = mementor;
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
        public string MessageAddress { get; set; }
        public MessageService MessageService { get; set; }
        public Mementor Mementor { get; set; }
        #endregion

        #region COPY/PASTE/RESET
        public void CopyColorSelector()
        {
            IDataObject data = new DataObject();
            data.SetData("ColorSelectorModel", this.GetModel(), false);
            Clipboard.SetDataObject(data);
        }

        public void PasteColorSelector()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("ColorSelectorModel"))
            {
                this.Mementor.BeginBatch();
                MessageService.Disable();

                var colorselectormodel = data.GetData("ColorSelectorModel") as ColorSelectorModel;
                var colorselectormessageaddress = MessageAddress;
                this.SetViewModel(colorselectormodel);
                MessageService.Enable();
                this.Mementor.EndBatch();
                //SendMessages(MessageAddress, GetModel());
            }
        }

        public void ResetColorSelector()
        {
            this.Reset();
            //SendMessages(MessageAddress, GetModel());
        }


        public void Reset()
        {
            MessageService.Disable();

            ColorPicker.Reset();

            MessageService.Enable();
        }
        #endregion
    }
}
