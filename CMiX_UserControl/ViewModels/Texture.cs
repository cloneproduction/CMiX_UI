﻿using System;
using System.Collections.ObjectModel;
using CMiX.Services;
using CMiX.Controls;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace CMiX.ViewModels
{
    public class Texture : ViewModel, IMessengerData
    {
        public Texture(string layername, IMessenger messenger)
            : this(
                  messenger: messenger,
                  messageaddress : String.Format("{0}/{1}/", layername, nameof(Texture)),
                  messageEnabled : true,
                  texturePaths: new ObservableCollection<ListBoxFileName>(),
                  brightness: 0.0,
                  contrast: 0.0,
                  saturation: 0.0,
                  keying: 0.0,
                  invert: 0.0,
                  invertMode: ((TextureInvertMode)0).ToString())
        {
            TexturePaths = new ObservableCollection<ListBoxFileName>();
            TexturePaths.CollectionChanged += ContentCollectionChanged;
        }

        public Texture(
            IEnumerable<ListBoxFileName> texturePaths,
            IMessenger messenger,
            string messageaddress,
            bool messageEnabled,
            double brightness,
            double contrast,
            double saturation,
            double keying,
            double invert,
            string invertMode)
        {
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            MessageAddress = messageaddress;
            MessageEnabled = messageEnabled;

            TexturePaths = new ObservableCollection<ListBoxFileName>();
            TexturePaths.CollectionChanged += ContentCollectionChanged;

            AssertNotNegative(() => brightness);
            AssertNotNegative(() => contrast);
            AssertNotNegative(() => saturation);
            AssertNotNegative(() => keying);

            AssertNotNegative(() => invert);
            Brightness = brightness;
            Contrast = contrast;
            Saturation = saturation;
            Keying = keying;
            Invert = invert;
            InvertMode = invertMode;
        }

        public string MessageAddress { get; set; }

        public bool MessageEnabled { get; set; }

        public IMessenger Messenger { get; }

        [OSC]
        public ObservableCollection<ListBoxFileName> TexturePaths { get; }

        private double _brightness;
        [OSC]
        public double Brightness
        {
            get => _brightness;
            set
            {
                SetAndNotify(ref _brightness, CoerceNotNegative(value));
                if(MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Brightness), Brightness);
            }
        }

        private double _contrast;
        [OSC]
        public double Contrast
        {
            get => _contrast;
            set
            {
                SetAndNotify(ref _contrast, CoerceNotNegative(value));
                if(MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Contrast), Contrast);
            }
        }

        private double _saturation;
        [OSC]
        public double Saturation
        {
            get => _saturation;
            set
            {
                SetAndNotify(ref _saturation, CoerceNotNegative(value));
                if(MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Saturation), Saturation);
            }
        }

        private double _keying;
        [OSC]
        public double Keying
        {
            get => _keying;
            set
            {
                SetAndNotify(ref _keying, CoerceNotNegative(value));
                if(MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Keying), Keying);
            }
        }

        private double _invert;
        [OSC]
        public double Invert
        {
            get => _invert;
            set
            {
                SetAndNotify(ref _invert, CoerceNotNegative(value));
                if (MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Invert), Invert);
            }
        }

        private string _invertMode;
        [OSC]
        public string InvertMode
        {
            get => _invertMode;
            set
            {
                SetAndNotify(ref _invertMode, value);
                if (MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(InvertMode), InvertMode);
            }
        }

        public void ContentCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (ListBoxFileName item in e.OldItems)
                {
                    //Removed items
                    item.PropertyChanged -= EntityViewModelPropertyChanged;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (ListBoxFileName item in e.NewItems)
                {
                    //Added items
                    item.PropertyChanged += EntityViewModelPropertyChanged;
                }
            }

            List<string> filename = new List<string>();
            foreach (ListBoxFileName lb in TexturePaths)
            {
                if (lb.FileIsSelected == true)
                {
                    filename.Add(lb.FileName);
                }
            }
            if (MessageEnabled)
                Messenger.SendMessage(MessageAddress + nameof(TexturePaths), filename.ToArray());
        }

        public void EntityViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            List<string> filename = new List<string>();
            foreach (ListBoxFileName lb in TexturePaths)
            {
                if (lb.FileIsSelected == true)
                {
                    filename.Add(lb.FileName);
                }
            }
            if (MessageEnabled)
                Messenger.SendMessage(MessageAddress + nameof(TexturePaths), filename.ToArray());
        }
    }
}