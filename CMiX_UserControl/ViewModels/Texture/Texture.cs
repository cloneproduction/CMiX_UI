using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using CMiX.MVVM;

namespace CMiX.Studio.ViewModels
{
    public class Texture : ViewModel, ICopyPasteModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public Texture(string messageaddress, Messenger messenger, Mementor mementor)
        {
            MessageAddress = $"{messageaddress}{nameof(Texture)}/";

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

            FileSelector.UpdateMessageAddress($"{MessageAddress}{nameof(FileSelector)}/");
            Brightness.UpdateMessageAddress($"{MessageAddress}{nameof(Brightness)}/");
            Contrast.UpdateMessageAddress($"{MessageAddress}{nameof(Contrast)}/");
            Invert.UpdateMessageAddress($"{MessageAddress}{nameof(Invert)}/");
            Hue.UpdateMessageAddress($"{MessageAddress}{nameof(Hue)}/");
            Saturation.UpdateMessageAddress($"{MessageAddress}{nameof(Saturation)}/");
            Luminosity.UpdateMessageAddress($"{MessageAddress}{nameof(Luminosity)}/");
            Keying.UpdateMessageAddress($"{MessageAddress}{nameof(Keying)}/");
            Scale.UpdateMessageAddress($"{MessageAddress}{nameof(Scale)}/");
            Rotate.UpdateMessageAddress($"{MessageAddress}{nameof(Rotate)}/");
            Pan.UpdateMessageAddress($"{MessageAddress}{nameof(Pan)}/");
            Tilt.UpdateMessageAddress($"{MessageAddress}{nameof(Tilt)}/");
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
        public void CopyModel(IModel model)
        {
            TextureModel textureModel = model as TextureModel;
            textureModel.MessageAddress = MessageAddress;
            FileSelector.CopyModel(textureModel.FileSelector);
            Brightness.CopyModel(textureModel.Brightness);
            Contrast.CopyModel(textureModel.Contrast);
            Saturation.CopyModel(textureModel.Saturation);
            Luminosity.CopyModel(textureModel.Luminosity);
            Hue.CopyModel(textureModel.Hue);
            Pan.CopyModel(textureModel.Pan);
            Tilt.CopyModel(textureModel.Tilt);
            Scale.CopyModel(textureModel.Scale);
            Rotate.CopyModel(textureModel.Rotate);
            Keying.CopyModel(textureModel.Keying);
            Invert.CopyModel(textureModel.Invert);
            textureModel.InvertMode = InvertMode;
        }

        public void PasteModel(IModel model)
        {
            Messenger.Disable();

            TextureModel textureModel = model as TextureModel;
            MessageAddress = textureModel.MessageAddress;
            FileSelector.PasteModel(textureModel.FileSelector);
            Brightness.PasteModel(textureModel.Brightness);
            Contrast.PasteModel(textureModel.Contrast);
            Saturation.PasteModel(textureModel.Saturation);
            Luminosity.PasteModel(textureModel.Luminosity);
            Hue.PasteModel(textureModel.Hue);
            Pan.PasteModel(textureModel.Pan);
            Tilt.PasteModel(textureModel.Tilt);
            Scale.PasteModel(textureModel.Scale);
            Rotate.PasteModel(textureModel.Rotate);
            Keying.PasteModel(textureModel.Keying);
            Invert.PasteModel(textureModel.Invert);
            InvertMode = textureModel.InvertMode;

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
            TextureModel textureModel = new TextureModel();
            this.CopyModel(textureModel);
            IDataObject data = new DataObject();
            data.SetData("TextureModel", textureModel, false);
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
                this.PasteModel(texturemodel);
                UpdateMessageAddress(texturemessageaddress);
                this.CopyModel(texturemodel);

                Messenger.Enable();
                Mementor.EndBatch();
                //SendMessages(nameof(TextureModel), texturemodel);
            }
        }

        public void ResetTexture()
        {
            TextureModel texturemodel = new TextureModel();
            this.Reset();
            this.CopyModel(texturemodel);
            //SendMessages(nameof(TextureModel), texturemodel);
        }
        #endregion
    }
}