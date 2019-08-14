using System;

using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Memento;
using System.Windows;

namespace CMiX.ViewModels
{
    public class PostFX : ViewModel
    {
        #region CONSTRUCTORS
        public PostFX(string messageaddress, ObservableCollection<OSCValidation> oscvalidation, Mementor mementor) : base(oscvalidation, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(PostFX));

            Feedback = new Slider(MessageAddress + nameof(Feedback),oscvalidation, mementor);
            Blur = new Slider(MessageAddress + nameof(Blur),oscvalidation, mementor);

            Transforms = ((PostFXTransforms)0).ToString();
            View = ((PostFXView)0).ToString();

            CopyPostFXCommand = new RelayCommand(p => CopyPostFX());
            PastePostFXCommand = new RelayCommand(p => PastePostFX());
            ResetPostFXCommand = new RelayCommand(p => ResetPostFX());
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
        public ICommand CopyPostFXCommand { get; }
        public ICommand PastePostFXCommand { get; }
        public ICommand ResetPostFXCommand { get; }

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

            Transforms = postFXmodel.Transforms;
            View = postFXmodel.View;

            Feedback.Paste(postFXmodel.Feedback);
            Blur.Paste(postFXmodel.Blur);

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

        public void CopyPostFX()
        {
            PostFXModel postfxmodel = new PostFXModel();
            this.Copy(postfxmodel);
            IDataObject data = new DataObject();
            data.SetData("PostFX", postfxmodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PastePostFX()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("PostFXModel"))
            {
                Mementor.BeginBatch();
                var postfxmodel = data.GetData("PostFXModel") as PostFXModel;
                this.Paste(postfxmodel);
                Mementor.EndBatch();
            }
        }

        public void ResetPostFX()
        {
            PostFXModel postfxmodel = new PostFXModel();
            this.Reset();
            this.Copy(postfxmodel);
            QueueObjects(postfxmodel);
            SendQueues();
        }
        #endregion
    }
}