using System.Windows;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class Translate : Sendable
    {
        public Translate()
        {
            X = new Slider(nameof(X), this);
            Y = new Slider(nameof(Y), this);
            Z = new Slider(nameof(Z), this);
        }

        public Translate(Sendable parentSendable)
        {
            X = new Slider(nameof(X), this);
            Y = new Slider(nameof(Y), this);
            Z = new Slider(nameof(Z), this);

            SubscribeToEvent(parentSendable);
        }

        public Slider X { get; set; }
        public Slider Y { get; set; }
        public Slider Z { get; set; }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as TranslateModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            IDataObject data = new DataObject();
            data.SetData("TranslateModel", this.GetModel(), false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("TranslateModel"))
            {
                //Mementor.BeginBatch();
                var translatemodel = data.GetData("TranslateModel") as TranslateModel;
                this.Paste(translatemodel);
                //Mementor.EndBatch();
                //SendMessages(nameof(TranslateModel), GetModel());
            }
        }

        public void ResetGeometry()
        {
            
            TranslateModel translatemodel = this.GetModel();
            this.Reset();
            //this.SendMessages(nameof(TranslateModel), translatemodel);
        }


        public void Paste(TranslateModel translatemodel)
        {
            X.SetViewModel(translatemodel.X);
            Y.SetViewModel(translatemodel.Y);
            Z.SetViewModel(translatemodel.Z);
        }

        public void Reset()
        {
            //Mementor.BeginBatch();

            X.Reset();
            Y.Reset();
            Z.Reset();

            //Mementor.EndBatch();

            //this.SendMessages(nameof(TranslateModel), GetModel());
        }
        #endregion
    }
}
