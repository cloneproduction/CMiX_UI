using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.ViewModels
{
    public class MainBeat : ViewModel
    {
        double _Period = 0.0;
        public double Period
        {
            get => _Period;
            set => this.SetAndNotify(ref _Period, value);
        }

        int _BeatMultiplier = 1;
        public int BeatMultiplier
        {
            get => _BeatMultiplier;
            set => this.SetAndNotify(ref _BeatMultiplier, value);
        }

        bool _ResetTime = false;
        public bool ResetTime
        {
            get => _ResetTime;
            set => this.SetAndNotify(ref _ResetTime, value);
        }
    }
}
