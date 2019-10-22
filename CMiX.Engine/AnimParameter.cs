using System;

namespace CMiX.Engine
{
    public class AnimParameter
    {
        public AnimParameter(double period)
        {
            Period = period;
            Stopwatcher = new Stopwatcher(Period);
            Mode = new LFO(Stopwatcher);
        }

        private Stopwatcher _stopwatcher;
        public Stopwatcher Stopwatcher
        {
            get { return _stopwatcher; }
            set { _stopwatcher = value; }
        }

        public double Period { get; set; }

        private Mode _mode;
        public Mode Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }

        private ModeType modeType;
        public ModeType ModeType
        {
            get { return modeType; }
            set
            {
                if (ModeType == value)
                    return;
                else
                {
                    modeType = value;
                    ChangeModeType(ModeType);
                }
            }
        }

        public void Update()
        {
            Stopwatcher.Update();
            Mode.UpdateValue.Invoke();
        }

        public void ChangeModeType(ModeType modetype)
        {
            switch (modetype)
            {
                case ModeType.STEADY:
                    {
                        Mode = new Steady(Stopwatcher);
                        break;
                    }
                case ModeType.RANDOM:
                    {
                        Mode = new Randomized(Stopwatcher);
                        break;
                    }
                case ModeType.LFO:
                    {
                        Mode = new LFO(Stopwatcher);
                        break;
                    }
                case ModeType.STEPPER:
                    {
                        Mode = new Stepper(Stopwatcher);
                        break;
                    }
            }
        }
    }
}
