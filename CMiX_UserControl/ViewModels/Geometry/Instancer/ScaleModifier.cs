using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using Memento;
using System;
using System.Windows;

namespace CMiX.Studio.ViewModels
{
    [Serializable]
    public class ScaleModifier : ViewModel, ISendable, IUndoable
    {
        public ScaleModifier(string messageaddress, MessageService messageService, Mementor mementor, Beat beat)
        {
            MessageAddress = $"{messageaddress}{nameof(ScaleModifier)}/";
            MessageService = messageService;

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
                MessageService.Disable();

                var Scalemodifiermodel = data.GetData("ScaleModifierModel") as ScaleModifierModel;
                var messageaddress = MessageAddress;
                this.Paste(Scalemodifiermodel);
                //UpdateMessageAddress(messageaddress);
                this.Copy(Scalemodifiermodel);

                MessageService.Enable();
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
            Scale.Copy(Scalemodifiermodel.Scale);
            ScaleX.Copy(Scalemodifiermodel.ScaleX);
            ScaleY.Copy(Scalemodifiermodel.ScaleY);
            ScaleZ.Copy(Scalemodifiermodel.ScaleZ);
        }

        public void Paste(ScaleModifierModel Scalemodifiermodel)
        {
            MessageService.Disable();

            Scale.Paste(Scalemodifiermodel.Scale);
            ScaleX.Paste(Scalemodifiermodel.ScaleX);
            ScaleY.Paste(Scalemodifiermodel.ScaleY);
            ScaleZ.Paste(Scalemodifiermodel.ScaleZ);

            MessageService.Enable();
        }

        public void Reset()
        {
            MessageService.Disable();

            Scale.Reset();
            ScaleX.Reset();
            ScaleY.Reset();
            ScaleZ.Reset();

            MessageService.Enable();

            ScaleModifierModel Scalemodifiermodel = new ScaleModifierModel();
            this.Copy(Scalemodifiermodel);
            //SendMessages(MessageAddress, Scalemodifiermodel);
            //QueueObjects(Scalemodifiermodel);
            //SendQueues();
        }
        #endregion
    }
}