using System;
using System.Diagnostics;
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
            BeatTick = 0;

            Stopwatch = new Stopwatch();
            Stopwatch.Start();
            
        }



        public override void OnApplyTemplate()
        {
            //BPMDisplay = GetTemplateChild("BPMDisplay") as TextBlock;
            BeatDisplay = GetTemplateChild("BeatDisplay") as Border;
            BeatDisplayCanvas = GetTemplateChild("BeatDisplayCanvas") as Canvas;

            CompositionTarget.Rendering += CompositionTarget_Rendering;

            if (BeatDisplay != null)
            {
                BeatDisplay.Width = 10;// BeatDisplayCanvas.ActualWidth / 4;
                BeatDisplayTranslate = new TranslateTransform(0, 0);
                BeatDisplay.RenderTransform = BeatDisplayTranslate;
                sb = new Storyboard();
                da = new DoubleAnimation();
                Storyboard.SetTarget(da, BeatDisplay);
                Storyboard.SetTargetProperty(da, new PropertyPath("RenderTransform.X"));
                sb.Children.Add(da);
                sb.RepeatBehavior = RepeatBehavior.Forever;
                da.IsAdditive = false;
                SetStoryBoard(new TimeSpan(0, 0, 0, 0, Convert.ToInt32(Period)));
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
            {
                BeatTick = 0;
                //StartAnimation();
            }
                
            //BeatDisplay.Width = BeatDisplayCanvas.ActualWidth / 4;
            //BeatDisplayTranslate.X = BeatDisplayCanvas.ActualWidth / 4 * BeatTick;
        }
        private Storyboard sb { get; set; }
        private DoubleAnimation da { get; set; }
        private void SetStoryBoard(TimeSpan timeSpan)
        {
            if(sb != null)
            {
                sb.Stop();
                da.To = 20;
                da.Duration = new Duration(timeSpan);

                Console.WriteLine("SetStoryBoard");
                sb.Begin();
            }

        }

        private void RestartAnimation()
        {
            
        }

        public void StartAnimation()
        {
            SetStoryBoard(new TimeSpan(0, 0, 0, 0, Convert.ToInt32(Period)));

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
        DependencyProperty.Register("Period", typeof(double), typeof(MasterBeatController), new FrameworkPropertyMetadata(1000.0, new PropertyChangedCallback(OnPeriodChange)));

        private static void OnPeriodChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mbc = (MasterBeatController)d;
            var val = (double)e.NewValue;
            if(val != double.NaN && val > 0)
            {


                //mbc.sb.Stop();
                
                
                
                //mbc.sb.Begin();
                Console.WriteLine(mbc.da.Duration +"   " + mbc.sb.Children[0].Duration);
                //mbc.sb.Children.Clear();
                //mbc.sb.Begin();
                mbc.sb.Seek(TimeSpan.Zero, TimeSeekOrigin.BeginTime);
                mbc.sb.Stop();
                mbc.da.Duration = new Duration(TimeSpan.FromMilliseconds(Convert.ToInt32(val)));
                mbc.sb.Begin();
                //mbc.SetStoryBoard(new TimeSpan(0, 0, 0, 0, Convert.ToInt32(val)));
            }
                
        }

        public double Period
        {
            get { return (double)GetValue(PeriodProperty); }
            set { SetValue(PeriodProperty, value); }
        }
    }
}