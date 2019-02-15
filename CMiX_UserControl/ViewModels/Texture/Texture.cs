using System;
using System.Windows;
using System.Windows.Input;
using CMiX.Services;
using CMiX.Models;
using GuiLabs.Undo;

namespace CMiX.ViewModels
{
    public class Texture : ViewModel
    {
        #region CONSTRUCTORS
        public Texture(string layername, OSCMessenger messenger, ActionManager actionmanager)
            : this
            (
                actionmanager: actionmanager,
                messenger: messenger,
                fileselector: new FileSelector(messenger, String.Format("{0}/{1}/", layername, nameof(Texture)), actionmanager),
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
                messageaddress: String.Format("{0}/{1}/", layername, nameof(Texture))
            )
        { }

        public Texture
            (
                ActionManager actionmanager,
                OSCMessenger messenger,
                FileSelector fileselector,
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
                string messageaddress
            )
            : base (actionmanager)
        {
            Messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));
            FileSelector = fileselector;
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
            MessageAddress = messageaddress;
            CopySelfCommand = new RelayCommand(p => CopySelf());
            PasteSelfCommand = new RelayCommand(p => PasteSelf());
            ResetSelfCommand = new RelayCommand(p => ResetSelf());
        }
        #endregion

        #region PROPERTIES
        public ICommand CopySelfCommand { get; }
        public ICommand PasteSelfCommand { get; }
        public ICommand ResetSelfCommand { get; }

        public FileSelector FileSelector { get;  }
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
                Messenger.SendMessage(MessageAddress + nameof(InvertMode), InvertMode);
            }
        }
        #endregion

        #region COPY/PASTE/RESET
        public void Copy(TextureDTO texturedto)
        {
            Brightness.Copy(texturedto.Brightness);
            Contrast.Copy(texturedto.Contrast);
            Saturation.Copy(texturedto.Saturation);
            Pan.Copy(texturedto.Pan);
            Tilt.Copy(texturedto.Tilt);
            Scale.Copy(texturedto.Scale);
            Rotate.Copy(texturedto.Rotate);
            Keying.Copy(texturedto.Keying);
            Invert.Copy(texturedto.Invert);
            texturedto.InvertMode = InvertMode;
        }

        public void Paste(TextureDTO texturedto)
        {
            Messenger.SendEnabled = false;

            Brightness.Paste(texturedto.Brightness);
            Contrast.Paste(texturedto.Contrast);
            Saturation.Paste(texturedto.Saturation);
            Pan.Paste(texturedto.Pan);
            Tilt.Paste(texturedto.Tilt);
            Scale.Paste(texturedto.Scale);
            Rotate.Paste(texturedto.Rotate);
            Keying.Paste(texturedto.Keying);
            Invert.Paste(texturedto.Invert);
            InvertMode = texturedto.InvertMode;

            Messenger.SendEnabled = true;
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