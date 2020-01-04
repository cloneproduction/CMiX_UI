using System;
using System.Windows;
using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class Instancer : ViewModel, ISendable, IUndoable
    {
        public Instancer(string messageaddress, Messenger messenger, Mementor mementor, Beat beat)
        {
            MessageAddress = $"{messageaddress}{nameof(Instancer)}/";

            Transform = new Transform(MessageAddress, messenger, mementor);
            Counter = new Counter(MessageAddress, messenger, mementor);
            TranslateModifier = new TranslateModifier(MessageAddress, messenger, mementor, beat);
            ScaleModifier = new ScaleModifier(MessageAddress, messenger, mementor, beat);
            RotationModifier = new RotationModifier(MessageAddress, messenger, mementor, beat);

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
                //SendMessages(MessageAddress + nameof(NoAspectRatio), NoAspectRatio.ToString());
            }
        }

        public string MessageAddress { get; set; }
        public Messenger Messenger { get; set; }
        public Mementor Mementor { get; set; }

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
                Messenger.Disable();

                var instancermodel = data.GetData("InstancerModel") as InstancerModel;
                var geometrymessageaddress = MessageAddress;
                this.Paste(instancermodel);
                UpdateMessageAddress(geometrymessageaddress);
                this.Copy(instancermodel);

                Messenger.Enable();
                Mementor.EndBatch();
                //SendMessages(nameof(InstancerModel), instancermodel);
            }
        }

        public void ResetGeometry()
        {
            InstancerModel instancermodel = new InstancerModel();
            this.Reset();
            this.Copy(instancermodel);
            //SendMessages(nameof(InstancerModel), instancermodel);
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
            Messenger.Disable();

            MessageAddress = instancermodel.MessageAddress;
            Transform.Paste(instancermodel.Transform);
            Counter.Paste(instancermodel.Counter);
            TranslateModifier.Paste(instancermodel.TranslateModifier);
            ScaleModifier.Paste(instancermodel.ScaleModifier);
            RotationModifier.Paste(instancermodel.RotationModifier);
            NoAspectRatio = instancermodel.NoAspectRatio;

            Messenger.Enable();
        }

        public void Reset()
        {
            Messenger.Disable();
            //Mementor.BeginBatch();
            Counter.Reset();
            TranslateModifier.Reset();
            ScaleModifier.Reset();
            RotationModifier.Reset();
            NoAspectRatio = false;
            //Mementor.EndBatch();
            Messenger.Enable();

            InstancerModel instancermodel = new InstancerModel();
            this.Copy(instancermodel);
            //SendMessages(nameof(InstancerModel), instancermodel);
        }
        #endregion
    }
}
