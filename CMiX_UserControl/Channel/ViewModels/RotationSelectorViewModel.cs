using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX
{
    public class RotationSelectorViewModel : INotifyPropertyChanged
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
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        List<bool> _RotationAxis = new List<bool> { true, true, true };
        public List<bool> RotationAxis
        {
            get { return _RotationAxis; }
            set
            {
                if (_RotationAxis != value)
                {
                    _RotationAxis = value; OnPropertyChanged("RotationAxis");
                }
            }
        }

        string _Rotation = "STD_CTR";
        public string Rotation
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
    }
}
