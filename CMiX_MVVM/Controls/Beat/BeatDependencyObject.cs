using CMiX.MVVM.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace CMiX.MVVM.Controls
{
    public class BeatDependencyObject : ViewModel
    {
        public BeatDependencyObject()
        {
            BeatTick = 0;
            AnimationCollection = new ObservableCollection<DummyDO>();
            sb = new Storyboard();
            sb.Completed += Sb_Completed;
            MakeCollection(sb);
        }

        private void Sb_Completed(object sender, EventArgs e)
        {
            BeatTick++;
            if (BeatTick >= 4)
                BeatTick = 0;
            sb.Begin();
        }

        public void Resync()
        {
            if(this.sb != null)
            {
                sb.Stop();
                BeatTick = 0;
                sb.Begin();
            }
        }

        private void MakeCollection(Storyboard storyboard)
        {
            BeatTick = 0;
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
            if(period > 0)
            {
                var dummyDO = new DummyDO();
                this.AnimationCollection.Add(dummyDO);

                var newda = new DoubleAnimation(0, 100, new Duration(TimeSpan.FromMilliseconds(period / multiplier)));
                newda.RepeatBehavior = RepeatBehavior.Forever;
                Storyboard.SetTarget(newda, dummyDO);
                Storyboard.SetTargetProperty(newda, new PropertyPath(DummyDO.AnimationPositionProperty));
                storyboard.Children.Add(newda);
            }

        }

        private Storyboard sb { get; set; }

        private double _period;
        public double Period
        {
            get => _period;
            set
            {
                SetAndNotify(ref _period, value);
                MakeCollection(sb);
            }
        }


        private double _beatTick;
        public double BeatTick
        {
            get => _beatTick;
            set
            {
                SetAndNotify(ref _beatTick, value);
                Console.WriteLine("BeatTick Changed");
            }
        }


        private ObservableCollection<DummyDO> _animationCollection;
        public ObservableCollection<DummyDO> AnimationCollection
        {
            get => _animationCollection;
            set => SetAndNotify(ref _animationCollection, value);
        }
    }
}
