using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM.ViewModels
{
    public interface IAnimMode
    {
        void UpdateOnBeatTick(double period);
        double UpdatePeriod(double period, AnimParameter animParameter);

        AnimParameter AnimParameter { get; set; }
    }
}
