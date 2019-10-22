using System;

namespace CMiX.Engine
{
    public class LFO : Mode
    {
        public LFO(Stopwatcher stopwatcher)
        {
            Stopwatcher = stopwatcher;
            EasingType = EasingType.SineEaseInOut;
            UpdateValue = new Action(Update);
        }

        public EasingType EasingType { get; set; }

        public void Update()
        {
            ParameterValue = Easing.Interpolate(Stopwatcher.LFO, EasingType);
        }
    }
}