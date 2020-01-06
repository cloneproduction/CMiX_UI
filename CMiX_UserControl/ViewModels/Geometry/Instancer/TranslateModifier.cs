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
        public TranslateModifier(string messageaddress, Sender sender, Mementor mementor, Beat beat) 
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(TranslateModifier));

            Translate = new AnimParameter(MessageAddress + nameof(Translate), sender, mementor, beat, true);
            TranslateX = new AnimParameter(MessageAddress + nameof(TranslateX), sender, mementor, beat, false);
            TranslateY = new AnimParameter(MessageAddress + nameof(TranslateY), sender, mementor, beat, false);
            TranslateZ = new AnimParameter(MessageAddress + nameof(TranslateZ), sender, mementor, beat, false);
        }

        #region PROPERTIES
        public AnimParameter Translate { get; set; }
        public AnimParameter TranslateX { get; set; }
        public AnimParameter TranslateY { get; set; }
        public AnimParameter TranslateZ { get; set; }
        public string MessageAddress { get; set; }
        public Sender Sender { get; set; }
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
                Sender.Disable();

                var translatemodifiermodel = data.GetData("TranslateModifierModel") as TranslateModifierModel;
                var messageaddress = MessageAddress;
                this.Paste(translatemodifiermodel);
                UpdateMessageAddress(messageaddress);
                this.Copy(translatemodifiermodel);

                Sender.Enable();
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
            Sender.Disable();

            MessageAddress = translatemodifiermodel.MessageAddress;

            Translate.Paste(translatemodifiermodel.Translate);
            TranslateX.Paste(translatemodifiermodel.TranslateX);
            TranslateY.Paste(translatemodifiermodel.TranslateY);
            TranslateZ.Paste(translatemodifiermodel.TranslateZ);

            Sender.Enable();
        }

        public void Reset()
        {
            Sender.Disable();

            Translate.Reset();
            TranslateX.Reset();
            TranslateY.Reset();
            TranslateZ.Reset();

            Sender.Enable();

            TranslateModifierModel translatemodifiermodel = new TranslateModifierModel();
            this.Copy(translatemodifiermodel);
            //SendMessages(MessageAddress, translatemodifiermodel);
            //QueueObjects(translatemodifiermodel);
            //SendQueues();
        }
        #endregion
    }
}
