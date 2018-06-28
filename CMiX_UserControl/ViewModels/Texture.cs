using System;
using System.Collections.ObjectModel;
using CMiX.Services;
using CMiX.Controls;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace CMiX.ViewModels
{
    public class Texture : ViewModel
    {
        public Texture(string layername, IMessenger messenger)
            : this(
                  layerName: layername,
                  messenger: messenger,
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
            string layerName,
            IEnumerable<ListBoxFileName> texturePaths,
            IMessenger messenger,
            double brightness,
            double contrast,
            double saturation,
            double keying,
            double invert,
            string invertMode)
        {
            AssertNotNegative(() => brightness);
            AssertNotNegative(() => contrast);
            AssertNotNegative(() => saturation);
            AssertNotNegative(() => keying);
            AssertNotNegative(() => invert);

            LayerName = layerName;
            TexturePaths = new ObservableCollection<ListBoxFileName>();
            TexturePaths.CollectionChanged += ContentCollectionChanged;
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            Brightness = brightness;
            Contrast = contrast;
            Saturation = saturation;
            Keying = keying;
            Invert = invert;
            InvertMode = invertMode;
        }

        private string _layerName;
        public string LayerName
        {
            get => _layerName;
            set => SetAndNotify(ref _layerName, value);
        }


        public IMessenger Messenger { get; }

        [OSC(OSCType.ADRESS)]
        public string Address => String.Format("{0}/{1}/", LayerName, nameof(Texture));

        [OSC(OSCType.DATA)]
        public ObservableCollection<ListBoxFileName> TexturePaths { get; }

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
            Messenger.SendMessage(Address + nameof(TexturePaths), filename.ToArray());
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
            Messenger.SendMessage(Address + nameof(TexturePaths), filename.ToArray());
        }


        private double _brightness;
        [OSC(OSCType.DATA)]
        public double Brightness
        {
            get => _brightness;
            set
            {
                SetAndNotify(ref _brightness, CoerceNotNegative(value));
                Messenger.SendMessage(Address + nameof(Brightness), Brightness);
            }
        }

        private double _contrast;
        [OSC(OSCType.DATA)]
        public double Contrast
        {
            get => _contrast;
            set
            {
                SetAndNotify(ref _contrast, CoerceNotNegative(value));
                Messenger.SendMessage(Address + nameof(Contrast), Contrast);
            }
        }

        private double _saturation;
        [OSC(OSCType.DATA)]
        public double Saturation
        {
            get => _saturation;
            set
            {
                SetAndNotify(ref _saturation, CoerceNotNegative(value));
                Messenger.SendMessage(Address + nameof(Saturation), Saturation);
            }
        }

        private double _keying;
        [OSC(OSCType.DATA)]
        public double Keying
        {
            get => _keying;
            set
            {
                SetAndNotify(ref _keying, CoerceNotNegative(value));
                Messenger.SendMessage(Address + nameof(Keying), Keying);
            }
        }

        private double _invert;
        [OSC(OSCType.DATA)]
        public double Invert
        {
            get => _invert;
            set
            {
                SetAndNotify(ref _invert, CoerceNotNegative(value));
                Messenger.SendMessage(Address + nameof(Invert), Invert);
            }
        }

        private string _invertMode;
        [OSC(OSCType.DATA)]
        public string InvertMode
        {
            get => _invertMode;
            set
            {
                SetAndNotify(ref _invertMode, value);
                Messenger.SendMessage(Address + nameof(InvertMode), InvertMode);
            }
        }
    }
}
