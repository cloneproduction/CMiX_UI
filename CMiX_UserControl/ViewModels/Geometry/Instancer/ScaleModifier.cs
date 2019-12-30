using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using Memento;
using System;
using System.Windows;

namespace CMiX.ViewModels
{
    [Serializable]
    public class ScaleModifier : ViewModel, ISendable, IUndoable
    {
        public ScaleModifier(string messageaddress, MessageService messageService, Mementor mementor, Beat beat)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(ScaleModifier));

            Scale = new AnimParameter(MessageAddress + nameof(Scale), messageService, mementor, beat, true);
            ScaleX = new AnimParameter(MessageAddress + nameof(ScaleX), messageService, mementor, beat, false);
            ScaleY = new AnimParameter(MessageAddress + nameof(ScaleY), messageService, mementor, beat, false);
            ScaleZ = new AnimParameter(MessageAddress + nameof(ScaleZ), messageService, mementor, beat, false);
        }

        #region PROPERTIES
        public AnimParameter Scale { get; set; }
        public AnimParameter ScaleX { get; set; }
        public AnimParameter ScaleY { get; set; }
        public AnimParameter ScaleZ { get; set; }
        public string MessageAddress { get; set; }
        public MessageService MessageService { get; set; }
        public Mementor Mementor { get; set; }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;

            Scale.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Scale)));
            ScaleX.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(ScaleX)));
            ScaleY.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(ScaleY)));
            ScaleZ.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(ScaleZ)));
        }
        #endregion

        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            ScaleModifierModel Scalemodifiermodel = new ScaleModifierModel();
            this.Copy(Scalemodifiermodel);
            IDataObject data = new DataObject();
            data.SetData("ScaleModifierModel", Scalemodifiermodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("ScaleModifierModel"))
            {
                Mementor.BeginBatch();
                MessageService.DisabledMessages();

                var Scalemodifiermodel = data.GetData("ScaleModifierModel") as ScaleModifierModel;
                var messageaddress = MessageAddress;
                this.Paste(Scalemodifiermodel);
                UpdateMessageAddress(messageaddress);
                this.Copy(Scalemodifiermodel);

                MessageService.EnabledMessages();
                Mementor.EndBatch();
                //SendMessages(MessageAddress, Scalemodifiermodel);
                //QueueObjects(Scalemodifiermodel);
                //SendQueues();
            }
        }

        public void ResetGeometry()
        {
            ScaleModifierModel Scalemodifiermodel = new ScaleModifierModel();
            this.Reset();
            this.Copy(Scalemodifiermodel);
            //SendMessages(MessageAddress, Scalemodifiermodel);
            //QueueObjects(Scalemodifiermodel);
            //SendQueues();
        }

        public void Copy(ScaleModifierModel Scalemodifiermodel)
        {
            Scalemodifiermodel.MessageAddress = MessageAddress;
            Scale.Copy(Scalemodifiermodel.Scale);
            ScaleX.Copy(Scalemodifiermodel.ScaleX);
            ScaleY.Copy(Scalemodifiermodel.ScaleY);
            ScaleZ.Copy(Scalemodifiermodel.ScaleZ);
        }

        public void Paste(ScaleModifierModel Scalemodifiermodel)
        {
            MessageService.DisabledMessages();

            MessageAddress = Scalemodifiermodel.MessageAddress;

            Scale.Paste(Scalemodifiermodel.Scale);
            ScaleX.Paste(Scalemodifiermodel.ScaleX);
            ScaleY.Paste(Scalemodifiermodel.ScaleY);
            ScaleZ.Paste(Scalemodifiermodel.ScaleZ);

            MessageService.EnabledMessages();
        }

        public void Reset()
        {
            MessageService.DisabledMessages();

            Scale.Reset();
            ScaleX.Reset();
            ScaleY.Reset();
            ScaleZ.Reset();

            MessageService.EnabledMessages();

            ScaleModifierModel Scalemodifiermodel = new ScaleModifierModel();
            this.Copy(Scalemodifiermodel);
            //SendMessages(MessageAddress, Scalemodifiermodel);
            //QueueObjects(Scalemodifiermodel);
            //SendQueues();
        }
        #endregion
    }
}