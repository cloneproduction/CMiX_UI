﻿using System;
using CMiX.Services;
using CMiX.Models;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Memento;

namespace CMiX.ViewModels
{
    public class PostFX : ViewModel
    {
        #region CONSTRUCTORS
        public PostFX(string layername, ObservableCollection<OSCMessenger> oscmessengers, Mementor mementor) : base (oscmessengers, mementor)
        {
            MessageAddress = String.Format("{0}/{1}/", layername, nameof(PostFX));
            Feedback = new Slider(String.Format("{0}/{1}/{2}", layername, nameof(PostFX), "Feedback"), oscmessengers, mementor);
            Blur = new Slider(String.Format("{0}/{1}/{2}", layername, nameof(PostFX), "Blur"), oscmessengers, mementor);
            Transforms = ((PostFXTransforms)0).ToString();
            View = ((PostFXView)0).ToString();
            CopySelfCommand = new RelayCommand(p => CopySelf());
            PasteSelfCommand = new RelayCommand(p => PasteSelf());
            ResetSelfCommand = new RelayCommand(p => ResetSelf());
        }
        #endregion

        #region PROPERTIES
        public ICommand CopySelfCommand { get; }
        public ICommand PasteSelfCommand { get; }
        public ICommand ResetSelfCommand { get; }

        public Slider Feedback { get; }
        public Slider Blur { get; }

        private string _transforms;
        [OSC]
        public string Transforms
        {
            get => _transforms;
            set
            {
                if(Mementor != null)
                    Mementor.PropertyChange(this, "Transforms");
                SetAndNotify(ref _transforms, value);
                SendMessages(MessageAddress + nameof(Transforms), Transforms);
            }
        }

        private string _view;
        [OSC]
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

        public void CopySelf()
        {
            PostFXModel postFXmodel = new PostFXModel();
            this.Copy(postFXmodel);
            IDataObject data = new DataObject();
            data.SetData("PostFX", postFXmodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteSelf()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("PostFX"))
            {
                var postFXmodel = (PostFXModel)data.GetData("PostFX") as PostFXModel;
                this.Paste(postFXmodel);

                QueueObjects(this);
                SendQueues();
            }
        }

        public void ResetSelf()
        {
            PostFXModel postFXmodel = new PostFXModel();
            this.Paste(postFXmodel);
        }
        #endregion
    }
}