using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX
{
    public class Camera : INotifyPropertyChanged
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

        enum CameraRotation { STD_CTR, STD_UP, STD_DN, STD_LT, STD_RT, STD_MID, SLD_RDM, SLD_DNUP, SLD_DN, SLD_UP, SLD_LT, SLD_RT, SLD_LTRT, FLA_RDM, FLA_DNUP, FLA_DN, FLA_UP, FLA_LT, FLA_RT, FLA_LTRT };

        Enum _Rotation;
        public Enum Rotation
        {
            get { return _Rotation; }
            set
            {
                if (_Rotation != value)
                {
                    _Rotation = value; OnPropertyChanged("Rotation");
                }
            }
        }

        enum CameraLookAt { STD_CTR, Oblique125, FlashRandom, SpinRandom, SpinRight, SpinLeft };

        Enum _LookAt;
        public Enum LookAt
        {
            get { return _LookAt; }
            set
            {
                if (_LookAt != value)
                {
                    _LookAt = value; OnPropertyChanged("LookAt");
                }
            }
        }

        enum CameraView { STD_CTR, FLA_RDM, SLD_RDM };

        Enum _View;
        public Enum View
        {
            get { return _View; }
            set
            {
                if (_View != value)
                {
                    _View = value; OnPropertyChanged("View");
                }
            }
        }


        int _BeatMultiplier = 1;
        public int BeatMultiplier
        {
            get { return _BeatMultiplier; }
            set
            {
                if (_BeatMultiplier != value)
                {
                    _BeatMultiplier = value; OnPropertyChanged("BeatMultiplier");
                }
            }
        }

        double _BeatChanceToHit = 1.0;
        public double BeatChanceToHit
        {
            get { return _BeatChanceToHit; }
            set
            {
                if (_BeatChanceToHit != value)
                {
                    _BeatChanceToHit = value; OnPropertyChanged("BeatChanceToHit");
                }
            }
        }

        double _CameraFOV = 1.0;
        public double CameraFOV
        {
            get { return _CameraFOV; }
            set
            {
                if (_CameraFOV != value)
                {
                    _CameraFOV = value; OnPropertyChanged("CameraFOV");
                }
            }
        }

        double _CameraZoom = 1.0;
        public double CameraZoom
        {
            get { return _CameraZoom; }
            set
            {
                if (_CameraZoom != value)
                {
                    _CameraZoom = value; OnPropertyChanged("CameraZoom");
                }
            }
        }
    }
}
