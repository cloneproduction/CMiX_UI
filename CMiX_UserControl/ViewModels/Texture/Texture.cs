using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.ViewModels
{
    public class Texture : ViewModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public Texture(string messageaddress, Messenger messenger, Mementor mementor)
        {
            MessageAddress = String.Format("{0}{1}/", messageaddress, nameof(Texture));

            FileSelector = new FileSelector(MessageAddress,  "Single", new List<string> { ".PNG", ".JPG", ".MOV", ".TXT" }, messenger, mementor);
            FileSelector.FilePaths.Add(new FileNameItem(string.Empty, FileSelector.MessageAddress, messenger) { FileIsSelected = true, FileName = "Black (default).png" });
            FileSelector.SelectedFileNameItem = new FileNameItem(string.Empty, FileSelector.MessageAddress, messenger) { FileIsSelected = true, FileName = "Black (default).png" };

            Brightness = new Slider(MessageAddress + nameof(Brightness), messenger, mementor);
            Brightness.Minimum = -1.0;

            Contrast = new Slider(MessageAddress + nameof(Contrast), messenger, mementor);
            Contrast.Minimum = -1.0;

            Invert = new Slider(MessageAddress + nameof(Invert), messenger, mementor);
            InvertMode = ((TextureInvertMode)0).ToString();

            Hue = new Slider(MessageAddress + nameof(Hue), messenger, mementor);
            Hue.Minimum = -1.0;

            Saturation = new Slider(MessageAddress + nameof(Saturation), messenger, mementor);
            Saturation.Minimum = -1.0;

            Luminosity = new Slider(MessageAddress + nameof(Luminosity), messenger, mementor);
            Luminosity.Minimum = -1.0;

            Keying = new Slider(MessageAddress + nameof(Keying), messenger, mementor);

            Scale = new Slider(MessageAddress + nameof(Scale), messenger, mementor);
            Scale.Minimum = -1.0;

            Rotate = new Slider(MessageAddress + nameof(Rotate), messenger, mementor);
            Rotate.Minimum = -1.0;

            Pan = new Slider(MessageAddress + nameof(Pan), messenger, mementor);
            Pan.Minimum = -1.0;

            Tilt = new Slider(MessageAddress + nameof(Tilt), messenger, mementor);
            Tilt.Minimum = -1.0;

            CopyTextureCommand = new RelayCommand(p => CopyTexture());
            PasteTextureCommand = new RelayCommand(p => PasteTexture());
            ResetTextureCommand = new RelayCommand(p => ResetTexture());
        }
        #endregion

        #region METHODS
        public void UpdateMessageAddress(string messageaddress)
        {
            MessageAddress =  messageaddress;

            FileSelector.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(FileSelector)));
            Brightness.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Brightness)));
            Contrast.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Contrast)));
            Invert.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Invert)));
            Hue.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Hue)));
            Saturation.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Saturation)));
            Luminosity.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Luminosity)));
            Keying.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Keying)));
            Scale.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Scale)));
            Rotate.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Rotate)));
            Pan.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Pan)));
            Tilt.UpdateMessageAddress(String.Format("{0}{1}/", MessageAddress, nameof(Tilt)));
        }
        #endregion

        #region PROPERTIES
        public ICommand CopyTextureCommand { get; }
        public ICommand PasteTextureCommand { get; }
        public ICommand ResetTextureCommand { get; }

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
                //SendMessages(MessageAddress + nameof(InvertMode), InvertMode);
            }
        }

        public string MessageAddress { get; set; }
        public Messenger Messenger { get; set; }
        public Mementor Mementor { get; set; }
        #endregion

        #region COPY/PASTE/RESET
        public void Copy(TextureModel texturemodel)
        {
            texturemodel.MessageAddress = MessageAddress;
            FileSelector.Copy(texturemodel.FileSelector);
            Brightness.Copy(texturemodel.Brightness);
            Contrast.Copy(texturemodel.Contrast);
            Saturation.Copy(texturemodel.Saturation);
            Luminosity.Copy(texturemodel.Luminosity);
            Hue.Copy(texturemodel.Hue);
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
            Messenger.Disable();

            MessageAddress = texturemodel.MessageAddress;
            FileSelector.Paste(texturemodel.FileSelector);
            Brightness.Paste(texturemodel.Brightness);
            Contrast.Paste(texturemodel.Contrast);
            Saturation.Paste(texturemodel.Saturation);
            Luminosity.Paste(texturemodel.Luminosity);
            Hue.Paste(texturemodel.Hue);
            Pan.Paste(texturemodel.Pan);
            Tilt.Paste(texturemodel.Tilt);
            Scale.Paste(texturemodel.Scale);
            Rotate.Paste(texturemodel.Rotate);
            Keying.Paste(texturemodel.Keying);
            Invert.Paste(texturemodel.Invert);
            InvertMode = texturemodel.InvertMode;

            Messenger.Enable();
        }


        public void Reset()
        {
            Messenger.Disable();

            InvertMode = ((TextureInvertMode)0).ToString();
            FileSelector.Reset();
            Brightness.Reset();
            Contrast.Reset();
            Invert.Reset();
            Hue.Reset();
            Saturation.Reset();
            Luminosity.Reset();
            Keying.Reset();
            Scale.Reset();
            Rotate.Reset();
            Pan.Reset();
            Tilt.Reset();

            Messenger.Enable();
        }

        public void CopyTexture()
        {
            TextureModel texturemodel = new TextureModel();
            this.Copy(texturemodel);
            IDataObject data = new DataObject();
            data.SetData("TextureModel", texturemodel, false);
            Clipboard.SetDataObject(data);
        }

        public void PasteTexture()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("TextureModel"))
            {
                Mementor.BeginBatch();
                Messenger.Disable();

                var texturemodel = data.GetData("TextureModel") as TextureModel;
                var texturemessageaddress = MessageAddress;
                this.Paste(texturemodel);
                UpdateMessageAddress(texturemessageaddress);
                this.Copy(texturemodel);

                Messenger.Enable();
                Mementor.EndBatch();
                //SendMessages(nameof(TextureModel), texturemodel);
            }
        }

        public void ResetTexture()
        {
            TextureModel texturemodel = new TextureModel();
            this.Reset();
            this.Copy(texturemodel);
            //SendMessages(nameof(TextureModel), texturemodel);
        }
        #endregion
    }
}