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
        public TranslateModifier(string messageaddress, Messenger messenger, Mementor mementor, Beat beat) 
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(TranslateModifier));

            Translate = new AnimParameter(MessageAddress + nameof(Translate), messenger, mementor, beat, true);
            TranslateX = new AnimParameter(MessageAddress + nameof(TranslateX), messenger, mementor, beat, false);
            TranslateY = new AnimParameter(MessageAddress + nameof(TranslateY), messenger, mementor, beat, false);
            TranslateZ = new AnimParameter(MessageAddress + nameof(TranslateZ), messenger, mementor, beat, false);
        }

        #region PROPERTIES
        public AnimParameter Translate { get; set; }
        public AnimParameter TranslateX { get; set; }
        public AnimParameter TranslateY { get; set; }
        public AnimParameter TranslateZ { get; set; }
        public string MessageAddress { get; set; }
        public Messenger Messenger { get; set; }
        public Mementor Mementor { get; set; }
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
                Messenger.Disable();

                var translatemodifiermodel = data.GetData("TranslateModifierModel") as TranslateModifierModel;
                var messageaddress = MessageAddress;
                this.Paste(translatemodifiermodel);
                UpdateMessageAddress(messageaddress);
                this.Copy(translatemodifiermodel);

                Messenger.Enable();
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
            Messenger.Disable();

            MessageAddress = translatemodifiermodel.MessageAddress;

            Translate.Paste(translatemodifiermodel.Translate);
            TranslateX.Paste(translatemodifiermodel.TranslateX);
            TranslateY.Paste(translatemodifiermodel.TranslateY);
            TranslateZ.Paste(translatemodifiermodel.TranslateZ);

            Messenger.Enable();
        }

        public void Reset()
        {
            Messenger.Disable();

            Translate.Reset();
            TranslateX.Reset();
            TranslateY.Reset();
            TranslateZ.Reset();

            Messenger.Enable();

            TranslateModifierModel translatemodifiermodel = new TranslateModifierModel();
            this.Copy(translatemodifiermodel);
            //SendMessages(MessageAddress, translatemodifiermodel);
            //QueueObjects(translatemodifiermodel);
            //SendQueues();
        }
        #endregion
    }
}
