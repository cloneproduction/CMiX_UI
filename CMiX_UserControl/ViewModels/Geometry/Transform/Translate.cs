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

            TranslateX = new Slider(MessageAddress + nameof(TranslateX), messageService, mementor);
            TranslateX.Minimum = -1;
            TranslateX.Maximum = 1;
            TranslateY = new Slider(MessageAddress + nameof(TranslateY), messageService, mementor);
            TranslateY.Minimum = -1;
            TranslateY.Maximum = 1;
            TranslateZ = new Slider(MessageAddress + nameof(TranslateZ), messageService, mementor);
            TranslateZ.Minimum = -1;
            TranslateZ.Maximum = 1;
        }

        public Slider TranslateX { get; set; }
        public Slider TranslateY { get; set; }
        public Slider TranslateZ { get; set; }
        public string MessageAddress { get; set; }

        public Mementor Mementor { get; set; }
        public MessageService MessageService { get; set; }

        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            IDataObject data = new DataObject();
            data.SetData("TranslateModel", GetModel(), false);
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

            TranslateX.SetViewModel(translatemodel.TranslateX);
            TranslateY.SetViewModel(translatemodel.TranslateY);
            TranslateZ.SetViewModel(translatemodel.TranslateZ);

            MessageService.Enable();
        }

        public void Reset()
        {
            MessageService.Disable();
            //Mementor.BeginBatch();

            TranslateX.Reset();
            TranslateY.Reset();
            TranslateZ.Reset();

            //Mementor.EndBatch();
            MessageService.Enable();

            //this.SendMessages(nameof(TranslateModel), GetModel());
        }
        #endregion
    }
}
