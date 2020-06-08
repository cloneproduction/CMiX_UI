using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CMiX.MVVM
{
    public class Clock
    {
        private delegate void TimerEventHandler(int id, int msg, IntPtr user, int dw1, int dw2);

        private const int TIME_PERIODIC = 1;
        private const int EVENT_TYPE = TIME_PERIODIC;

        [DllImport("winmm.dll")]
        private static extern int timeSetEvent(int delay, int resolution, TimerEventHandler handler, IntPtr user, int eventType);
        [DllImport("winmm.dll")]
        private static extern int timeKillEvent(int id);
        [DllImport("winmm.dll")]
        private static extern int timeBeginPeriod(int msec);
        [DllImport("winmm.dll")]
        private static extern int timeEndPeriod(int msec);

        public int _timerId;
        private TimerEventHandler _handler = delegate { };

        public event EventHandler OnTick;

        public int Interval { get; set; }

        public Clock()
        {
            Interval = 1000;
            //OnTick = tick;
            _handler = new TimerEventHandler(timerElapsed);
        }

        public void Stop()
        {
            int res = timeKillEvent(_timerId);
            timeEndPeriod(1);
            _timerId = 0;
        }

        public void Start()
        {
            timeBeginPeriod(1);
            _timerId = timeSetEvent(Interval, 0, _handler, IntPtr.Zero, EVENT_TYPE);
        }
        private void timerElapsed(int id, int msg, IntPtr user, int dw1, int dw2)
        {
            OnTick(this, new EventArgs());
        }
    }
}
