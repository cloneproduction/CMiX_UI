﻿using System;
using System.Windows;
using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class Instancer : ViewModel, IUndoable
    {
        public Instancer(string messageaddress, MessengerService messengerService, Mementor mementor, Beat beat)
        {
            MessageAddress = $"{messageaddress}{nameof(Instancer)}/";
            MessengerService = messengerService;

            Transform = new Transform(MessageAddress, messengerService, mementor);
            Counter = new Counter(MessageAddress, messengerService, mementor);
            TranslateModifier = new TranslateModifier(MessageAddress, messengerService, mementor, beat);
            ScaleModifier = new ScaleModifier(MessageAddress, messengerService, mementor, beat);
            RotationModifier = new RotationModifier(MessageAddress, messengerService, mementor, beat);

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
        public Mementor Mementor { get; set; }
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
                Mementor.BeginBatch();
                MessengerService.Disable();

                var instancermodel = data.GetData("InstancerModel") as InstancerModel;
                this.SetViewModel(instancermodel);

                MessengerService.Enable();
                Mementor.EndBatch();
                //SendMessages(nameof(InstancerModel), GetModel());
            }
        }

        public void ResetGeometry()
        {
            this.Reset();
            //SendMessages(nameof(InstancerModel), GetModel());
        }

        //public InstancerModel GetModel()
        //{
        //    InstancerModel instancerModel = new InstancerModel();
        //    instancerModel.Transform = Transform.GetModel();
        //    instancerModel.Counter = Counter.GetModel();
        //    instancerModel.TranslateModifier = TranslateModifier.GetModel();
        //    instancerModel.ScaleModifier = ScaleModifier.GetModel();
        //    instancerModel.RotationModifier = RotationModifier.GetModel();
        //    instancerModel.NoAspectRatio = NoAspectRatio;
        //    return instancerModel;
        //}

        public void SetViewModel(InstancerModel model)
        {

        }
        //public void Copy(InstancerModel instancermodel)
        //{
        //    Transform.Copy(instancermodel.Transform);
        //    Counter.Copy(instancermodel.Counter);
        //    TranslateModifier.Copy(instancermodel.TranslateModifier);
        //    ScaleModifier.Copy(instancermodel.ScaleModifier);
        //    RotationModifier.Copy(instancermodel.RotationModifier);
        //    instancermodel.NoAspectRatio = NoAspectRatio;
        //}

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
