using System;
using System.Windows;
using Memento;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;

namespace CMiX.Studio.ViewModels
{
    public class Translate : ViewModel
    {
        public Translate(string messageAddress)
        {
            MessageAddress = $"{messageAddress}{nameof(Translate)}/";

            X = new Slider(MessageAddress + nameof(X));
            X.Minimum = -1;
            X.Maximum = 1;
            Y = new Slider(MessageAddress + nameof(Y));
            Y.Minimum = -1;
            Y.Maximum = 1;
            Z = new Slider(MessageAddress + nameof(Z));
            Z.Minimum = -1;
            Z.Maximum = 1;
        }

        public Slider X { get; set; }
        public Slider Y { get; set; }
        public Slider Z { get; set; }
        public string MessageAddress { get; set; }

        public MessengerService MessengerService { get; set; }

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
                MessengerService.Disable();

                var translatemodel = data.GetData("TranslateModel") as TranslateModel;
                this.Paste(translatemodel);

                MessengerService.Enable();
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
