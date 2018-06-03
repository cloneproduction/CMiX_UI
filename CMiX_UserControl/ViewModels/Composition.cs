using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX
{
    public class Composition : INotifyPropertyChanged
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

        MainBeat _MainBeat = new MainBeat();
        public MainBeat MainBeat
        {
            get { return _MainBeat; }
            set
            {
                if (_MainBeat != value)
                {
                    _MainBeat = value; OnPropertyChanged("MainBeat");
                }
            }
        }

        Camera _Camera = new Camera();
        public Camera Camera
        {
            get { return _Camera; }
            set
            {
                if (_Camera != value)
                {
                    _Camera = value; OnPropertyChanged("Camera");
                }
            }
        }

        List<Layer> _Layer = new List<Layer>();
        public List<Layer> Layer
        {
            get { return _Layer; }
            set
            {
                if (_Layer != value)
                {
                    _Layer = value; OnPropertyChanged("Layer");
                }
            }
        }
    }
}
