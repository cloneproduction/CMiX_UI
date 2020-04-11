using System;
using System.Windows;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class TranslateModifier : ViewModel, ISendable, IUndoable
    {
        public TranslateModifier(string messageaddress, MessageService messageService, Mementor mementor, Beat beat) 
        {
            MessageAddress = $"{messageaddress}{nameof(TranslateModifier)}/";
            MessageService = messageService;

            Translate = new AnimParameter(MessageAddress + nameof(Translate), messageService, mementor, beat, true);
            TranslateX = new AnimParameter(MessageAddress + nameof(TranslateX), messageService, mementor, beat, false);
            TranslateY = new AnimParameter(MessageAddress + nameof(TranslateY), messageService, mementor, beat, false);
            TranslateZ = new AnimParameter(MessageAddress + nameof(TranslateZ), messageService, mementor, beat, false);
        }

        #region PROPERTIES
        public AnimParameter Translate { get; set; }
        public AnimParameter TranslateX { get; set; }
        public AnimParameter TranslateY { get; set; }
        public AnimParameter TranslateZ { get; set; }
        public string MessageAddress { get; set; }
        public MessageService MessageService { get; set; }
        public Mementor Mementor { get; set; }
        #endregion

        #region COPY/PASTE/RESET
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
                Mementor.BeginBatch();
                MessageService.Disable();

                var translatemodifiermodel = data.GetData("TranslateModifierModel") as TranslateModifierModel;
                var messageaddress = MessageAddress;
                this.Paste(translatemodifiermodel);

                MessageService.Enable();
                Mementor.EndBatch();
                //SendMessages(MessageAddress, GetModel());
                //QueueObjects(translatemodifiermodel);
                //SendQueues();
            }
        }

        public void ResetGeometry()
        {
            this.Reset();
            //SendMessages(MessageAddress, GetModel());
        }

        //public TranslateModifierModel GetModel()
        //{
        //    TranslateModifierModel translateModifierModel = new TranslateModifierModel();

        //    return translateModifierModel;
        //}

        public void Paste(TranslateModifierModel translatemodifiermodel)
        {
            MessageService.Disable();

            Translate.Paste(translatemodifiermodel.Translate);
            TranslateX.Paste(translatemodifiermodel.TranslateX);
            TranslateY.Paste(translatemodifiermodel.TranslateY);
            TranslateZ.Paste(translatemodifiermodel.TranslateZ);

            MessageService.Enable();
        }

        public void Reset()
        {
            MessageService.Disable();

            Translate.Reset();
            TranslateX.Reset();
            TranslateY.Reset();
            TranslateZ.Reset();

            MessageService.Enable();

            //SendMessages(MessageAddress, GetModel());
            //QueueObjects(translatemodifiermodel);
            //SendQueues();
        }
        #endregion
    }
}
