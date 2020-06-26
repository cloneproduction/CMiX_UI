using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System;

namespace CMiX.MVVM.ViewModels
{
    [Serializable]
    public class ScaleModifier : Sendable
    {
        public ScaleModifier(Beat beat)
        {
            Scale = new AnimParameter(nameof(Scale), beat, true, this);
            ScaleX = new AnimParameter(nameof(ScaleX), beat, false, this);
            ScaleY = new AnimParameter(nameof(ScaleY), beat, false, this);
            ScaleZ = new AnimParameter(nameof(ScaleZ), beat, false, this);
        }

        public ScaleModifier(Beat beat, Sendable parentSendable) : this(beat)
        {
            SubscribeToEvent(parentSendable);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as ScaleModifierModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        public AnimParameter Scale { get; set; }
        public AnimParameter ScaleX { get; set; }
        public AnimParameter ScaleY { get; set; }
        public AnimParameter ScaleZ { get; set; }


        //public void CopyGeometry()
        //{
        //    IDataObject data = new DataObject();
        //    data.SetData("ScaleModifierModel", this.GetModel(), false);
        //    Clipboard.SetDataObject(data);
        //}

        //public void PasteGeometry()
        //{
        //    IDataObject data = Clipboard.GetDataObject();
        //    if (data.GetDataPresent("ScaleModifierModel"))
        //    {
        //        var Scalemodifiermodel = data.GetData("ScaleModifierModel") as ScaleModifierModel;
        //    }
        //}
    }
}