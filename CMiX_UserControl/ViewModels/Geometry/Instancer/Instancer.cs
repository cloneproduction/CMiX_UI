using System;
using System.Windows;
using System.Collections.ObjectModel;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;

namespace CMiX.ViewModels
{
    [Serializable]
    public class Instancer : ViewModel
    {
        public Instancer(string messageaddress, ObservableCollection<OSCValidation> oscvalidation, Mementor mementor, Beat beat)
            : base(oscvalidation, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Instancer));

            Transform = new Transform(MessageAddress, oscvalidation, mementor);
            Counter = new Counter(MessageAddress, oscvalidation, mementor);
            TranslateModifier = new TranslateModifier(MessageAddress, oscvalidation, mementor, beat);
            ScaleModifier = new ScaleModifier(MessageAddress, oscvalidation, mementor, beat);
            RotationModifier = new RotationModifier(MessageAddress, oscvalidation, mementor, beat);

            NoAspectRatio = false;
        }

        public Transform Transform { get; set; }
        public Counter Counter { get; set; }
        public TranslateModifier TranslateModifier { get; set; }
        public ScaleModifier ScaleModifier { get; set; }
        public RotationModifier RotationModifier { get; set; }

        private bool _noAspectRatio;
        public bool NoAspectRatio
        {
            get => _noAspectRatio;
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, "NoAspectRatio");
                SetAndNotify(ref _noAspectRatio, value);
                SendMessages(MessageAddress + nameof(NoAspectRatio), NoAspectRatio.ToString());
            }
        }

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;

            Counter.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Counter)));
            TranslateModifier.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(TranslateModifier)));
            ScaleModifier.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(ScaleModifier)));
            RotationModifier.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(RotationModifier)));
        }
        #endregion

        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            InstancerModel instancermodel = new InstancerModel();
            this.Copy(instancermodel);
            IDataObject data = new DataObject();
            data.SetData("InstancerModel", instancermodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("InstancerModel"))
            {
                Mementor.BeginBatch();
                DisabledMessages();

                var instancermodel = data.GetData("InstancerModel") as InstancerModel;
                var geometrymessageaddress = MessageAddress;
                this.Paste(instancermodel);
                UpdateMessageAddress(geometrymessageaddress);
                this.Copy(instancermodel);

                EnabledMessages();
                Mementor.EndBatch();

                QueueObjects(instancermodel);
                SendQueues();
            }
        }

        public void ResetGeometry()
        {
            InstancerModel instancermodel = new InstancerModel();
            this.Reset();
            this.Copy(instancermodel);
            QueueObjects(instancermodel);
            SendQueues();
        }

        public void Copy(InstancerModel instancermodel)
        {
            instancermodel.MessageAddress = MessageAddress;
            Transform.Copy(instancermodel.Transform);
            Counter.Copy(instancermodel.Counter);
            TranslateModifier.Copy(instancermodel.TranslateModifier);
            ScaleModifier.Copy(instancermodel.ScaleModifier);
            RotationModifier.Copy(instancermodel.RotationModifier);
            instancermodel.NoAspectRatio = NoAspectRatio;
        }

        public void Paste(InstancerModel instancermodel)
        {
            DisabledMessages();

            MessageAddress = instancermodel.MessageAddress;
            Transform.Paste(instancermodel.Transform);
            Counter.Paste(instancermodel.Counter);
            TranslateModifier.Paste(instancermodel.TranslateModifier);
            ScaleModifier.Paste(instancermodel.ScaleModifier);
            RotationModifier.Paste(instancermodel.RotationModifier);
            NoAspectRatio = instancermodel.NoAspectRatio;

            EnabledMessages();
        }

        public void Reset()
        {
            DisabledMessages();
            //Mementor.BeginBatch();
            Counter.Reset();
            TranslateModifier.Reset();
            ScaleModifier.Reset();
            RotationModifier.Reset();
            NoAspectRatio = false;
            //Mementor.EndBatch();
            EnabledMessages();

            InstancerModel instancermodel = new InstancerModel();
            this.Copy(instancermodel);
            QueueObjects(instancermodel);
            SendQueues();
        }
        #endregion
    }
}
