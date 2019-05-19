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
        public Texture(string layername, ObservableCollection<OSCMessenger> oscmessengers, Mementor mementor)
        {
            MessageAddress = String.Format("{0}/{1}/", layername, nameof(Texture));
            FileSelector = new FileSelector("Extended", new List<string> { ".PNG", ".JPG", ".MOV", ".TXT" }, oscmessengers, String.Format("{0}/{1}/", layername, nameof(Texture)), mementor);
            Brightness = new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Brightness"), oscmessengers, mementor);
            Contrast = new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Contrast"), oscmessengers, mementor);
            Invert = new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Invert"), oscmessengers, mementor);
            InvertMode = ((TextureInvertMode)0).ToString();
            Hue = new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Hue"), oscmessengers, mementor);
            Saturation = new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Saturation"), oscmessengers, mementor);
            Luminosity = new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Luminosity"), oscmessengers, mementor);
            Keying = new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Keying"), oscmessengers, mementor);
            Scale = new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Scale"), oscmessengers, mementor);
            Rotate = new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Rotate"), oscmessengers, mementor);
            Pan = new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Pan"), oscmessengers, mementor);
            Tilt = new Slider(String.Format("{0}/{1}/{2}", layername, nameof(Texture), "Tilt"), oscmessengers, mementor);
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