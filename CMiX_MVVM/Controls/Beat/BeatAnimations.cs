using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Animation;
using CMiX.MVVM.Controls;

namespace CMiX.MVVM.ViewModels
{
    public class BeatAnimations : ViewModel
    {
        public BeatAnimations()
        {
            AnimationCollection = new ObservableCollection<AnimatedDouble>();
            Storyboard = new Storyboard();
            MakeCollection(Storyboard);
        }

        private double _period;
        public double Period
        {
            get => _period;
            set
            {
                SetAndNotify(ref _period, value);
                MakeCollection(Storyboard);
            }
        }

        private Storyboard Storyboard { get; set; }

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
            if (period > 0)
            {
                var dummyDO = new AnimatedDouble();
                this.AnimationCollection.Add(dummyDO);

                var newda = new DoubleAnimation(0, 100, new Duration(TimeSpan.FromMilliseconds(period / multiplier)));
                newda.RepeatBehavior = RepeatBehavior.Forever;
                Storyboard.SetTarget(newda, dummyDO);
                Storyboard.SetTargetProperty(newda, new PropertyPath(AnimatedDouble.AnimationPositionProperty));
                storyboard.Children.Add(newda);
            }
        }

        private ObservableCollection<AnimatedDouble> _animationCollection;
        public ObservableCollection<AnimatedDouble> AnimationCollection
        {
            get => _animationCollection;
            set => SetAndNotify(ref _animationCollection, value);
        }
    }
}