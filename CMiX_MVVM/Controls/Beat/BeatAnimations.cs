using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Animation;
using CMiX.MVVM.Controls;

namespace CMiX.MVVM.ViewModels
{
    public class BeatAnimations
    {
        public BeatAnimations()
        {
            AnimatedDoubles = new ObservableCollection<AnimatedDouble>();
            Storyboard = new Storyboard();
            //MakeStoryBoard(periods);
        }

        private ObservableCollection<AnimatedDouble> _animatedDoubles;
        public ObservableCollection<AnimatedDouble> AnimatedDoubles
        {
            get => _animatedDoubles;
            set => _animatedDoubles = value;
        }

        private Storyboard Storyboard { get; set; }

        public void MakeStoryBoard(double[] periods)
        {
            Storyboard.Children.Clear();
            AnimatedDoubles.Clear();

            for (int i = 0; i < periods.Length; i++)
            {
                Storyboard.Children.Add(CreateAnimation(periods[i]));
            }

            Storyboard.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard.Begin();
        }

        private DoubleAnimation CreateAnimation(double period)
        {
            var animatedDouble = new AnimatedDouble();
            AnimatedDoubles.Add(animatedDouble);
            DoubleAnimation newda = new DoubleAnimation();

            if(period > 0)
            {
                newda.From = 1;
                newda.To = 0;
                QuadraticEase easing = new QuadraticEase();
                easing.EasingMode = System.Windows.Media.Animation.EasingMode.EaseOut;
                newda.EasingFunction = easing;
                newda.Duration = new Duration(TimeSpan.FromMilliseconds(period));
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