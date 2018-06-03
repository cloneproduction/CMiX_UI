using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX
{
    public class Geometry : INotifyPropertyChanged
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


        int _GeometryCount = 1;
        public int GeometryCount
        {
            get { return _GeometryCount; }
            set
            {
                if (_GeometryCount != value)
                {
                    _GeometryCount = value; OnPropertyChanged("GeometryCount");
                }
            }
        }


        List<string> _SelectedGeometryPath = new List<string>();
        public List<string> SelectedGeometryPath
        {
            get { return _SelectedGeometryPath; }
            set
            {
                if (_SelectedGeometryPath != value)
                {
                    _SelectedGeometryPath = value; OnPropertyChanged("SelectedGeometryPath");
                }
            }
        }

        Enum _GeometryTranslateMode;
        public Enum GeometryTranslateMode
        {
            get { return _GeometryTranslateMode; }
            set
            {
                if (_GeometryTranslateMode != value)
                {
                    _GeometryTranslateMode = value; OnPropertyChanged("GeometryTranslateMode");
                }
            }
        }

        double _GeometryTranslateAmount;
        public double GeometryTranslateAmount
        {
            get { return _GeometryTranslateAmount; }
            set
            {
                if (_GeometryTranslateAmount != value)
                {
                    _GeometryTranslateAmount = value; OnPropertyChanged("GeometryTranslateAmount");
                }
            }
        }


        Enum _GeometryScaleMode;
        public Enum GeometryScaleMode
        {
            get { return _GeometryScaleMode; }
            set
            {
                if (_GeometryScaleMode != value)
                {
                    _GeometryScaleMode = value; OnPropertyChanged("GeometryScaleMode");
                }
            }
        }

        double _GeometryScaleAmount;
        public double GeometryScaleAmount
        {
            get { return _GeometryScaleAmount; }
            set
            {
                if (_GeometryScaleAmount != value)
                {
                    _GeometryScaleAmount = value; OnPropertyChanged("GeometryScaleAmount");
                }
            }
        }

        Enum _GeometryRotationMode;
        public Enum GeometryRotationMode
        {
            get { return _GeometryRotationMode; }
            set
            {
                if (_GeometryRotationMode != value)
                {
                    _GeometryRotationMode = value; OnPropertyChanged("GeometryRotationMode");
                }
            }
        }

        double _GeometryRotationAmount;
        public double GeometryRotationAmount
        {
            get { return _GeometryRotationAmount; }
            set
            {
                if (_GeometryRotationAmount != value)
                {
                    _GeometryRotationAmount = value; OnPropertyChanged("GeometryRotationAmount");
                }
            }
        }

        bool _Is3D;
        public bool Is3D
        {
            get { return _Is3D; }
            set
            {
                if (_Is3D != value)
                {
                    _Is3D = value; OnPropertyChanged("Is3D");
                }
            }
        }

        bool _KeepAspectRatio;
        public bool KeepAspectRatio
        {
            get { return _KeepAspectRatio; }
            set
            {
                if (_KeepAspectRatio != value)
                {
                    _KeepAspectRatio = value; OnPropertyChanged("KeepAspectRatio");
                }
            }
        }
    }
}
