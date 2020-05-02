using System.Windows;
using System.Windows.Input;
using Memento;
using CMiX.MVVM.ViewModels;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.Studio.ViewModels
{
    public class Texture : ViewModel, IUndoable
    {
        #region CONSTRUCTORS
        public Texture()
        {
            //MessageAddress = $"{messageaddress}{nameof(Texture)}/";

            AssetPathSelector = new AssetPathSelector<AssetTexture>(MessageAddress);

            Brightness = new Slider(MessageAddress + nameof(Brightness));
            Brightness.Minimum = -1.0;

            Contrast = new Slider(MessageAddress + nameof(Contrast));
            Contrast.Minimum = -1.0;

            Invert = new Slider(MessageAddress + nameof(Invert));
            InvertMode = ((TextureInvertMode)0).ToString();

            Hue = new Slider(MessageAddress + nameof(Hue));
            Hue.Minimum = -1.0;

            Saturation = new Slider(MessageAddress + nameof(Saturation));
            Saturation.Minimum = -1.0;

            Luminosity = new Slider(MessageAddress + nameof(Luminosity));
            Luminosity.Minimum = -1.0;

            Keying = new Slider(MessageAddress + nameof(Keying));

            Scale = new Slider(MessageAddress + nameof(Scale));
            Scale.Minimum = -1.0;

            Rotate = new Slider(MessageAddress + nameof(Rotate));
            Rotate.Minimum = -1.0;

            Pan = new Slider(MessageAddress + nameof(Pan));
            Pan.Minimum = -1.0;

            Tilt = new Slider(MessageAddress + nameof(Tilt));
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

        public AssetPathSelector<AssetTexture> AssetPathSelector { get; set; }
        public AssetTexture AssetTexture { get; set; }

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
        public MessengerService MessengerService { get; set; }
        public Mementor Mementor { get; set; }
        #endregion

        #region COPY/PASTE/RESET
        public void Reset()
        {
            MessengerService.Disable();

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

            MessengerService.Enable();
        }

        public void CopyTexture()
        {
            IDataObject data = new DataObject();
            data.SetData("TextureModel", this.GetModel(), false);
            Clipboard.SetDataObject(data);
        }

        public void PasteTexture()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent("TextureModel"))
            {
                Mementor.BeginBatch();
                MessengerService.Disable();

                var texturemodel = data.GetData("TextureModel") as TextureModel;
                this.SetViewModel(texturemodel);

                MessengerService.Enable();
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