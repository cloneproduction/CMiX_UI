using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Animation;
using CMiX.MVVM.Controls;

namespace CMiX.MVVM.ViewModels
{
    public class BeatAnimations
    {
        public BeatAnimations(double period)
        {
            AnimatedDoubles = new ObservableCollection<AnimatedDouble>();
            Storyboard = new Storyboard();
            MakeCollection(period);
        }

        private ObservableCollection<AnimatedDouble> _animatedDoubles;
        public ObservableCollection<AnimatedDouble> AnimatedDoubles
        {
            get => _animatedDoubles;
            set => _animatedDoubles = value;
        }

        private Storyboard Storyboard { get; set; }
        private double step = 16.0;

        public void MakeCollection(double period)
        {
            Storyboard.Children.Clear();
            AnimatedDoubles.Clear();

            double Multiplier = 1;

            for (int i = 1; i < step; i++)
            {
                Storyboard.Children.Add(CreateAnimation(Multiplier / 128, period));
                Multiplier *= 2;
            }

            Storyboard.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard.Begin();
        }

        private DoubleAnimation CreateAnimation(double multiplier, double period)
        {
            var animatedDouble = new AnimatedDouble();
            AnimatedDoubles.Add(animatedDouble);
            DoubleAnimation newda = new DoubleAnimation();

            if(multiplier > 0 && period > 0)
            {
                newda.From = 1;
                newda.To = 0;
                QuadraticEase easing = new QuadraticEase();  // or whatever easing class you want
                easing.EasingMode = System.Windows.Media.Animation.EasingMode.EaseOut;
                newda.EasingFunction = easing;
                newda.Duration = new Duration(TimeSpan.FromMilliseconds(period / multiplier));
                newda.RepeatBehavior = RepeatBehavior.Forever;
            }

            Storyboard.SetTarget(newda, animatedDouble);
            Storyboard.SetTargetProperty(newda, new PropertyPath(AnimatedDouble.AnimationPositionProperty));
            return newda;
        }

        public void ResetAnimation()
        {
            Storyboard.Stop();
            Storyboard.Begin();
        }
    }
}