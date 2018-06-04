using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.ViewModels
{
    public class Geometry : ViewModel
    {
        int _GeometryCount = 1;
        public int GeometryCount
        {
            get => _GeometryCount;
            set => this.SetAndNotify(ref _GeometryCount, value);
        }

        List<string> _GeometryPath = new List<string>();
        public List<string> GeometryPath
        {
            get => _GeometryPath;
            set => this.SetAndNotify(ref _GeometryPath, value);
        }

        GeometryTranslateMode _TranslateMode;
        public GeometryTranslateMode TranslateMode
        {
            get => _TranslateMode;
            set => this.SetAndNotify(ref _TranslateMode, value);
        }

        double _GeometryTranslateAmount = 0.0;
        public double GeometryTranslateAmount
        {
            get => _GeometryTranslateAmount;
            set => this.SetAndNotify(ref _GeometryTranslateAmount, value);
        }


        GeometryScaleMode _ScaleMode;
        public GeometryScaleMode ScaleMode
        {
            get => _ScaleMode;
            set => this.SetAndNotify(ref _ScaleMode, value);
        }

        double _GeometryScaleAmount = 0.0;
        public double GeometryScaleAmount
        {
            get => _GeometryTranslateAmount;
            set => this.SetAndNotify(ref _GeometryTranslateAmount, value);
        }

        GeometryRotationMode _RotationMode;
        public GeometryRotationMode RotationMode
        {
            get => _RotationMode;
            set => this.SetAndNotify(ref _RotationMode, value);
        }

        double _GeometryRotationAmount = 0.0;
        public double GeometryRotationAmount
        {
            get => _GeometryRotationAmount;
            set => this.SetAndNotify(ref _GeometryRotationAmount, value);
        }

        bool _Is3D = false;
        public bool Is3D
        {
            get => _Is3D;
            set => this.SetAndNotify(ref _Is3D, value);
        }

        bool _KeepAspectRatio = false;
        public bool KeepAspectRatio
        {
            get => _KeepAspectRatio;
            set => this.SetAndNotify(ref _KeepAspectRatio, value);
        }
    }
}
