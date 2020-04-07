using System.Windows;
using System.Windows.Input;
using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class Texture : ViewModel, ISendable, IUndoable
    {
        #region CONSTRUCTORS
        public Texture(string messageaddress, MessageService messageService, Mementor mementor)
        {
            MessageAddress = $"{messageaddress}{nameof(Texture)}/";
            MessageService = messageService;

            AssetSelector = new AssetSelector(MessageAddress, messageService, mementor);

            Brightness = new Slider(MessageAddress + nameof(Brightness), messageService, mementor);
            Brightness.Minimum = -1.0;

            Contrast = new Slider(MessageAddress + nameof(Contrast), messageService, mementor);
            Contrast.Minimum = -1.0;

            Invert = new Slider(MessageAddress + nameof(Invert), messageService, mementor);
            InvertMode = ((TextureInvertMode)0).ToString();

            Hue = new Slider(MessageAddress + nameof(Hue), messageService, mementor);
            Hue.Minimum = -1.0;

            Saturation = new Slider(MessageAddress + nameof(Saturation), messageService, mementor);
            Saturation.Minimum = -1.0;

            Luminosity = new Slider(MessageAddress + nameof(Luminosity), messageService, mementor);
            Luminosity.Minimum = -1.0;

            Keying = new Slider(MessageAddress + nameof(Keying), messageService, mementor);

            Scale = new Slider(MessageAddress + nameof(Scale), messageService, mementor);
            Scale.Minimum = -1.0;

            Rotate = new Slider(MessageAddress + nameof(Rotate), messageService, mementor);
            Rotate.Minimum = -1.0;

            Pan = new Slider(MessageAddress + nameof(Pan), messageService, mementor);
            Pan.Minimum = -1.0;

            Tilt = new Slider(MessageAddress + nameof(Tilt), messageService, mementor);
            Tilt.Minimum = -1.0;

            CopyTextureCommand = new RelayCommand(p => CopyTexture());
            PasteTextureCommand = new RelayCommand(p => PasteTexture());
            ResetTextureCommand = new RelayCommand(p => ResetTexture());
        }
        #endregion

        #region PROPERTIES
        public ICommand CopyTextureCommand { get; }
        public ICommand PasteTextureCommand { get; }
        public ICommand ResetTextureCommand { get; }

        public AssetSelector AssetSelector { get; set; }
        public TextureItem TextureItem { get; set; }

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
        public MessageService MessageService { get; set; }
        public Mementor Mementor { get; set; }
        #endregion

        #region COPY/PASTE/RESET
        public TextureModel GetModel()
        {
            TextureModel textureModel = new TextureModel();

            textureModel.AssetSelectorModel = AssetSelector.GetModel();
            textureModel.Brightness = Brightness.GetModel();
            textureModel.Contrast = Contrast.GetModel();
            textureModel.Saturation = Saturation.GetModel();
            textureModel.Luminosity = Luminosity.GetModel();
            textureModel.Hue = Hue.GetModel();
            textureModel.Pan = Pan.GetModel();
            textureModel.Tilt = Tilt.GetModel();
            textureModel.Scale = Scale.GetModel();
            textureModel.Rotate = Rotate.GetModel();
            textureModel.Keying = Keying.GetModel();
            textureModel.Invert = Invert.GetModel();
            textureModel.InvertMode = InvertMode;
            return textureModel;
        }

        public void SetViewModel(TextureModel textureModel)
        {
            MessageService.Disable();

            AssetSelector.SetViewModel(textureModel.AssetSelectorModel);
            Brightness.SetViewModel(textureModel.Brightness);
            Contrast.SetViewModel(textureModel.Contrast);
            Saturation.SetViewModel(textureModel.Saturation);
            Luminosity.SetViewModel(textureModel.Luminosity);
            Hue.SetViewModel(textureModel.Hue);
            Pan.SetViewModel(textureModel.Pan);
            Tilt.SetViewModel(textureModel.Tilt);
            Scale.SetViewModel(textureModel.Scale);
            Rotate.SetViewModel(textureModel.Rotate);
            Keying.SetViewModel(textureModel.Keying);
            Invert.SetViewModel(textureModel.Invert);
            InvertMode = textureModel.InvertMode;

            MessageService.Enable();
        }


        public void Reset()
        {
            MessageService.Disable();

            InvertMode = ((TextureInvertMode)0).ToString();
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

            MessageService.Enable();
        }

        public void CopyTexture()
        {
            IDataObject data = new DataObject();
            data.SetData("TextureModel", GetModel(), false);
            Clipboard.SetDataObject(data);
        }

        public void PasteTexture()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("TextureModel"))
            {
                Mementor.BeginBatch();
                MessageService.Disable();

                var texturemodel = data.GetData("TextureModel") as TextureModel;
                this.SetViewModel(texturemodel);

                MessageService.Enable();
                Mementor.EndBatch();
                //SendMessages(nameof(TextureModel), GetModel());
            }
        }

        public void ResetTexture()
        {
            this.Reset();
            //SendMessages(nameof(TextureModel), GetModel());
        }
        #endregion
    }
}