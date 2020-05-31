using System.Windows;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class Scale : Sendable
    {
        public Scale() 
        {
            X = new Slider(nameof(X), this);
            Y = new Slider(nameof(Y), this);
            Z = new Slider(nameof(Z), this);
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


        #region COPY/PASTE/RESET
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
                this.Paste(scalemodel);
                //Mementor.EndBatch();
                //SendMessages(nameof(ScaleModel), GetModel());
            }
        }

        public void ResetGeometry()
        {
            ScaleModel scalemodel = this.GetModel();
            this.Reset();
            //SendMessages(nameof(ScaleModel), scalemodel);
        }

        public void Paste(ScaleModel scalemodel)
        {
            X.SetViewModel(scalemodel.X);
            Y.SetViewModel(scalemodel.Y);
            Z.SetViewModel(scalemodel.Z);
        }

        public void Reset()
        {
            //Mementor.BeginBatch();

            X.Reset();
            Y.Reset();
            Z.Reset();

            //Mementor.EndBatch();

            ScaleModel scalemodel = this.GetModel();
            //SendMessages(nameof(ScaleModel), scalemodel);
        }


        #endregion
    }
}
