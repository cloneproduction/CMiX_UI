using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace CMiX.MVVM.Controls
{
    public class BeatDependencyObject : DependencyObject
    {
        //protected override Freezable CreateInstanceCore()
        //{
        //    return new BeatDependencyObject();
        //}

        public BeatDependencyObject()
        {
            sb = new Storyboard();
            MakeCollection(sb);
            Console.WriteLine("MakeCollectionMakeCollection");
        }

        private void MakeCollection(Storyboard storyboard)
        {
            
            storyboard.Children.Clear();
            AnimationCollection.Clear();
            double Multiplier = 1.0;
            for (int i = 0; i <= 3; i++)
            {
                Multiplier *= 2;
                
                CreateAnimation(this.Period, Multiplier, storyboard);
            }

            CreateAnimation(this.Period, 1, storyboard);

            Multiplier = 1.0;
            for (int i = 0; i <= 3; i++)
            {
                Multiplier /= 2;
                CreateAnimation(this.Period, Multiplier, storyboard);
            }
            storyboard.RepeatBehavior = RepeatBehavior.Forever;
            storyboard.Begin();
        }

        private void CreateAnimation(double period, double multiplier, Storyboard storyboard)
        {
            var dummyDO = new DummyDO();
            //dummyDO.Name = "DUMMY " + i.ToString();
            this.AnimationCollection.Add(dummyDO);

            var newda = new DoubleAnimation(0, 100, new Duration(TimeSpan.FromMilliseconds(period / multiplier)));
            newda.RepeatBehavior = RepeatBehavior.Forever;

            Storyboard.SetTarget(newda, dummyDO);
            Storyboard.SetTargetProperty(newda, new PropertyPath(DummyDO.AnimationPositionProperty));
            storyboard.Children.Add(newda);
            AnimatedDouble.Add(dummyDO.AnimationPosition);
        }

        private Storyboard sb { get; set; }

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
                Console.WriteLine("OnPeriodChange " + mbc.Period);
                //mbc.sb.Stop();
                mbc.MakeCollection(mbc.sb);
            }
        }

        public static readonly DependencyProperty AnimatedDoubleProperty =
        DependencyProperty.Register("AnimatedDouble", typeof(ObservableCollection<double>), typeof(BeatDependencyObject), new FrameworkPropertyMetadata(new ObservableCollection<double>()));
        public ObservableCollection<double> AnimatedDouble
        {
            get { return (ObservableCollection<double>)GetValue(AnimatedDoubleProperty); }
            set { SetValue(AnimatedDoubleProperty, value); }
        }

        public static readonly DependencyProperty AnimationCollectionProperty =
        DependencyProperty.Register("AnimationCollection", typeof(ObservableCollection<DummyDO>), typeof(BeatDependencyObject), new FrameworkPropertyMetadata(new ObservableCollection<DummyDO>()));
        public ObservableCollection<DummyDO> AnimationCollection
        {
            get { return (ObservableCollection<DummyDO>)GetValue(AnimationCollectionProperty); }
            set { SetValue(AnimationCollectionProperty, value); }
        }
    }
}
