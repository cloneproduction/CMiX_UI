using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CMiX
{
    public class Coloration : INotifyPropertyChanged
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

        Color _ObjectColor;
        public Color ObjectColor
        {
            get { return _ObjectColor; }
            set
            {
                if (_ObjectColor != value)
                {
                    _ObjectColor = value; OnPropertyChanged("ObjectColor");
                }
            }
        }

        Color _BackgroundColor;
        public Color BackgroundColor
        {
            get { return _BackgroundColor; }
            set
            {
                if (_BackgroundColor != value)
                {
                    _BackgroundColor = value; OnPropertyChanged("BackgroundColor");
                }
            }
        }

        enum ColorModifier { STD_CTR, SLD_RDM, FLA_RDM, SLD_LIN };

        double _Hue = 0.0;
        public double Hue
        {
            get { return _Hue; }
            set
            {
                if (_Hue != value)
                {
                    _Hue = value; OnPropertyChanged("Hue");
                }
            }
        }

        Enum _SelectedHueModifier;
        public Enum SelectedHueModifier
        {
            get { return _SelectedHueModifier; }
            set
            {
                if (_SelectedHueModifier != value)
                {
                    _SelectedHueModifier = value; OnPropertyChanged("SelectedHueModifier");
                }
            }
        }

        double _Saturation = 0.0;
        public double Saturation
        {
            get { return _Saturation; }
            set
            {
                if (_Saturation != value)
                {
                    _Saturation = value; OnPropertyChanged("Saturation");
                }
            }
        }

        Enum _SelectedSaturationModifier;
        public Enum SelectedSaturationModifier
        {
            get { return _SelectedSaturationModifier; }
            set
            {
                if (_SelectedSaturationModifier != value)
                {
                    _SelectedSaturationModifier = value; OnPropertyChanged("SelectedSaturationModifier");
                }
            }
        }

        double _Lightness = 0.0;
        public double Lightness
        {
            get { return _Lightness; }
            set
            {
                if (_Lightness != value)
                {
                    _Lightness = value; OnPropertyChanged("Lightness");
                }
            }
        }

        Enum _SelectedLightnessModifier;
        public Enum SelectedLightnessModifier
        {
            get { return _SelectedLightnessModifier; }
            set
            {
                if (_SelectedLightnessModifier != value)
                {
                    _SelectedLightnessModifier = value; OnPropertyChanged("SelectedLightnessModifier");
                }
            }
        }


    }
}
