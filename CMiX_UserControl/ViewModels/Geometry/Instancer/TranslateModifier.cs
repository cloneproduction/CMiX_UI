using System;
using System.Windows;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Services;

namespace CMiX.ViewModels
{
    public class TranslateModifier : ViewModel, ISendable, IUndoable
    {
        public TranslateModifier(string messageaddress, MessageService messageService, Mementor mementor, Beat beat) 
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(TranslateModifier));

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
        public string MessageAddress { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public MessageService MessageService { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Mementor Mementor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;

            Translate.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Translate)));
            TranslateX.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(TranslateX)));
            TranslateY.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(TranslateY)));
            TranslateZ.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(TranslateZ)));
        }
        #endregion

        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            TranslateModifierModel translatemodifiermodel = new TranslateModifierModel();
            this.Copy(translatemodifiermodel);
            IDataObject data = new DataObject();
            data.SetData("TranslateModifierModel", translatemodifiermodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("TranslateModifierModel"))
            {
                Mementor.BeginBatch();
                MessageService.DisabledMessages();

                var translatemodifiermodel = data.GetData("TranslateModifierModel") as TranslateModifierModel;
                var messageaddress = MessageAddress;
                this.Paste(translatemodifiermodel);
                UpdateMessageAddress(messageaddress);
                this.Copy(translatemodifiermodel);

                MessageService.EnabledMessages();
                Mementor.EndBatch();
                //SendMessages(MessageAddress, translatemodifiermodel);
                //QueueObjects(translatemodifiermodel);
                //SendQueues();
            }
        }

        public void ResetGeometry()
        {
            TranslateModifierModel translatemodifiermodel = new TranslateModifierModel();
            this.Reset();
            this.Copy(translatemodifiermodel);
            //SendMessages(MessageAddress, translatemodifiermodel);
        }

        public void Copy(TranslateModifierModel translatemodifiermodel)
        {
            translatemodifiermodel.MessageAddress = MessageAddress;
            Translate.Copy(translatemodifiermodel.Translate);
            TranslateX.Copy(translatemodifiermodel.TranslateX);
            TranslateY.Copy(translatemodifiermodel.TranslateY);
            TranslateZ.Copy(translatemodifiermodel.TranslateZ);
        }

        public void Paste(TranslateModifierModel translatemodifiermodel)
        {
            MessageService.DisabledMessages();

            MessageAddress = translatemodifiermodel.MessageAddress;

            Translate.Paste(translatemodifiermodel.Translate);
            TranslateX.Paste(translatemodifiermodel.TranslateX);
            TranslateY.Paste(translatemodifiermodel.TranslateY);
            TranslateZ.Paste(translatemodifiermodel.TranslateZ);

            MessageService.EnabledMessages();
        }

        public void Reset()
        {
            MessageService.DisabledMessages();

            Translate.Reset();
            TranslateX.Reset();
            TranslateY.Reset();
            TranslateZ.Reset();

            MessageService.EnabledMessages();

            TranslateModifierModel translatemodifiermodel = new TranslateModifierModel();
            this.Copy(translatemodifiermodel);
            //SendMessages(MessageAddress, translatemodifiermodel);
            //QueueObjects(translatemodifiermodel);
            //SendQueues();
        }
        #endregion
    }
}
