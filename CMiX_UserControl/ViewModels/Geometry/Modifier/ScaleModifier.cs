using System;
using System.Windows;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;

namespace CMiX.ViewModels
{
    [Serializable]
    public class ScaleModifier : ViewModel
    {
        public ScaleModifier(string messageaddress, ObservableCollection<OSCValidation> oscvalidation, Mementor mementor, Beat beat) 
            : base(oscvalidation, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(ScaleModifier));

            ScaleMode = new GeometryScale(MessageAddress + nameof(ScaleMode), oscvalidation, mementor);
            X = new Slider(MessageAddress + nameof(X), oscvalidation, mementor);
            Y = new Slider(MessageAddress + nameof(Y), oscvalidation, mementor);
            Z = new Slider(MessageAddress + nameof(Z), oscvalidation, mementor);
            ScaleBeatModifier = new BeatModifier(MessageAddress, beat, oscvalidation, mementor);
        }

        #region PROPERTIES
        public GeometryScale ScaleMode { get; }
        public BeatModifier ScaleBeatModifier { get; }

        public Slider X { get; set; }
        public Slider Y { get; set; }
        public Slider Z { get; set; }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;

            ScaleMode.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(ScaleMode)));
            X.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(X)));
            Y.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Y)));
            Z.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Z)));
            ScaleBeatModifier.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(ScaleBeatModifier)));
        }
        #endregion

        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            ScaleModifierModel scalemodifiermodel = new ScaleModifierModel();
            this.Copy(scalemodifiermodel);
            IDataObject data = new DataObject();
            data.SetData("ScaleModifierModel", scalemodifiermodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("ScaleModifierModel"))
            {
                Mementor.BeginBatch();
                DisabledMessages();

                var scalemodifiermodel = data.GetData("ScaleModifierModel") as ScaleModifierModel;
                var messageaddress = MessageAddress;
                this.Paste(scalemodifiermodel);
                UpdateMessageAddress(messageaddress);
                this.Copy(scalemodifiermodel);

                EnabledMessages();
                Mementor.EndBatch();

                QueueObjects(scalemodifiermodel);
                SendQueues();
            }
        }

        public void ResetGeometry()
        {
            ScaleModifierModel scalemodifiermodel = new ScaleModifierModel();
            this.Reset();
            this.Copy(scalemodifiermodel);
            QueueObjects(scalemodifiermodel);
            SendQueues();
        }

        public void Copy(ScaleModifierModel scalemodifiermodel)
        {
            scalemodifiermodel.MessageAddress = MessageAddress;

            ScaleMode.Copy(scalemodifiermodel.ScalePattern);
            X.Copy(scalemodifiermodel.X);
            Y.Copy(scalemodifiermodel.Y);
            Z.Copy(scalemodifiermodel.Z);
            ScaleBeatModifier.Copy(scalemodifiermodel.ScaleBeatModifier);
        }

        public void Paste(ScaleModifierModel scalemodifiermodel)
        {
            DisabledMessages();

            MessageAddress = scalemodifiermodel.MessageAddress;
            X.Paste(scalemodifiermodel.X);
            Y.Paste(scalemodifiermodel.Y);
            Z.Paste(scalemodifiermodel.Z);
            ScaleMode.Paste(scalemodifiermodel.ScalePattern);
            ScaleBeatModifier.Paste(scalemodifiermodel.ScaleBeatModifier);
            EnabledMessages();
        }

        public void Reset()
        {
            DisabledMessages();
            //Mementor.BeginBatch();
            ScaleMode.Reset();
            X.Reset();
            Y.Reset();
            Y.Reset();
            ScaleBeatModifier.Reset();
            //Mementor.EndBatch();
            EnabledMessages();

            ScaleModifierModel scalemodifiermodel = new ScaleModifierModel();
            this.Copy(scalemodifiermodel);
            QueueObjects(scalemodifiermodel);
            SendQueues();
        }
        #endregion
    }
}
