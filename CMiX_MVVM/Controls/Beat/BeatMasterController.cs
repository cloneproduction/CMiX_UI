using CMiX.MVVM.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace CMiX.MVVM.Controls
{
    public class MasterBeatController : Control
    {
        public MasterBeatController()
        {
            OnApplyTemplate();

            tapPeriods = new List<double>();
            tapTime = new List<double>();
            BeatTick = 0;

            Timer = new HighResolutionTimer();
            Timer.Interval = (float)Period /2.0f;
            Timer.Elapsed += Timer_Elapsed;
            Timer.Start();
        }



        public override void OnApplyTemplate()
        {
            BeatTapButton = GetTemplateChild("BeatTapButton") as Button;
            BPMDisplay = GetTemplateChild("BPMDisplay") as TextBlock;
            BeatDisplay = GetTemplateChild("BeatDisplay") as Border;
            ResetTimerButton = GetTemplateChild("ResetTimerButton") as Button;
            BeatDisplayCanvas = GetTemplateChild("BeatDisplayCanvas") as Canvas;

            if (BeatTapButton != null)
            {
                BeatTapButton.Click += BeatTapButton_Click;
            }

            if(ResetTimerButton != null)
            {
                ResetTimerButton.Click += ResetTimerButton_Click;
            }


            //if (BeatDisplayCanvas != null)
            //{
            //    BeatDisplayCanvas.Loaded += BeatDisplayCanvas_Loaded;
            //}

            if (BeatDisplay != null)
            {
                BeatDisplayTranslate = new TranslateTransform(0, 0);
                BeatDisplay.RenderTransform = BeatDisplayTranslate;
            }
            base.OnApplyTemplate();
        }

        private void ResetTimerButton_Click(object sender, RoutedEventArgs e)
        {
            //Timer.Stop();
            //Timer.Start();
            BeatTick = 0;
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                Animate();
            });
        }

        private void Animate()
        {
            BeatTick++;
            if (BeatTick > 3)
                BeatTick = 0;
            BeatDisplay.Width = BeatDisplayCanvas.ActualWidth / 4;
            BeatDisplayTranslate.X = BeatDisplayCanvas.ActualWidth / 4 * BeatTick;
        }

        private void Timer_Elapsed(object sender, HighResolutionTimerElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                Animate();
            });
        }

        private void BeatTapButton_Click(object sender, RoutedEventArgs e)
        {
            Period = GetMasterPeriod();
            BPMDisplay.Text = GetBPM().ToString("0") + " BPM";
            if(Period > 0)
                Timer.Interval = (float)Period;
        }


        private double GetBPM()
        {
            var _bpm = 60000 / Period;
            if (double.IsInfinity(_bpm) || double.IsNaN(_bpm))
                return 0;
            else
                return _bpm;
        }

        private double GetMasterPeriod()
        {
            double ms = CurrentTime;

            if (tapTime.Count > 1 && ms - tapTime[tapTime.Count - 1] > 5000)
                tapTime.Clear();

            tapTime.Add(ms);

            if (tapTime.Count > 1)
            {
                tapPeriods.Clear();
                for (int i = 1; i < tapTime.Count; i++)
                    tapPeriods.Add(tapTime[i] - tapTime[i - 1]);
            }
            return tapPeriods.Sum() / tapPeriods.Count;
        }

        private readonly List<double> tapPeriods;
        private readonly List<double> tapTime;
        private double CurrentTime => (DateTime.UtcNow - DateTime.MinValue).TotalMilliseconds;
        private HighResolutionTimer Timer { get; set; }
        private Button BeatTapButton { get; set; }
        private Button ResetTimerButton { get; set; }
        private TextBlock BPMDisplay { get; set; }
        private TranslateTransform BeatDisplayTranslate { get; set; }
        private Canvas BeatDisplayCanvas { get; set; }
        private double BeatDisplayCanvasActualWidth { get; set; }
        private Border BeatDisplay { get; set; }
        private int BeatTick { get; set; }



        public static readonly DependencyProperty PeriodProperty =
        DependencyProperty.Register("Period", typeof(double), typeof(MasterBeatController), new FrameworkPropertyMetadata(1000.0, new PropertyChangedCallback(OnPeriodChanged)));
        public double Period
        {
            get { return (double)GetValue(PeriodProperty); }
            set { SetValue(PeriodProperty, value); }
        }

        private static void OnPeriodChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            double val = (double)e.NewValue;// as double;
            if(val != double.NaN && val > 0)
            {
                ((MasterBeatController)d).Timer.Interval = (float)val;
            }
            
            //Console.WriteLine(e.NewValue.GetType().Name);
            //
            //Application.Current.Dispatcher.Invoke((Action)delegate
            //{

            //});
        }


        public static readonly DependencyProperty BeatTickOnResetProperty =
        DependencyProperty.Register("BeatTickOnReset", typeof(int), typeof(MasterBeatController), new FrameworkPropertyMetadata(0, new PropertyChangedCallback(OnBeatTickCount)));
        public int BeatTickOnReset
        {
            get { return (int)GetValue(BeatTickOnResetProperty); }
            set { SetValue(BeatTickOnResetProperty, value); }
        }

        private static void OnBeatTickCount(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MasterBeatController)d).BeatTick = (int)e.NewValue;
            //Application.Current.Dispatcher.Invoke((Action)delegate
            //{
               
            //});
        }
    }
}