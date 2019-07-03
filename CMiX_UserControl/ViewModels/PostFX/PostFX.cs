using System;
using CMiX.Models;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Memento;

namespace CMiX.ViewModels
{
    public class PostFX : ViewModel
    {
        #region CONSTRUCTORS
        public PostFX(string messageaddress, ObservableCollection<OSCMessenger> oscmessengers, Mementor mementor) : base(oscmessengers, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(PostFX));

            Feedback = new Slider(MessageAddress + nameof(Feedback), oscmessengers, mementor);
            Blur = new Slider(MessageAddress + nameof(Blur), oscmessengers, mementor);

            Transforms = ((PostFXTransforms)0).ToString();
            View = ((PostFXView)0).ToString();
            ResetCommand = new RelayCommand(p => Reset());
        }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress = messageaddress;

            Feedback.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Feedback)));
            Blur.UpdateMessageAddress(String.Format("{0}{1}/", messageaddress, nameof(Blur)));
        }
        #endregion

        #region PROPERTIES
        public ICommand ResetCommand { get; }

        public Slider Feedback { get; }
        public Slider Blur { get; }

        private string _transforms;
        public string Transforms
        {
            get => _transforms;
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, "Transforms");
                SetAndNotify(ref _transforms, value);
                SendMessages(MessageAddress + nameof(Transforms), Transforms);
            }
        }

        private string _view;
        public string View
        {
            get => _view;
            set
            {
                if (Mementor != null)
                    Mementor.PropertyChange(this, "View");
                SetAndNotify(ref _view, value);
                SendMessages(MessageAddress + nameof(View), View);
            }
        }
        #endregion

        #region COPY/PASTE/RESET
        public void Copy(PostFXModel postFXmodel)
        {
            Feedback.Copy(postFXmodel.Feedback);
            Blur.Copy(postFXmodel.Blur);
            postFXmodel.MessageAddress = MessageAddress;
            postFXmodel.Transforms = Transforms;
            postFXmodel.View = View;
        }

        public void Paste(PostFXModel postFXmodel)
        {
            DisabledMessages();

            MessageAddress = postFXmodel.MessageAddress;
            Feedback.Paste(postFXmodel.Feedback);
            Blur.Paste(postFXmodel.Blur);
            Transforms = postFXmodel.Transforms;
            View = postFXmodel.View;

            EnabledMessages();
        }

        public void Reset()
        {
            DisabledMessages();

            Transforms = ((PostFXTransforms)0).ToString();
            View = ((PostFXView)0).ToString();

            Feedback.Reset();
            Blur.Reset();

            PostFXModel postfxmodel = new PostFXModel();
            this.Copy(postfxmodel);
            QueueObjects(postfxmodel);
            SendQueues();

            EnabledMessages();
        }
        #endregion
    }
}