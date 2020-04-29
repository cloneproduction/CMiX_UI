using System;
using System.Windows;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class AnimParameter : ViewModel, IUndoable
    {
        public AnimParameter(string messageaddress, MessengerService messengerService, Mementor mementor, Beat beat, bool isEnabled)
        {
            Mode = AnimMode.None;
            MessengerService = messengerService;
            IsEnabled = isEnabled;
            Slider = new Slider(MessageAddress, messengerService, mementor);
            BeatModifier = new BeatModifier(MessageAddress, beat, messengerService, mementor);
        }

        #region PROPERTIES
        private bool _IsEnabled;
        public bool IsEnabled
        {
            get => _IsEnabled;
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, nameof(IsEnabled));
                SetAndNotify(ref _IsEnabled, value);
                //SendMessages(MessageAddress + nameof(Mode), IsEnabled);
            }
        }

        private AnimMode _Mode;
        public AnimMode Mode
        {
            get => _Mode;
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, nameof(Mode));
                SetAndNotify(ref _Mode, value);
                //SendMessages(MessageAddress + nameof(Mode), Mode);
            }
        }

        public Slider Slider { get; set; }
        public BeatModifier BeatModifier { get; set; }
        public string MessageAddress { get; set; }
        public MessengerService MessengerService { get; set; }
        public Mementor Mementor { get; set; }
        #endregion

        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            IDataObject data = new DataObject();
            data.SetData("AnimParameterModel", this.GetModel(), false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("AnimParameterModel"))
            {
                Mementor.BeginBatch();
                MessengerService.Disable();

                var animparametermodel = data.GetData("AnimParameterModel") as AnimParameterModel;
                this.Paste(animparametermodel);

                MessengerService.Enable();
                Mementor.EndBatch();
                //SendMessages(MessageAddress, GetModel());
                //QueueObjects(animparametermodel);
                //SendQueues();
            }
        }

        public void ResetGeometry()
        {
            this.Reset();
            //SendMessages(MessageAddress, GetModel());
            //QueueObjects(animparametermodel);
            //SendQueues();
        }



        //public void Copy(AnimParameterModel animparametermodel)
        //{
        //    Slider.CopyModel(animparametermodel.Slider);
        //    BeatModifier.CopyModel(animparametermodel.BeatModifier);
        //}

        public void Paste(AnimParameterModel animparametermodel)
        {
            MessengerService.Disable();

            Slider.SetViewModel(animparametermodel.Slider);
            BeatModifier.SetViewModel(animparametermodel.BeatModifier);

            MessengerService.Enable();
        }

        public void Reset()
        {
            MessengerService.Disable();

            Slider.Reset();
            BeatModifier.Reset();

            MessengerService.Enable();

            AnimParameterModel animparametermodel = this.GetModel();
            //this.Copy(animparametermodel);
            //SendMessages(MessageAddress, GetModel());
            //QueueObjects(animparametermodel);
            //SendQueues();
        }
        #endregion
    }
}
