using System;
using System.Windows;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;

namespace CMiX.ViewModels
{
    [Serializable]
    public class TranslateModifier : ViewModel
    {
        public TranslateModifier(string messageaddress, ObservableCollection<OSCValidation> oscvalidation, Mementor mementor, Beat beat) 
            : base(oscvalidation, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(TranslateModifier));

            TranslateMode = new GeometryTranslate(MessageAddress + nameof(TranslateMode), oscvalidation, mementor);
            X = new Slider(MessageAddress + nameof(X), oscvalidation, mementor);
            Y = new Slider(MessageAddress + nameof(Y), oscvalidation, mementor);
            Z = new Slider(MessageAddress + nameof(Z), oscvalidation, mementor);
            TranslateBeatModifier = new BeatModifier(MessageAddress, beat, oscvalidation, mementor);
        }

        #region PROPERTIES
        public GeometryTranslate TranslateMode { get; set; }
        public BeatModifier TranslateBeatModifier { get; set; }

        public Slider X { get; set; }
        public Slider Y { get; set; }
        public Slider Z { get; set; }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;

            TranslateMode.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(TranslateMode)));
            X.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(X)));
            Y.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Y)));
            Z.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Z)));
            TranslateBeatModifier.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(TranslateBeatModifier)));
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
                DisabledMessages();

                var translatemodifiermodel = data.GetData("TranslateModifierModel") as TranslateModifierModel;
                var messageaddress = MessageAddress;
                this.Paste(translatemodifiermodel);
                UpdateMessageAddress(messageaddress);
                this.Copy(translatemodifiermodel);

                EnabledMessages();
                Mementor.EndBatch();

                QueueObjects(translatemodifiermodel);
                SendQueues();
            }
        }

        public void ResetGeometry()
        {
            TranslateModifierModel translatemodifiermodel = new TranslateModifierModel();
            this.Reset();
            this.Copy(translatemodifiermodel);
            QueueObjects(translatemodifiermodel);
            SendQueues();
        }

        public void Copy(TranslateModifierModel translatemodifiermodel)
        {
            translatemodifiermodel.MessageAddress = MessageAddress;

            TranslateMode.Copy(translatemodifiermodel.TranslatePattern);
            X.Copy(translatemodifiermodel.X);
            Y.Copy(translatemodifiermodel.Y);
            Z.Copy(translatemodifiermodel.Z);
            TranslateBeatModifier.Copy(translatemodifiermodel.TranslateBeatModifier);
        }

        public void Paste(TranslateModifierModel translatemodifiermodel)
        {
            DisabledMessages();

            MessageAddress = translatemodifiermodel.MessageAddress;
            X.Paste(translatemodifiermodel.X);
            Y.Paste(translatemodifiermodel.Y);
            Z.Paste(translatemodifiermodel.Z);
            TranslateMode.Paste(translatemodifiermodel.TranslatePattern);
            TranslateBeatModifier.Paste(translatemodifiermodel.TranslateBeatModifier);
            EnabledMessages();
        }

        public void Reset()
        {
            DisabledMessages();
            //Mementor.BeginBatch();
            TranslateMode.Reset();
            X.Reset();
            Y.Reset();
            Y.Reset();
            TranslateBeatModifier.Reset();
            //Mementor.EndBatch();
            EnabledMessages();

            TranslateModifierModel translatemodifiermodel = new TranslateModifierModel();
            this.Copy(translatemodifiermodel);
            QueueObjects(translatemodifiermodel);
            SendQueues();
        }
        #endregion
    }
}
