using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX
{
    public class MainBeat : INotifyPropertyChanged
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

        double _Period = 1.0;
        public double Period
        {
            get { return _Period; }
            set
            {
                if (_Period != value)
                {
                    _Period = value; OnPropertyChanged("Period");
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

        int _ResetTime = 0;
        public int ResetTime
        {
            get { return _ResetTime; }
            set
            {
                if (_ResetTime != value)
                {
                    _ResetTime = value; OnPropertyChanged("ResetTime");
                }
            }
        }
    }
}
