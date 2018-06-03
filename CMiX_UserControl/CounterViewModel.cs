using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX
{
    public class CounterViewModel : INotifyPropertyChanged
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


        int _Count = 0;
        public int Count
        {
            get { return _Count; }
            set
            {
                if (_Count != value)
                {
                    _Count = value; OnPropertyChanged("Count");
                }
            }
        }
    }
}
