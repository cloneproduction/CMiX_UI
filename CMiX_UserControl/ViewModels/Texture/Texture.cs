using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using CMiX.Services;
using CMiX.Controls;
using CMiX.Models;
using GuiLabs.Undo;

namespace CMiX.ViewModels
{
    public class Texture : ViewModel, IMessengerData
    {
        #region CONSTRUCTORS
        public Texture(string layername, IMessenger messenger, ActionManager actionmanager)
            : this
            (
                actionmanager: actionmanager,
                texturePaths: new ObservableCollection<ListBoxFileName>(),
                brightness: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Brightness"), messenger, actionmanager),
                contrast: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Contrast"), messenger, actionmanager),
                invert: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Invert"), messenger, actionmanager),
                invertMode: ((TextureInvertMode)0).ToString(),
                hue: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Hue"), messenger, actionmanager),
                saturation: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Saturation"), messenger, actionmanager),
                luminosity: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Luminosity"), messenger, actionmanager),
                keying: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Keying"), messenger, actionmanager),
                scale: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Scale"), messenger, actionmanager),
                rotate: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Rotate"), messenger, actionmanager),
                pan: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Pan"), messenger, actionmanager),
                tilt: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Tilt"), messenger, actionmanager),
                messenger: messenger,
                messageaddress: String.Format("{0}/{1}/", layername, nameof(Texture)),
                messageEnabled: true
            )
        {
            TexturePaths = new ObservableCollection<ListBoxFileName>();
            TexturePaths.CollectionChanged += ContentCollectionChanged;
        }

        public Texture
            (
                IEnumerable<ListBoxFileName> texturePaths,
                Slider brightness,
                Slider contrast,
                Slider keying,
                Slider invert,
                string invertMode,
                Slider hue,
                Slider saturation,
                Slider luminosity,
                Slider scale,
                Slider rotate,
                Slider pan,
                Slider tilt,
                IMessenger messenger,
                string messageaddress,
                bool messageEnabled,
                ActionManager actionmanager
            )
            : base (actionmanager)
        {
            TexturePaths = new ObservableCollection<ListBoxFileName>();
            TexturePaths.CollectionChanged += ContentCollectionChanged;
            Brightness = brightness;
            Contrast = contrast;
            Hue = hue;
            Saturation = saturation;
            Luminosity = luminosity;
            Keying = keying;
            Invert = invert;
            InvertMode = invertMode;
            Scale = scale;
            Rotate = rotate;
            Pan = pan;
            Tilt = tilt;

            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            MessageAddress = messageaddress;
            MessageEnabled = messageEnabled;

            CopySelfCommand = new RelayCommand(p => CopySelf());
            PasteSelfCommand = new RelayCommand(p => PasteSelf());
            ResetSelfCommand = new RelayCommand(p => ResetSelf());
        }
        #endregion

        #region PROPERTIES
        public string MessageAddress { get; set; }

        public bool MessageEnabled { get; set; }

        public IMessenger Messenger { get; }

        public ICommand CopySelfCommand { get; }
        public ICommand PasteSelfCommand { get; }
        public ICommand ResetSelfCommand { get; }

        [OSC]
        public ObservableCollection<ListBoxFileName> TexturePaths { get; }

        public Slider Brightness { get; }
        public Slider Contrast { get; }
        public Slider Invert { get; }

        public Slider Hue { get; }
        public Slider Saturation { get; }
        public Slider Luminosity { get; }
        public Slider Keying { get; }

        public Slider Pan { get; }
        public Slider Tilt { get; }

        public Slider Scale { get; }
        public Slider Rotate { get; }


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
        #endregion

        #region COLLECTIONCHANGE
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
        #endregion

        #region COPY/PASTE/RESET
        public void Copy(TextureDTO texturedto)
        {
            foreach (ListBoxFileName lbfn in TexturePaths)
            {
                texturedto.TexturePaths.Add(lbfn);
            }

            /*texturedto.Brightness = Brightness;
            texturedto.Contrast = Contrast;
            texturedto.Saturation = Saturation;

            texturedto.Pan = Pan;
            texturedto.Tilt = Tilt;
            texturedto.Scale = Scale;
            texturedto.Rotate = Rotate;

            texturedto.Keying = Keying;
            texturedto.Invert = Invert;*/
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

            /*Brightness = texturedto.Brightness;
            Contrast = texturedto.Contrast;
            Saturation = texturedto.Saturation;

            Pan = texturedto.Pan;
            Tilt = texturedto.Tilt;
            Scale = texturedto.Scale;
            Rotate = texturedto.Rotate;

            Keying = texturedto.Keying;
            Invert = texturedto.Invert;*/
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
        #endregion
    }
}