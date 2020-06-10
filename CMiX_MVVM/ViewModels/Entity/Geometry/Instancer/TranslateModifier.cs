using System.Windows;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class TranslateModifier : Sendable
    {
        public TranslateModifier(Beat beat) 
        {
            Translate = new AnimParameter(nameof(Translate), beat, true);
            TranslateX = new AnimParameter(nameof(TranslateX), beat, false);
            TranslateY = new AnimParameter(nameof(TranslateY), beat, false);
            TranslateZ = new AnimParameter(nameof(TranslateZ), beat, false);
        }


        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as TranslateModifierModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }


        public AnimParameter Translate { get; set; }
        public AnimParameter TranslateX { get; set; }
        public AnimParameter TranslateY { get; set; }
        public AnimParameter TranslateZ { get; set; }


        public void CopyGeometry()
        {
            IDataObject data = new DataObject();
            data.SetData(nameof(TranslateModifierModel), this.GetModel(), false);
            Clipboard.SetDataObject(data);
        }


        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent(nameof(TranslateModifierModel)))
            {
                var translatemodifiermodel = data.GetData(nameof(TranslateModifierModel)) as TranslateModifierModel;
                this.SetViewModel(translatemodifiermodel);
            }
        }
    }
}
