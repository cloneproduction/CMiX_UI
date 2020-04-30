using System;
using System.Windows;
using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class Instancer : ViewModel
    {
        public Instancer(string messageaddress, Beat beat)
        {
            MessageAddress = $"{messageaddress}{nameof(Instancer)}/";
            //MessengerService = messengerService;

            Transform = new Transform(MessageAddress);
            Counter = new Counter(MessageAddress);
            TranslateModifier = new TranslateModifier(MessageAddress, beat);
            ScaleModifier = new ScaleModifier(MessageAddress, beat);
            RotationModifier = new RotationModifier(MessageAddress, beat);

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
                //if (Mementor != null)
                //    Mementor.PropertyChange(this, "NoAspectRatio");
                SetAndNotify(ref _noAspectRatio, value);
                //SendMessages(MessageAddress + nameof(NoAspectRatio), NoAspectRatio.ToString());
            }
        }

        public string MessageAddress { get; set; }
        public MessengerService MessengerService { get; set; }

        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            IDataObject data = new DataObject();
            data.SetData("InstancerModel", this.GetModel(), false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("InstancerModel"))
            {
                //Mementor.BeginBatch();
                MessengerService.Disable();

                var instancermodel = data.GetData("InstancerModel") as InstancerModel;
                this.SetViewModel(instancermodel);

                MessengerService.Enable();
                //Mementor.EndBatch();
                //SendMessages(nameof(InstancerModel), GetModel());
            }
        }

        public void ResetGeometry()
        {
            this.Reset();
            //SendMessages(nameof(InstancerModel), GetModel());
        }

        public void Reset()
        {
            MessengerService.Disable();
            //Mementor.BeginBatch();
            Counter.Reset();
            TranslateModifier.Reset();
            ScaleModifier.Reset();
            RotationModifier.Reset();
            NoAspectRatio = false;
            //Mementor.EndBatch();
            MessengerService.Enable();

            InstancerModel instancermodel = new InstancerModel();
            //this.Copy(instancermodel);
            //SendMessages(nameof(InstancerModel), instancermodel);
        }


        #endregion
    }
}
