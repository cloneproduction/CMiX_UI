using System.Windows.Input;
using System.Windows;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;

namespace CMiX.Studio.ViewModels
{
    public class ColorSelector : ViewModel
    {
        #region CONSTRUCTORS
        public ColorSelector() 
        {
            ColorPicker = new ColorPicker.ViewModels.ColorPicker();

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
            ColorPicker.Reset();
        }
        #endregion
    }
}
