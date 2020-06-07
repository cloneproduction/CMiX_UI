using System.Windows;
using CMiX.MVVM.Models;

namespace CMiX.MVVM.ViewModels
{
    public class TranslateModifier : ViewModel
    {
        public TranslateModifier(Beat beat) 
        {
            Translate = new AnimParameter(nameof(Translate), beat, true);
            TranslateX = new AnimParameter(nameof(TranslateX), beat, false);
            TranslateY = new AnimParameter(nameof(TranslateY), beat, false);
            TranslateZ = new AnimParameter(nameof(TranslateZ), beat, false);
        }


        public AnimParameter Translate { get; set; }
        public AnimParameter TranslateX { get; set; }
        public AnimParameter TranslateY { get; set; }
        public AnimParameter TranslateZ { get; set; }


        public void CopyGeometry()
        {
            IDataObject data = new DataObject();
            data.SetData("TranslateModifierModel", this.GetModel(), false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("TranslateModifierModel"))
            {
                var translatemodifiermodel = data.GetData("TranslateModifierModel") as TranslateModifierModel;
                this.SetViewModel(translatemodifiermodel);

            }
        }
    }
}
