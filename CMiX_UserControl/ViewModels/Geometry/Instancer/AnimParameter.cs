using System;
using System.Collections.ObjectModel;
using System.Windows;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using Memento;
using CMiX.MVVM.Services;

namespace CMiX.ViewModels
{
    public class AnimParameter : ViewModel, ISendable, IUndoable
    {
        public AnimParameter(string messageaddress, MessageService messageService, Mementor mementor, Beat beat, bool isEnabled)
        {
            Mode = AnimMode.None;
            IsEnabled = isEnabled;
            Slider = new Slider(MessageAddress, messageService, mementor);
            BeatModifier = new BeatModifier(MessageAddress, beat, messageService, mementor);
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
        public MessageService MessageService { get; set; }
        public Mementor Mementor { get; set; }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;
            Slider.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Slider)));
            BeatModifier.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(BeatModifier)));
        }
        #endregion

        #region COPY/PASTE/RESET
        public void CopyGeometry()
        {
            AnimParameterModel animparametermodel = new AnimParameterModel();
            this.Copy(animparametermodel);
            IDataObject data = new DataObject();
            data.SetData("AnimParameterModel", animparametermodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteGeometry()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("AnimParameterModel"))
            {
                Mementor.BeginBatch();
                MessageService.DisabledMessages();

                var animparametermodel = data.GetData("AnimParameterModel") as AnimParameterModel;
                var messageaddress = MessageAddress;
                this.Paste(animparametermodel);
                UpdateMessageAddress(messageaddress);
                this.Copy(animparametermodel);

                MessageService.EnabledMessages();
                Mementor.EndBatch();
                //SendMessages(MessageAddress, animparametermodel);
                //QueueObjects(animparametermodel);
                //SendQueues();
            }
        }

        public void ResetGeometry()
        {
            AnimParameterModel animparametermodel = new AnimParameterModel();
            this.Reset();
            this.Copy(animparametermodel);
            //SendMessages(MessageAddress, animparametermodel);
            //QueueObjects(animparametermodel);
            //SendQueues();
        }

        public void Copy(AnimParameterModel animparametermodel)
        {
            animparametermodel.MessageAddress = MessageAddress;

            Slider.Copy(animparametermodel.Slider);
            BeatModifier.Copy(animparametermodel.BeatModifier);
        }

        public void Paste(AnimParameterModel animparametermodel)
        {
            MessageService.DisabledMessages();

            MessageAddress = animparametermodel.MessageAddress;
            Slider.Paste(animparametermodel.Slider);
            BeatModifier.Paste(animparametermodel.BeatModifier);

            MessageService.EnabledMessages();
        }

        public void Reset()
        {
            MessageService.DisabledMessages();

            Slider.Reset();
            BeatModifier.Reset();

            MessageService.EnabledMessages();

            AnimParameterModel animparametermodel = new AnimParameterModel();
            this.Copy(animparametermodel);
            //SendMessages(MessageAddress, animparametermodel);
            //QueueObjects(animparametermodel);
            //SendQueues();
        }
        #endregion
    }
}
