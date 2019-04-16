using System;
using System.Windows;
using System.Windows.Input;
using CMiX.Services;
using CMiX.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Memento;

namespace CMiX.ViewModels
{
    public class Texture : ViewModel
    {
        #region CONSTRUCTORS
        public Texture(string layername, ObservableCollection<OSCMessenger> messengers, Mementor mementor)
            : this
            (
                mementor: mementor,
                messengers: messengers,
                fileselector: new FileSelector("Extended", new List<string> { ".PNG", ".JPG", ".MOV" }, messengers, String.Format("{0}/{1}/", layername, nameof(Texture)), mementor),
                brightness: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Brightness"), messengers, mementor),
                contrast: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Contrast"), messengers, mementor),
                invert: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Invert"), messengers, mementor),
                invertMode: ((TextureInvertMode)0).ToString(),
                hue: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Hue"), messengers, mementor),
                saturation: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Saturation"), messengers, mementor),
                luminosity: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Luminosity"), messengers, mementor),
                keying: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Keying"), messengers, mementor),
                scale: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Scale"), messengers, mementor),
                rotate: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Rotate"), messengers, mementor),
                pan: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Pan"), messengers, mementor),
                tilt: new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Tilt"), messengers, mementor),
                messageaddress: String.Format("{0}/{1}/", layername, nameof(Texture))
            )
        { }

        public Texture
            (
                Mementor mementor,
                ObservableCollection<OSCMessenger> messengers,
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
            : base (messengers)
        {
            Messengers = messengers ?? throw new ArgumentNullException(nameof(messengers));
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

            Mementor = mementor;
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
                if(Mementor != null)
                    Mementor.PropertyChange(this, "InvertMode");
                SetAndNotify(ref _invertMode, value);
                SendMessages(MessageAddress + nameof(InvertMode), InvertMode);
            }
        }
        #endregion

        #region COPY/PASTE/RESET
        public void Copy(TextureDTO texturedto)
        {
            FileSelector.Copy(texturedto.FileSelector);
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
            DisabledMessages();

            FileSelector.Paste(texturedto.FileSelector);
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

            EnabledMessages();
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

                QueueObjects(this);
                SendQueues();
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