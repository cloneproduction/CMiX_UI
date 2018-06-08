using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace CMiX.ViewModels
{
    public class Geometry : ViewModel
    {
        public Geometry(string layerName)
            : this(
                  layerName : layerName,
                  message : new Messenger(),
                  geometryCount: 1,
                  geometryPaths: Enumerable.Empty<string>(),
                  translateMode: default,
                  translateAmount: 0.0,
                  scaleMode: default,
                  scaleAmount: 0.0,
                  rotationMode: default,
                  rotationAmount: 0.0,
                  is3D: false,
                  keepAspectRatio: false)
        { }

        public Geometry(
            string layerName,
            Messenger message,
            int geometryCount,
            IEnumerable<string> geometryPaths,
            GeometryTranslateMode translateMode,
            double translateAmount,
            GeometryScaleMode scaleMode,
            double scaleAmount,
            GeometryRotationMode rotationMode,
            double rotationAmount,
            bool is3D,
            bool keepAspectRatio)
        {
            if (geometryPaths == null)
            {
                throw new ArgumentNullException(nameof(geometryPaths));
            }
            LayerName = layerName;
            Message = message;
            GeometryCount = geometryCount;
            GeometryPaths = new ObservableCollection<string>(geometryPaths);
            TranslateMode = translateMode;
            TranslateAmount = translateAmount;
            ScaleMode = scaleMode;
            ScaleAmount = scaleAmount;
            RotationMode = rotationMode;
            RotationAmount = rotationAmount;
            Is3D = is3D;
            KeepAspectRatio = keepAspectRatio;
        }

        private string Address => String.Format("{0}/{1}/", LayerName, nameof(Geometry));

        private Messenger _message;
        public Messenger Message
        {
            get => _message;
            set => SetAndNotify(ref _message, value);
        }

        private string _layerName;
        public string LayerName
        {
            get => _layerName;
            set => SetAndNotify(ref _layerName, value);
        }

        private int _geometryCount;
        public int GeometryCount
        {
            get => _geometryCount;
            set => SetAndNotify(ref _geometryCount, value);
        }
        
        public ObservableCollection<string> GeometryPaths { get; }

        private GeometryTranslateMode _translateMode;
        public GeometryTranslateMode TranslateMode
        {
            get => _translateMode;
            set => SetAndNotify(ref _translateMode, value);
        }

        private double _translateAmount;
        public double TranslateAmount
        {
            get => _translateAmount;
            set
            {
                SetAndNotify(ref _translateAmount, value);
                Message.SendOSC(Address + nameof(TranslateAmount), TranslateAmount.ToString());
            }
        }

        private GeometryScaleMode _scaleMode;
        public GeometryScaleMode ScaleMode
        {
            get => _scaleMode;
            set => SetAndNotify(ref _scaleMode, value);
        }

        private double _scaleAmount;
        public double ScaleAmount
        {
            get => _scaleAmount;
            set
            {
                SetAndNotify(ref _scaleAmount, value);
                Message.SendOSC(Address + nameof(ScaleAmount), ScaleAmount.ToString());
            }
        }

        private GeometryRotationMode _RotationMode;
        public GeometryRotationMode RotationMode
        {
            get => _RotationMode;
            set => SetAndNotify(ref _RotationMode, value);
        }

        private double _rotationAmount;
        public double RotationAmount
        {
            get => _rotationAmount;
            set
            {
                SetAndNotify(ref _rotationAmount, value);
                Message.SendOSC(Address + nameof(RotationAmount), RotationAmount.ToString());
            }
        }

        private bool _is3D;
        public bool Is3D
        {
            get => _is3D;
            set
            {
                SetAndNotify(ref _is3D, value);
                Message.SendOSC(Address + nameof(Is3D), Is3D.ToString());
            }
        }

        private bool _keepAspectRatio;
        public bool KeepAspectRatio
        {
            get => _keepAspectRatio;
            set
            {
                SetAndNotify(ref _keepAspectRatio, value);
                Message.SendOSC(Address + nameof(KeepAspectRatio), KeepAspectRatio.ToString());
            }
        }
    }
}
