using System.Windows.Input;
using System.Windows;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class ColorSelector : Sendable
    {
        public ColorSelector() 
        {
            ColorPicker = new ColorPicker(this);
        }

        public ColorSelector(Sendable parentSendable) : this()
        {
            SubscribeToEvent(parentSendable);
        }


        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as ColorSelectorModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        public ICommand CopyColorSelectorCommand { get; }
        public ICommand PasteColorSelectorCommand { get; }
        public ICommand ResetColorSelectorCommand { get; }
        public ColorPicker ColorPicker { get; set; }


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
                var colorselectormodel = data.GetData("ColorSelectorModel") as ColorSelectorModel;
                this.SetViewModel(colorselectormodel);
            }
        }

        public void ResetColorSelector()
        {
            this.Reset();
        }


        public void Reset()
        {
            //ColorPicker.Reset();
        }


        #endregion
    }
}
