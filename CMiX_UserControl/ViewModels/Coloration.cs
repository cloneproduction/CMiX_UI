using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CMiX.ViewModels
{
    public class Coloration : ViewModel
    {
        Color _ObjectColor;
        public Color ObjectColor
        {
            get => _ObjectColor;
            set => this.SetAndNotify(ref _ObjectColor, value);
        }

        Color _BackgroundColor;
        public Color BackgroundColor
        {
            get => _BackgroundColor;
            set => this.SetAndNotify(ref _BackgroundColor, value);
        }

        double _HueColor = 0.0;
        public double HueColor
        {
            get => _HueColor;
            set => this.SetAndNotify(ref _HueColor, value);
        }

        ColorationModifier _HueModifier;
        public ColorationModifier HueModifier
        {
            get => _HueModifier;
            set => this.SetAndNotify(ref _HueModifier, value);
        }

        double _Saturation = 0.0;
        public double Saturation
        {
            get => _Saturation;
            set => this.SetAndNotify(ref _Saturation, value);
        }

        ColorationModifier _SaturationModifier;
        public ColorationModifier SaturationModifier
        {
            get => _SaturationModifier;
            set => this.SetAndNotify(ref _SaturationModifier, value);
        }

        double _Lightness = 0.0;
        public double Lightness
        {
            get => _Lightness;
            set => this.SetAndNotify(ref _Lightness, value);
        }

        ColorationModifier _LightnessModifier;
        public ColorationModifier LightnessModifier
        {
            get => _LightnessModifier;
            set => this.SetAndNotify(ref _LightnessModifier, value);
        }
    }
}
