using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace CMiX.MVVM.Controls
{
    public class BeatDependencyObject : Control
    {
        public BeatDependencyObject()
        {
            sb = new Storyboard();
            da = new DoubleAnimation();
            Storyboard.SetTarget(da, this);
            Storyboard.SetTargetProperty(da, new PropertyPath("AnimationPosition"));
            sb.Children.Add(da);
            sb.RepeatBehavior = RepeatBehavior.Forever;
            SetStoryBoard(new TimeSpan(0, 0, 0, 0, Convert.ToInt32(1000)));
        }


        private Storyboard sb { get; set; }
        private DoubleAnimation da { get; set; }
        private void SetStoryBoard(TimeSpan timeSpan)
        {
            if (sb != null)
            {
                sb.Stop();
                da.From = 0;
                da.To = 100;
                da.Duration = new Duration(timeSpan);
                sb.Begin();
            }
        }


        public static readonly DependencyProperty PeriodProperty =
        DependencyProperty.Register("Period", typeof(double), typeof(BeatDependencyObject), new FrameworkPropertyMetadata(1000.0, new PropertyChangedCallback(OnPeriodChange)));
        public double Period
        {
            get { return (double)GetValue(PeriodProperty); }
            set { SetValue(PeriodProperty, value); }
        }
        private static void OnPeriodChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mbc = (BeatDependencyObject)d;
            var val = (double)e.NewValue;
            if (val != double.NaN && val > 0)
            {
                mbc.sb.Stop();
                mbc.da.Duration = new Duration(TimeSpan.FromMilliseconds(Convert.ToInt32(val)));
                mbc.sb.Begin();
            }
        }




        public static readonly DependencyProperty AnimationPositionProperty =
        DependencyProperty.Register("AnimationPosition", typeof(double), typeof(BeatDependencyObject), new FrameworkPropertyMetadata(0.0));
        public double AnimationPosition
        {
            get { return (double)GetValue(AnimationPositionProperty); }
            set { SetValue(AnimationPositionProperty, value); }
        }
    }
}
