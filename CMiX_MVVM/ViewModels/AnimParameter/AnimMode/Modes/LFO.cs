using System;
using CMiX.MVVM.ViewModels;

namespace CMiX.MVVM.ViewModels
{
    public class LFO : Mode, IAnimMode
    {
        public LFO()
        {

        }
        public LFO(Stopwatcher stopwatcher)
        {
            Stopwatcher = stopwatcher;
            EasingType = EasingType.SineEaseInOut;
            //UpdateValue = new Action(Update);
        }

        public EasingType EasingType { get; set; }
        public Range Range { get; set; }

        //public void Update()
        //{
        //    //ParameterValue = Easing.Interpolate(Stopwatcher.LFO, EasingType);
        //}
    }
}