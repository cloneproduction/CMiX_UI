using System;
using CMiX.Services;
using CMiX.Models;

namespace CMiX.ViewModels
{
    public class PostFX : ViewModel, IMessengerData
    {
        public PostFX(string layername, IMessenger messenger)
            : this(
                  feedback: 0.0, 
                  blur: 0.0, 
                  transforms: ((PostFXTransforms)0).ToString(), 
                  view: ((PostFXView)0).ToString(),

                  messageaddress: String.Format("{0}/{1}/", layername, nameof(PostFX)),
                  messenger: messenger,
                  messageEnabled: true)
        { }

        public PostFX(
            double feedback, 
            double blur,
            string transforms,
            string view,

            IMessenger messenger,
            string messageaddress,
            bool messageEnabled
            )
        {
            AssertNotNegative(() => feedback);
            AssertNotNegative(() => blur);

            Feedback = feedback;
            Blur = blur;
            Transforms = transforms;
            View = view;

            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            MessageAddress = messageaddress;
            MessageEnabled = messageEnabled;
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
                if(MessageEnabled)
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
                if(MessageEnabled)
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
                if(MessageEnabled)
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
                if(MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(View), View);
            }
        }

        public void Copy(PostFXDTO postFXdto)
        {
            postFXdto.Feedback = Feedback;
            postFXdto.Blur = Blur;
            postFXdto.Transforms = Transforms;
            postFXdto.View = View;
        }

        public void Paste(PostFXDTO postFXdto)
        {
            MessageEnabled = false;

            Feedback = postFXdto.Feedback;
            Blur = postFXdto.Blur;
            Transforms = postFXdto.Transforms;
            View = postFXdto.View;

            MessageEnabled = true;
        }
    }
}