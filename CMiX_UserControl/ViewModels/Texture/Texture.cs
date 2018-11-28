using System;
using System.Collections.ObjectModel;
using CMiX.Services;
using CMiX.Controls;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using CMiX.Models;
using System.Windows.Input;
using System.Windows;

namespace CMiX.ViewModels
{
    public class Texture : ViewModel, IMessengerData
    {
        public Texture(string layername, IMessenger messenger)
            : this(
                  texturePaths: new ObservableCollection<ListBoxFileName>(),
                  brightness: 0.0,
                  contrast: 0.0,
                  saturation: 0.0,
                  keying: 0.0,
                  invert: 0.0,
                  invertMode: ((TextureInvertMode)0).ToString(),

                  messenger: messenger,
                  messageaddress: String.Format("{0}/{1}/", layername, nameof(Texture)),
                  messageEnabled: true
                  )
        {
            TexturePaths = new ObservableCollection<ListBoxFileName>();
            TexturePaths.CollectionChanged += ContentCollectionChanged;
        }

        public Texture(
            IEnumerable<ListBoxFileName> texturePaths,
            double brightness,
            double contrast,
            double saturation,
            double keying,
            double invert,
            string invertMode,
            IMessenger messenger,
            string messageaddress,
            bool messageEnabled
            )
        {
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

            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            MessageAddress = messageaddress;
            MessageEnabled = messageEnabled;

            CopySelfCommand = new RelayCommand(p => CopySelf());
            PasteSelfCommand = new RelayCommand(p => PasteSelf());
            ResetSelfCommand = new RelayCommand(p => ResetSelf());
        }

        public string MessageAddress { get; set; }

        public bool MessageEnabled { get; set; }

        public IMessenger Messenger { get; }

        public ICommand CopySelfCommand { get; }
        public ICommand PasteSelfCommand { get; }
        public ICommand ResetSelfCommand { get; }

        [OSC]
        public ObservableCollection<ListBoxFileName> TexturePaths { get; }


        private double _brightness;
        [OSC]
        public double Brightness
        {
            get => _brightness;
            set
            {
                SetAndNotify(ref _brightness, value);
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
                SetAndNotify(ref _contrast, value);
                if(MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Contrast), Contrast);
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


        private double _hue;
        [OSC]
        public double Hue
        {
            get => _hue;
            set
            {
                SetAndNotify(ref _hue, value);
                if(MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Hue), Hue);
            }
        }

        private double _saturation;
        [OSC]
        public double Saturation
        {
            get => _saturation;
            set
            {
                SetAndNotify(ref _saturation, value);
                if (MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Saturation), Saturation);
            }
        }

        private double _luminosity;
        [OSC]
        public double Luminosity
        {
            get => _luminosity;
            set
            {
                SetAndNotify(ref _luminosity, value);
                if (MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Luminosity), Luminosity);
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


        private double _pan;
        [OSC]
        public double Pan
        {
            get => _pan;
            set
            {
                SetAndNotify(ref _pan, value);
                if (MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Pan), Pan);
            }
        }

        private double _tilt;
        [OSC]
        public double Tilt
        {
            get => _tilt;
            set
            {
                SetAndNotify(ref _tilt, value);
                if (MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Tilt), Tilt);
            }
        }

        private double _scale;
        [OSC]
        public double Scale
        {
            get => _scale;
            set
            {
                SetAndNotify(ref _scale, value);
                if (MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Scale), Scale);
            }
        }

        private double _rotate;
        [OSC]
        public double Rotate
        {
            get => _rotate;
            set
            {
                SetAndNotify(ref _rotate, value);
                if (MessageEnabled)
                    Messenger.SendMessage(MessageAddress + nameof(Rotate), Rotate);
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

        public void Copy(TextureDTO texturedto)
        {
            foreach (ListBoxFileName lbfn in TexturePaths)
            {
                texturedto.TexturePaths.Add(lbfn);
            }

            texturedto.Brightness = Brightness;
            texturedto.Contrast = Contrast;
            texturedto.Saturation = Saturation;

            texturedto.Pan = Pan;
            texturedto.Tilt = Tilt;
            texturedto.Scale = Scale;
            texturedto.Rotate = Rotate;

            texturedto.Keying = Keying;
            texturedto.Invert = Invert;
            texturedto.InvertMode = InvertMode;
        }

        public void Paste(TextureDTO texturedto)
        {
            MessageEnabled = false;

            TexturePaths.Clear();
            foreach (ListBoxFileName lbfn in texturedto.TexturePaths)
            {
                TexturePaths.Add(lbfn);
            }

            Brightness = texturedto.Brightness;
            Contrast = texturedto.Contrast;
            Saturation = texturedto.Saturation;

            Pan = texturedto.Pan;
            Tilt = texturedto.Tilt;
            Scale = texturedto.Scale;
            Rotate = texturedto.Rotate;

            Keying = texturedto.Keying;
            Invert = texturedto.Invert;
            InvertMode = texturedto.InvertMode;

            MessageEnabled = true;
        }

        public void CopySelf()
        {
            TextureDTO texturedto = new TextureDTO();
            this.Copy(texturedto);
            IDataObject data = new DataObject();
            data.SetData("Texture", texturedto, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteSelf()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("Texture"))
            {
                var texturedto = (TextureDTO)data.GetData("Texture") as TextureDTO;
                this.Paste(texturedto);

                Messenger.QueueObject(this);
                Messenger.SendQueue();
            }
        }

        public void ResetSelf()
        {
            TextureDTO texturedto = new TextureDTO();
            this.Paste(texturedto);
        }
    }
}