using System;
using System.Windows;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;

namespace CMiX.ViewModels
{
    [Serializable]
    public class RotationModifier : ViewModel
    {
        public RotationModifier(string messageaddress, ObservableCollection<OSCValidation> oscvalidation, Mementor mementor, Beat beat) 
            : base(oscvalidation, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(RotationModifier));

            RotationMode = new GeometryRotation(MessageAddress + nameof(RotationMode), oscvalidation, mementor);
            X = new Slider(MessageAddress + nameof(X), oscvalidation, mementor);
            Y = new Slider(MessageAddress + nameof(Y), oscvalidation, mementor);
            Z = new Slider(MessageAddress + nameof(Z), oscvalidation, mementor);
            RotationBeatModifier = new BeatModifier(MessageAddress, beat, oscvalidation, mementor);
        }

        #region PROPERTIES
        public GeometryRotation RotationMode { get; }
        public BeatModifier RotationBeatModifier { get; }

        public Slider X { get; set; }
        public Slider Y { get; set; }
        public Slider Z { get; set; }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;

            RotationMode.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(RotationMode)));
            X.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(X)));
            Y.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Y)));
            Z.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Z)));
            RotationBeatModifier.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(RotationBeatModifier)));
        }
        #endregion

        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            RotationModifierModel rotationmodifiermodel = new RotationModifierModel();
            this.Copy(rotationmodifiermodel);
            IDataObject data = new DataObject();
            data.SetData("RotationModifierModel", rotationmodifiermodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("RotationModifierModel"))
            {
                Mementor.BeginBatch();
                DisabledMessages();

                var rotationmodifiermodel = data.GetData("RotationModifierModel") as RotationModifierModel;
                var messageaddress = MessageAddress;
                this.Paste(rotationmodifiermodel);
                UpdateMessageAddress(messageaddress);
                this.Copy(rotationmodifiermodel);

                EnabledMessages();
                Mementor.EndBatch();

                QueueObjects(rotationmodifiermodel);
                SendQueues();
            }
        }

        public void ResetGeometry()
        {
            RotationModifierModel rotationmodifiermodel = new RotationModifierModel();
            this.Reset();
            this.Copy(rotationmodifiermodel);
            QueueObjects(rotationmodifiermodel);
            SendQueues();
        }

        public void Copy(RotationModifierModel rotationmodifiermodel)
        {
            rotationmodifiermodel.MessageAddress = MessageAddress;

            RotationMode.Copy(rotationmodifiermodel.RotationPattern);
            X.Copy(rotationmodifiermodel.X);
            Y.Copy(rotationmodifiermodel.Y);
            Z.Copy(rotationmodifiermodel.Z);
            RotationBeatModifier.Copy(rotationmodifiermodel.RotationBeatModifier);
        }

        public void Paste(RotationModifierModel rotationmodifiermodel)
        {
            DisabledMessages();

            MessageAddress = rotationmodifiermodel.MessageAddress;
            X.Paste(rotationmodifiermodel.X);
            Y.Paste(rotationmodifiermodel.Y);
            Z.Paste(rotationmodifiermodel.Z);
            RotationMode.Paste(rotationmodifiermodel.RotationPattern);
            RotationBeatModifier.Paste(rotationmodifiermodel.RotationBeatModifier);
            EnabledMessages();
        }

        public void Reset()
        {
            DisabledMessages();
            //Mementor.BeginBatch();
            RotationMode.Reset();
            X.Reset();
            Y.Reset();
            Y.Reset();
            RotationBeatModifier.Reset();
            //Mementor.EndBatch();
            EnabledMessages();

            RotationModifierModel rotationmodifiermodel = new RotationModifierModel();
            this.Copy(rotationmodifiermodel);
            QueueObjects(rotationmodifiermodel);
            SendQueues();
        }
        #endregion
    }
}
