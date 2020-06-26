using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class RotationModifier : Sendable
    {
        public RotationModifier(Beat beat)
        {
            Rotation = new AnimParameter(nameof(Rotation), beat, true, this);
            RotationX = new AnimParameter(nameof(RotationX), beat, false, this);
            RotationY = new AnimParameter(nameof(RotationY), beat, false, this);
            RotationZ = new AnimParameter(nameof(RotationZ), beat, false, this);
        }

        public RotationModifier(Beat beat, Sendable parentSendable) : this(beat)
        {
            SubscribeToEvent(parentSendable);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as RotationModifierModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        public AnimParameter Rotation { get; set; }
        public AnimParameter RotationX { get; set; }
        public AnimParameter RotationY { get; set; }
        public AnimParameter RotationZ { get; set; }


        //public void CopyGeometry()
        //{
        //    IDataObject data = new DataObject();
        //    data.SetData("RotationModifierModel", this.GetModel(), false);
        //    Clipboard.SetDataObject(data);
        //}

        //public void PasteGeometry()
        //{
        //    IDataObject data = Clipboard.GetDataObject();
        //    if (data.GetDataPresent("RotationModifierModel"))
        //    {
        //        var Rotationmodifiermodel = data.GetData("RotationModifierModel") as RotationModifierModel;
        //    }
        //}
    }
}
