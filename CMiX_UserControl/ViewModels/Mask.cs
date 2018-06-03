using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX
{
    public class Mask : INotifyPropertyChanged
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

        bool _Enable = true;
        public bool Enable
        {
            get { return _Enable; }
            set
            {
                if (_Enable != value)
                {
                    _Enable = value; OnPropertyChanged("Enable");
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

        Geometry _Geometry = new Geometry();
        public Geometry Geometry
        {
            get { return _Geometry; }
            set
            {
                if (_Geometry != value)
                {
                    _Geometry = value; OnPropertyChanged("Geometry");
                }
            }
        }

        Texture _Texture = new Texture();
        public Texture Texture
        {
            get { return _Texture; }
            set
            {
                if (_Texture != value)
                {
                    _Texture = value; OnPropertyChanged("Texture");
                }
            }
        }

        PostFX _PostFX = new PostFX();
        public PostFX PostFX
        {
            get { return _PostFX; }
            set
            {
                if (_PostFX != value)
                {
                    _PostFX = value; OnPropertyChanged("PostFX");
                }
            }
        }
    }
}
