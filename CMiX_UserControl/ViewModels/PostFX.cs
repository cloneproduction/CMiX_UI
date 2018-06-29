﻿using System;
using CMiX.Services;

namespace CMiX.ViewModels
{
    public class PostFX : ViewModel, IMessengerData
    {
        public PostFX(string layername, IMessenger messenger)
            : this(
                  messageaddress: String.Format("{0}/{1}/", layername, nameof(PostFX)),
                  messenger: messenger,
                  feedback: 0.0, 
                  blur: 0.0, 
                  transforms: ((PostFXTransforms)0).ToString(), 
                  view: ((PostFXView)0).ToString())
        { }

        public PostFX(
            IMessenger messenger,
            string messageaddress,
            double feedback, 
            double blur,
            string transforms,
            string view)
        {
            AssertNotNegative(() => feedback);
            AssertNotNegative(() => blur);

            MessageAddress = messageaddress;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            Feedback = feedback;
            Blur = blur;
            Transforms = transforms;
            View = view;
        }
        public string MessageAddress { get; set; }

        public bool MessageEnabled { get; set; }

        public IMessenger Messenger { get; }

        private double _feedback;
        [OSC]
        public double Feedback
        {
            get => _feedback;
            set
            {
                SetAndNotify(ref _feedback, CoerceNotNegative(value));
                Messenger.SendMessage(MessageAddress + nameof(Feedback), Feedback);
            }
        }

        private double _blur;
        [OSC]
        public double Blur
        {
            get => _blur;
            set
            {
                SetAndNotify(ref _blur, CoerceNotNegative(value));
                Messenger.SendMessage(MessageAddress + nameof(Blur), Blur);
            }
        }

        private string _transforms;
        [OSC]
        public string Transforms
        {
            get => _transforms;
            set
            {
                SetAndNotify(ref _transforms, value);
                Messenger.SendMessage(MessageAddress + nameof(Transforms), Transforms);
            }
        }

        private string _view;
        [OSC]
        public string View
        {
            get => _view;
            set
            {
                SetAndNotify(ref _view, value);
                Messenger.SendMessage(MessageAddress + nameof(View), View);
            }
        }
    }
}