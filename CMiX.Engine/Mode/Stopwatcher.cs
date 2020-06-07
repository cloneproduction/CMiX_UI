using System;
using System.Diagnostics;
//using VVVV.Utils.VMath;

namespace CMiX.Engine
{
    public class Stopwatcher
    {
        public Stopwatcher(double period)
        {
            Stopwatch = new Stopwatch();
            Stopwatch.Start();
            Inverse = false;
            Period = period;
        }

        public Stopwatch Stopwatch { get; set; }
        public bool Inverse { get; set; }

        private double _period;
        public double Period
        {
            get { return _period; }
            set { _period = value; }
        }

        public void Reset()
        {
            Stopwatch.Reset();
            Stopwatch.Start();
        }

        public void Update()
        {
            if (Stopwatch.ElapsedMilliseconds >= Period * 1000)
            {
                Reset();
                OnChange();
            }
        }

        public event EventHandler Change;
        public void OnChange()
        {
            EventHandler handler = Change;
            if (null != handler) handler(this, EventArgs.Empty);
        }
    }
}