using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX
{
    public class Layer : INotifyPropertyChanged
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

        string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value; OnPropertyChanged("Name");
                }
            }
        }

        double _Fade = 0.0;
        public double Fade
        {
            get { return _Fade; }
            set
            {
                if (_Fade != value)
                {
                    _Fade = value; OnPropertyChanged("Fade");
                }
            }
        }

        Enum _BlendMode;
        public Enum BlendMode
        {
            get { return _BlendMode; }
            set
            {
                if (_BlendMode != value)
                {
                    _BlendMode = value; OnPropertyChanged("BlendMode");
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

        Content _Content = new Content();
        public Content Content
        {
            get { return _Content; }
            set
            {
                if (_Content != value)
                {
                    _Content = value; OnPropertyChanged("Content");
                }
            }
        }

        Mask _Mask = new Mask();
        public Mask ContenMaskt
        {
            get { return _Mask; }
            set
            {
                if (_Mask != value)
                {
                    _Mask = value; OnPropertyChanged("Mask");
                }
            }
        }

        Coloration _Coloration = new Coloration();
        public Coloration Coloration
        {
            get { return _Coloration; }
            set
            {
                if (_Coloration != value)
                {
                    _Coloration = value; OnPropertyChanged("Coloration");
                }
            }
        }
    }
}
