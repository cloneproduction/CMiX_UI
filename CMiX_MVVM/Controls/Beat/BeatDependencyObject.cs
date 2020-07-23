using System;
using System.Collections.ObjectModel;
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
            MakeCollection(sb);
            //sb.Children.Add(da);
            //sb.RepeatBehavior = RepeatBehavior.Forever;
            SetStoryBoard(new TimeSpan(0, 0, 0, 0, Convert.ToInt32(1000)));
        }

        private void MakeCollection(Storyboard storyboard)
        {
            for (int i = 0; i < 50; i++)
            {
                var dummyDO = new DummyDO();
                dummyDO.Name = "DUMMY " + i.ToString();
                this.AnimationCollection.Add(dummyDO);

                var newda = new DoubleAnimation();
                newda.RepeatBehavior = RepeatBehavior.Forever;
                newda.From = 0;
                newda.To = 100;
                newda.Duration = new Duration(TimeSpan.FromMilliseconds(1000 * i));
                var path = new PropertyPath(DummyDO.AnimationPositionProperty);
                Storyboard.SetTarget(newda, dummyDO);
                Storyboard.SetTargetProperty(newda, path);
                storyboard.Children.Add(newda);
                storyboard.RepeatBehavior = RepeatBehavior.Forever;
                storyboard.Begin();
            }
            
        }

        private Storyboard sb { get; set; }
        private DoubleAnimation da { get; set; }
        private void SetStoryBoard(TimeSpan timeSpan)
        {
            if (sb != null)
            {
                //sb.Stop();
                //da.From = 0;
                //da.To = 100;
                //da.Duration = new Duration(timeSpan);
                //sb.Begin();
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


        public static readonly DependencyProperty AnimationCollectionProperty =
        DependencyProperty.Register("AnimationCollection", typeof(ObservableCollection<DummyDO>), typeof(BeatDependencyObject), new FrameworkPropertyMetadata(new ObservableCollection<DummyDO>()));
        public ObservableCollection<DummyDO> AnimationCollection
        {
            get { return (ObservableCollection<DummyDO>)GetValue(AnimationCollectionProperty); }
            set { SetValue(AnimationCollectionProperty, value); }
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
