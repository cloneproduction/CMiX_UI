using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX
{
    public class PostFX : INotifyPropertyChanged
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

        double _FeedBack = 0.0;
        public double FeedBack
        {
            get { return _FeedBack; }
            set
            {
                if (_FeedBack != value)
                {
                    _FeedBack = value; OnPropertyChanged("FeedBack");
                }
            }
        }

        double _Blur = 0.0;
        public double Blur
        {
            get { return _Blur; }
            set
            {
                if (_Blur != value)
                {
                    _Blur = value; OnPropertyChanged("Blur");
                }
            }
        }

        enum Transform { NONE, MIR_CTR, MIR_LT, MIR_TOP, MIR_RT, MIR_BOT, CLA_LT, CLA_TOP, CLA_RT, CLA_BOT };

        Enum _SelectedTransform;
        public Enum SelectedInvertMode
        {
            get { return _SelectedTransform; }
            set
            {
                if (_SelectedTransform != value)
                {
                    _SelectedTransform = value; OnPropertyChanged("SelectedTransform");
                }
            }
        }

        enum View { NONE, MIR_CTR, MIR_LT, MIR_TOP, MIR_RT, MIR_BOT, CLA_LT, CLA_TOP, CLA_RT, CLA_BOT };

        Enum _SelectedView;
        public Enum SelectedView
        {
            get { return _SelectedView; }
            set
            {
                if (_SelectedView != value)
                {
                    _SelectedView = value; OnPropertyChanged("SelectedView");
                }
            }
        }
    }
}
