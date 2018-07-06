﻿using CMiX.Services;
using System;

namespace CMiX.ViewModels
{
    [Serializable]
    public class Layer : ViewModel, IMessengerData
    {
        public Layer() { }

        public Layer(MasterBeat masterBeat, string layername, IMessenger messenger, int index)
        {
            Messenger = messenger;
            Index = index;
            LayerName = layername;
            MessageAddress = String.Format("{0}/", layername);
            MessageEnabled = true;
            Fade = 0.0;
            BlendMode = ((BlendMode)0).ToString();
            Index = 0;
            Enabled = false;
            BeatModifier = new BeatModifier(layername, messenger, masterBeat);
            Content = new Content(BeatModifier, layername, messenger);
            Mask = new Mask(BeatModifier, layername, messenger);
            Coloration = new Coloration(BeatModifier, layername, messenger);
        }

        public Layer(
            IMessenger messenger,
            string messageaddress,
            bool messageEnabled,
            string layername,
            bool enabled,
            int index,
            double fade,
            string blendMode,
            BeatModifier beatModifier,
            Content content,
            Mask mask,
            Coloration coloration)
        {

            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            MessageAddress = messageaddress;
            MessageEnabled = messageEnabled;
            LayerName = layername;
            Index = index;
            Enabled = enabled;
            Fade = fade;
            BlendMode = blendMode;
            BeatModifier = beatModifier ?? throw new ArgumentNullException(nameof(beatModifier));
            Content = content ?? throw new ArgumentNullException(nameof(content));
            Mask = mask ?? throw new ArgumentNullException(nameof(mask));
            Coloration = coloration ?? throw new ArgumentNullException(nameof(coloration));
        }

        private IMessenger Messenger { get; }

        public string MessageAddress { get; set; }

        public bool MessageEnabled { get; set; }

        private string _layername;
        public string LayerName
        {
            get => _layername;
            set => SetAndNotify(ref _layername, value);
        }

        private bool _enabled;
        public bool Enabled
        {
            get => _enabled;
            set => SetAndNotify(ref _enabled, value);
        }

        private int _index;
        public int Index
        {
            get => _index;
            set => SetAndNotify(ref _index, value);
        }

        private double _fade;
        [OSC]
        public double Fade
        {
            get => _fade;
            set
            {
                SetAndNotify(ref _fade, value);
                if(MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Fade), Fade);
            }
        }

        private string _blendMode;
        [OSC]
        public string BlendMode
        {
            get => _blendMode;
            set
            {
                SetAndNotify(ref _blendMode, value);
                if(MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(BlendMode), BlendMode);
            }
        }

        public BeatModifier BeatModifier { get; }

        public Content Content { get; }

        public Mask Mask { get; }

        public Coloration Coloration { get; }
    }
}