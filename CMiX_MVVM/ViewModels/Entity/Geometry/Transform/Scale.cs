using System.Windows;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class Scale : Sendable
    {
        public Scale() 
        {
            X = new Slider(nameof(X), this);
            X.Amount = 1.0;
            Y = new Slider(nameof(Y), this);
            Y.Amount = 1.0;
            Z = new Slider(nameof(Z), this);
            Z.Amount = 1.0;
        }

        public Scale(Sendable parentSendable) : this()
        {
            SubscribeToEvent(parentSendable);
        }

        public Slider X { get; set; }
        public Slider Y { get; set; }
        public Slider Z { get; set; }


        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as ScaleModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        public void CopyGeometry()
        {
            IDataObject data = new DataObject();
            data.SetData("ScaleModel", this.GetModel(), false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("ScaleModel"))
            {
                var scalemodel = data.GetData("ScaleModel") as ScaleModel;
                this.SetViewModel(scalemodel);
                //Mementor.EndBatch();
                //SendMessages(nameof(ScaleModel), GetModel());
            }
        }
    }
}
