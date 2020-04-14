using System;
using System.Windows;
using Memento;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;

namespace CMiX.Studio.ViewModels
{
    public class Translate : ViewModel, ISendable, IUndoable
    {
        public Translate(string messageAddress, MessageService messageService, Mementor mementor)
        {
            MessageAddress = $"{messageAddress}{nameof(Translate)}/";
            MessageService = messageService;

            X = new Slider(MessageAddress + nameof(X), messageService, mementor);
            X.Minimum = -1;
            X.Maximum = 1;
            Y = new Slider(MessageAddress + nameof(Y), messageService, mementor);
            Y.Minimum = -1;
            Y.Maximum = 1;
            Z = new Slider(MessageAddress + nameof(Z), messageService, mementor);
            Z.Minimum = -1;
            Z.Maximum = 1;
        }

        public Slider X { get; set; }
        public Slider Y { get; set; }
        public Slider Z { get; set; }
        public string MessageAddress { get; set; }

        public Mementor Mementor { get; set; }
        public MessageService MessageService { get; set; }

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
                Mementor.BeginBatch();
                MessageService.Disable();

                var translatemodel = data.GetData("TranslateModel") as TranslateModel;
                this.Paste(translatemodel);

                MessageService.Enable();
                Mementor.EndBatch();
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
            MessageService.Disable();

            X.SetViewModel(translatemodel.X);
            Y.SetViewModel(translatemodel.Y);
            Z.SetViewModel(translatemodel.Z);

            MessageService.Enable();
        }

        public void Reset()
        {
            MessageService.Disable();
            //Mementor.BeginBatch();

            X.Reset();
            Y.Reset();
            Z.Reset();

            //Mementor.EndBatch();
            MessageService.Enable();

            //this.SendMessages(nameof(TranslateModel), GetModel());
        }
        #endregion
    }
}
