using System.Windows;
using System.Windows.Input;
using CMiX.MVVM.Models;
using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class Texture : Sendable
    {
        #region CONSTRUCTORS
        public Texture()
        {
            AssetPathSelector = new AssetPathSelector(new AssetTexture(), this);

            Inverter = new Inverter(this);

            Brightness = new Slider(nameof(Brightness), this);
            Brightness.Minimum = -1.0;

            Contrast = new Slider(nameof(Contrast), this);
            Contrast.Minimum = -1.0;

            Hue = new Slider(nameof(Hue), this);
            Hue.Minimum = -1.0;

            Saturation = new Slider(nameof(Saturation), this);
            Saturation.Minimum = -1.0;

            Luminosity = new Slider(nameof(Luminosity), this);
            Luminosity.Minimum = -1.0;

            Keying = new Slider(nameof(Keying), this);

            Scale = new Slider(nameof(Scale), this);
            Scale.Minimum = -1.0;

            Rotate = new Slider(nameof(Rotate), this);
            Rotate.Minimum = -1.0;

            Pan = new Slider(nameof(Pan), this);
            Pan.Minimum = -1.0;

            Tilt = new Slider(nameof(Tilt), this);
            Tilt.Minimum = -1.0;

            CopyTextureCommand = new RelayCommand(p => CopyTexture());
            PasteTextureCommand = new RelayCommand(p => PasteTexture());
            ResetTextureCommand = new RelayCommand(p => ResetTexture());
        }
        #endregion

        public Texture(Sendable parentSendable) : this()
        {
            SubscribeToEvent(parentSendable);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {
            if (e.ParentMessageAddress + this.GetMessageAddress() == e.MessageAddress)
                this.SetViewModel(e.Model as TextureModel);
            else
                OnReceiveChange(e.Model, e.MessageAddress, e.ParentMessageAddress + this.GetMessageAddress());
        }

        #region PROPERTIES
        public ICommand CopyTextureCommand { get; }
        public ICommand PasteTextureCommand { get; }
        public ICommand ResetTextureCommand { get; }

        public AssetPathSelector AssetPathSelector { get; set; }
        public AssetTexture AssetTexture { get; set; }

        public Slider Brightness { get; }
        public Slider Contrast { get; }
        public Slider Hue { get; }
        public Slider Saturation { get; }
        public Slider Luminosity { get; }
        public Slider Keying { get; }
        public Slider Pan { get; }
        public Slider Tilt { get; }
        public Slider Scale { get; }
        public Slider Rotate { get; }

        public Inverter Inverter { get; set; }
        #endregion

        #region COPY/PASTE/RESET
        public void Reset()
        {
            Brightness.Reset();
            Contrast.Reset();
            Hue.Reset();
            Saturation.Reset();
            Luminosity.Reset();
            Keying.Reset();
            Scale.Reset();
            Rotate.Reset();
            Pan.Reset();
            Tilt.Reset();
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
                //Mementor.BeginBatch();

                var texturemodel = data.GetData("TextureModel") as TextureModel;
                this.SetViewModel(texturemodel);

                //Mementor.EndBatch();
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