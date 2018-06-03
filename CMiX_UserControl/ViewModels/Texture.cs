using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX
{
    public class Texture : INotifyPropertyChanged
    {
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        List<string> _SelectedTexturePath = new List<string>();
        public List<string> SelectedTexturePath
        {
            get { return _SelectedTexturePath; }
            set
            {
                if (_SelectedTexturePath != value)
                {
                    _SelectedTexturePath = value; OnPropertyChanged("SelectedTexturePath");
                }
            }
        }

        double _TextureBrightness = 0.0;
        public double TextureBrightness
        {
            get { return _TextureBrightness; }
            set
            {
                if (_TextureBrightness != value)
                {
                    _TextureBrightness = value; OnPropertyChanged("TextureBrightness");
                }
            }
        }

        double _TextureContrast = 0.0;
        public double TextureContrast
        {
            get { return _TextureContrast; }
            set
            {
                if (_TextureContrast != value)
                {
                    _TextureContrast = value; OnPropertyChanged("TextureContrast");
                }
            }
        }

        double _TextureSaturation = 0.0;
        public double TextureSaturation
        {
            get { return _TextureSaturation; }
            set
            {
                if (_TextureSaturation != value)
                {
                    _TextureSaturation = value; OnPropertyChanged("TextureSaturation");
                }
            }
        }

        double _TextureKeying = 0.0;
        public double TextureKeying
        {
            get { return _TextureKeying; }
            set
            {
                if (_TextureKeying != value)
                {
                    _TextureKeying = value; OnPropertyChanged("TextureKeying");
                }
            }
        }

        double _TextureInvert = 0.0;
        public double TextureInvert
        {
            get { return _TextureInvert; }
            set
            {
                if (_TextureInvert != value)
                {
                    _TextureInvert = value; OnPropertyChanged("TextureInvert");
                }
            }
        }

        enum TextureInvertMode { INV_RGB, INV_VAL };

        Enum _SelectedInvertMode;
        public Enum SelectedInvertMode
        {
            get { return _SelectedInvertMode; }
            set
            {
                if (_SelectedInvertMode != value)
                {
                    _SelectedInvertMode = value; OnPropertyChanged("SelectedInvertMode");
                }
            }
        }
    }
}
