using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.ViewModels
{
    public class Texture : ViewModel
    {
        List<string> _SelectedTexturePath = new List<string>();
        public List<string> SelectedTexturePath
        {
            get => _SelectedTexturePath;
            set => this.SetAndNotify(ref _SelectedTexturePath, value);
        }

        double _TextureBrightness = 0.0;
        public double TextureBrightness
        {
            get => _TextureBrightness;
            set => this.SetAndNotify(ref _TextureBrightness, value);
        }

        double _TextureContrast = 0.0;
        public double TextureContrast
        {
            get => _TextureContrast;
            set => this.SetAndNotify(ref _TextureContrast, value);
        }

        double _TextureSaturation = 0.0;
        public double TextureSaturation
        {
            get => _TextureSaturation;
            set => this.SetAndNotify(ref _TextureSaturation, value);
        }

        double _TextureKeying = 0.0;
        public double TextureKeying
        {
            get => _TextureKeying;
            set => this.SetAndNotify(ref _TextureKeying, value);
        }

        double _TextureInvert = 0.0;
        public double TextureInvert
        {
            get => _TextureInvert;
            set => this.SetAndNotify(ref _TextureInvert, value);
        }

        TextureInvertMode _InvertMode;
        public TextureInvertMode InvertMode
        {
            get => _InvertMode;
            set => this.SetAndNotify(ref _InvertMode, value);
        }
    }
}
