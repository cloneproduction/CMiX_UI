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
            : base (oscmessengers, mementor)
        {
            MessageAddress = String.Format("{0}{1}/", layername, nameof(Texture));

            FileSelector = new FileSelector(MessageAddress,  "Extended", new List<string> { ".PNG", ".JPG", ".MOV", ".TXT" }, oscmessengers, mementor);

            Brightness = new Slider(MessageAddress + nameof(Brightness), oscmessengers, mementor);
            Contrast = new Slider(MessageAddress + nameof(Contrast), oscmessengers, mementor);
            Invert = new Slider(MessageAddress + nameof(Invert), oscmessengers, mementor);
            InvertMode = ((TextureInvertMode)0).ToString();
            Hue = new Slider(MessageAddress + nameof(Hue), oscmessengers, mementor);
            Saturation = new Slider(MessageAddress + nameof(Saturation), oscmessengers, mementor);
            Luminosity = new Slider(MessageAddress + nameof(Luminosity), oscmessengers, mementor);
            Keying = new Slider(MessageAddress + nameof(Keying), oscmessengers, mementor);
            Scale = new Slider(MessageAddress + nameof(Scale), oscmessengers, mementor);
            Rotate = new Slider(MessageAddress + nameof(Rotate), oscmessengers, mementor);
            Pan = new Slider(MessageAddress + nameof(Pan), oscmessengers, mementor);
            Tilt = new Slider(MessageAddress + nameof(Tilt), oscmessengers, mementor);

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
        public string InvertMode
        {
            get => _invertMode;
            set
            {
                if(Mementor != null)
                    Mementor.PropertyChange(this, nameof(InvertMode));
                SetAndNotify(ref _invertMode, value);
                SendMessages(MessageAddress + nameof(InvertMode), InvertMode);
            }
        }
        #endregion

        #region COPY/PASTE/RESET
        public void Copy(TextureModel texturemodel)
        {
            texturemodel.MessageAddress = MessageAddress;
            FileSelector.Copy(texturemodel.FileSelector);
            Brightness.Copy(texturemodel.Brightness);
            Contrast.Copy(texturemodel.Contrast);
            Saturation.Copy(texturemodel.Saturation);
            Pan.Copy(texturemodel.Pan);
            Tilt.Copy(texturemodel.Tilt);
            Scale.Copy(texturemodel.Scale);
            Rotate.Copy(texturemodel.Rotate);
            Keying.Copy(texturemodel.Keying);
            Invert.Copy(texturemodel.Invert);
            texturemodel.InvertMode = InvertMode;
        }

        public void Paste(TextureModel texturemodel)
        {
            DisabledMessages();

            MessageAddress = texturemodel.MessageAddress;
            FileSelector.Paste(texturemodel.FileSelector);
            Brightness.Paste(texturemodel.Brightness);
            Contrast.Paste(texturemodel.Contrast);
            Saturation.Paste(texturemodel.Saturation);
            Pan.Paste(texturemodel.Pan);
            Tilt.Paste(texturemodel.Tilt);
            Scale.Paste(texturemodel.Scale);
            Rotate.Paste(texturemodel.Rotate);
            Keying.Paste(texturemodel.Keying);
            Invert.Paste(texturemodel.Invert);
            InvertMode = texturemodel.InvertMode;

            EnabledMessages();
        }

        public void CopySelf()
        {
            TextureModel texturemodel = new TextureModel();
            this.Copy(texturemodel);
            IDataObject data = new DataObject();
            data.SetData("Texture", texturemodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteSelf()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("Texture"))
            {
                var texturemodel = (TextureModel)data.GetData("Texture") as TextureModel;
                this.Paste(texturemodel);

                QueueObjects(this);
                SendQueues();
            }
        }

        public void ResetSelf()
        {
            TextureModel texturemodel = new TextureModel();
            this.Paste(texturemodel);
        }
        #endregion
    }
}