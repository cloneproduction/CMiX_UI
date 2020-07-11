using System;

namespace CMiX.MVVM.ViewModels
{
    public class Steady : Mode, IAnimMode
    {
        public Steady()
        {
            Range = new Range(0, 0);
        }
        public Steady(Stopwatcher stopwatcher)
        {
            Stopwatcher = stopwatcher;
            //UpdateValue = new Action(Update);
            ParameterValue = 0.0;
        }
        public void Update()
        {
            //Console.WriteLine("Steady Update");
        }

        public Range Range { get; set; }
        public EasingType EasingType { get; set; }
    }
}
