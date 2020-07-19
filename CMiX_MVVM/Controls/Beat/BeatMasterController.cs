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
                StartAnimation();
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
        private void SetStoryBoard(TimeSpan timeSpan)
        {
            double toValue = 10;// BeatDisplayCanvas.ActualWidth / 4;
            Duration duration = new Duration(timeSpan);
            var animation0 = new DoubleAnimation(toValue, duration);
            Storyboard.SetTarget(animation0, BeatDisplay);
            Storyboard.SetTargetProperty(animation0, new PropertyPath("RenderTransform.X"));
            sb.Children.Add(animation0);
        }

        private void RestartAnimation()
        {
            
        }

        public void StartAnimation()
        {
            //var moveTopUpDuration = TimeSpan.FromSeconds(1);
            //sb.RepeatBehavior = RepeatBehavior.Forever;





            //var moveTopDown = new ThicknessAnimation(new Thickness(0, 0, 0, 0), TimeSpan.FromSeconds(1));
            //Storyboard.SetTarget(moveTopDown, BeatDisplay);
            //Storyboard.SetTargetProperty(moveTopDown, new PropertyPath(Border.MarginProperty));
            //moveTopDown.BeginTime = moveTopUpDuration;


            //storyboard.Children.Add(moveTopDown);
            //moveTopUp.Completed += MoveTopUpCompleted;
            SetStoryBoard(new TimeSpan(0, 0, 0, 0, Convert.ToInt32(Period)));
            sb.Begin();
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
                mbc.sb.Stop();
                mbc.SetStoryBoard(new TimeSpan(0, 0, 0, 0, Convert.ToInt32(PeriodProperty)));
                mbc.sb.Begin();
            }
                
        }

        public double Period
        {
            get { return (double)GetValue(PeriodProperty); }
            set { SetValue(PeriodProperty, value); }
        }
    }
}