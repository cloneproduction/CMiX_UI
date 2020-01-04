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
        public Translate(string messageaddress, Messenger messenger, Mementor mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Translate));
            TranslateX = new Slider(MessageAddress + nameof(TranslateX), messenger, mementor);
            TranslateX.Minimum = -1;
            TranslateX.Maximum = 1;
            TranslateY = new Slider(MessageAddress + nameof(TranslateY), messenger, mementor);
            TranslateY.Minimum = -1;
            TranslateY.Maximum = 1;
            TranslateZ = new Slider(MessageAddress + nameof(TranslateZ), messenger, mementor);
            TranslateZ.Minimum = -1;
            TranslateZ.Maximum = 1;
        }

        public Slider TranslateX { get; set; }
        public Slider TranslateY { get; set; }
        public Slider TranslateZ { get; set; }
        public string MessageAddress { get; set; }
        public Messenger Messenger { get; set; }
        public Mementor Mementor { get; set; }

        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;
            TranslateX.UpdateMessageAddress($"{messageaddress}{nameof(TranslateX)}/");
            TranslateY.UpdateMessageAddress($"{messageaddress}{nameof(TranslateY)}/");
            TranslateZ.UpdateMessageAddress($"{messageaddress}{nameof(TranslateZ)}/");
        }

        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            TranslateModel translatemodel = new TranslateModel();
            this.Copy(translatemodel);
            IDataObject data = new DataObject();
            data.SetData("TranslateModel", translatemodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("TranslateModel"))
            {
                Mementor.BeginBatch();
                Messenger.Disable();

                var translatemodel = data.GetData("TranslateModel") as TranslateModel;
                var messageaddress = MessageAddress;
                this.Paste(translatemodel);
                UpdateMessageAddress(messageaddress);
                this.Copy(translatemodel);

                Messenger.Enable();
                Mementor.EndBatch();
                //SendMessages(nameof(TranslateModel), translatemodel);
            }
        }

        public void ResetGeometry()
        {
            TranslateModel translatemodel = new TranslateModel();
            this.Reset();
            this.Copy(translatemodel);
            //this.SendMessages(nameof(TranslateModel), translatemodel);
        }

        public void Copy(TranslateModel translatemodel)
        {
            translatemodel.MessageAddress = MessageAddress;
            TranslateX.CopyModel(translatemodel.TranslateX);
            TranslateY.CopyModel(translatemodel.TranslateY);
            TranslateZ.CopyModel(translatemodel.TranslateZ);
        }

        public void Paste(TranslateModel translatemodel)
        {
            Messenger.Disable();

            MessageAddress = translatemodel.MessageAddress;
            TranslateX.PasteModel(translatemodel.TranslateX);
            TranslateY.PasteModel(translatemodel.TranslateY);
            TranslateZ.PasteModel(translatemodel.TranslateZ);
            Messenger.Enable();
        }

        public void Reset()
        {
            Messenger.Disable();
            //Mementor.BeginBatch();

            TranslateX.Reset();
            TranslateY.Reset();
            TranslateZ.Reset();

            //Mementor.EndBatch();
            Messenger.Enable();

            TranslateModel translatemodel = new TranslateModel();
            this.Copy(translatemodel);
            //this.SendMessages(nameof(TranslateModel), translatemodel);
        }
        #endregion
    }
}
