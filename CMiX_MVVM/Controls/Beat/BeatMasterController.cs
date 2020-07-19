using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CMiX.MVVM.Controls
{
    public class MasterBeatController : Control
    {
        public MasterBeatController()
        {
            OnApplyTemplate();
            BeatTick = 0;

            Stopwatch = new Stopwatch();
            Stopwatch.Start();
        }



        public override void OnApplyTemplate()
        {
            BPMDisplay = GetTemplateChild("BPMDisplay") as TextBlock;
            BeatDisplay = GetTemplateChild("BeatDisplay") as Border;
            BeatDisplayCanvas = GetTemplateChild("BeatDisplayCanvas") as Canvas;

            CompositionTarget.Rendering += CompositionTarget_Rendering;

            if (BeatDisplay != null)
            {
                BeatDisplayTranslate = new TranslateTransform(0, 0);
                BeatDisplay.RenderTransform = BeatDisplayTranslate;
            }
            base.OnApplyTemplate();
        }


        private void SetNextTickTime()
        {
            if(Period > 0)
                NextTickTime = Stopwatch.ElapsedMilliseconds + Period;
        }

        private double NextTickTime = 0;
        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if(Stopwatch.ElapsedMilliseconds > NextTickTime)
            {
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    Animate();
                });
                SetNextTickTime();
            }
        }

        private void Animate()
        {
            BeatTick++;
            if (BeatTick > 3)
                BeatTick = 0;
            BeatDisplay.Width = BeatDisplayCanvas.ActualWidth / 4;
            BeatDisplayTranslate.X = BeatDisplayCanvas.ActualWidth / 4 * BeatTick;
        }

        private void Reset()
        {

        }

        private TranslateTransform BeatDisplayTranslate { get; set; }
        private Canvas BeatDisplayCanvas { get; set; }
        private double BeatDisplayCanvasActualWidth { get; set; }
        private Border BeatDisplay { get; set; }
        private Stopwatch Stopwatch { get; set; }
        private int BeatTick;

        public static readonly DependencyProperty PeriodProperty =
        DependencyProperty.Register("Period", typeof(double), typeof(MasterBeatController), new FrameworkPropertyMetadata(1000.0));
        public double Period
        {
            get { return (double)GetValue(PeriodProperty); }
            set { SetValue(PeriodProperty, value); }
        }
    }
}