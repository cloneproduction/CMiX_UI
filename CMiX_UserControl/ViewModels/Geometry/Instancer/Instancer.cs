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
        public Instancer(string messageaddress, Sender sender, Mementor mementor, Beat beat)
        {
            MessageAddress = $"{messageaddress}{nameof(Instancer)}/";
            Sender = sender;

            Transform = new Transform(MessageAddress, sender, mementor);
            Counter = new Counter(MessageAddress, sender, mementor);
            TranslateModifier = new TranslateModifier(MessageAddress, sender, mementor, beat);
            ScaleModifier = new ScaleModifier(MessageAddress, sender, mementor, beat);
            RotationModifier = new RotationModifier(MessageAddress, sender, mementor, beat);

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
        public Sender Sender { get; set; }
        public Mementor Mementor { get; set; }

        //#region METHODS
        //public void UpdateMessageAddress(string messageaddress)
        //{
        //    MessageAddress = messageaddress;

        //    Counter.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Counter)));
        //    TranslateModifier.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(TranslateModifier)));
        //    ScaleModifier.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(ScaleModifier)));
        //    RotationModifier.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(RotationModifier)));
        //}
        //#endregion

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
                Sender.Disable();

                var instancermodel = data.GetData("InstancerModel") as InstancerModel;
                var geometrymessageaddress = MessageAddress;
                this.Paste(instancermodel);
                //UpdateMessageAddress(geometrymessageaddress);
                this.Copy(instancermodel);

                Sender.Enable();
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
            Transform.Copy(instancermodel.Transform);
            Counter.Copy(instancermodel.Counter);
            TranslateModifier.Copy(instancermodel.TranslateModifier);
            ScaleModifier.Copy(instancermodel.ScaleModifier);
            RotationModifier.Copy(instancermodel.RotationModifier);
            instancermodel.NoAspectRatio = NoAspectRatio;
        }

        public void Paste(InstancerModel instancermodel)
        {
            Sender.Disable();

            Transform.Paste(instancermodel.Transform);
            Counter.Paste(instancermodel.Counter);
            TranslateModifier.Paste(instancermodel.TranslateModifier);
            ScaleModifier.Paste(instancermodel.ScaleModifier);
            RotationModifier.Paste(instancermodel.RotationModifier);
            NoAspectRatio = instancermodel.NoAspectRatio;

            Sender.Enable();
        }

        public void Reset()
        {
            Sender.Disable();
            //Mementor.BeginBatch();
            Counter.Reset();
            TranslateModifier.Reset();
            ScaleModifier.Reset();
            RotationModifier.Reset();
            NoAspectRatio = false;
            //Mementor.EndBatch();
            Sender.Enable();

            InstancerModel instancermodel = new InstancerModel();
            this.Copy(instancermodel);
            //SendMessages(nameof(InstancerModel), instancermodel);
        }
        #endregion
    }
}
