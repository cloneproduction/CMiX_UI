using System;
using System.Collections.Generic;
using System.Linq;

namespace CMiX
{
    class BeatSystem
    {
        public BeatSystem()
        {

        }

        public List<double> tapPeriods = new List<double>();
        public List<double> tapTime = new List<double>();

        public double GetMasterPeriod()
        {
            double ms = (DateTime.Now - DateTime.MinValue).TotalMilliseconds;

            if (tapTime.Count > 1 && ms - tapTime[tapTime.Count - 1] > 5000)
            {
                tapTime.Clear();
            }
            tapTime.Add(ms);

            if (tapTime.Count > 1)
            {
                tapPeriods.Clear();
                for (int i = 1; i < tapTime.Count; i++)
                {
                    double average = tapTime[i] - tapTime[i-1];
                    tapPeriods.Add(average);
                }
            }
            return tapPeriods.Sum() / tapPeriods.Count;
        }

        public double GetCurrentTime()
        {
            return (DateTime.Now - DateTime.MinValue).TotalMilliseconds;
        }
    }
}