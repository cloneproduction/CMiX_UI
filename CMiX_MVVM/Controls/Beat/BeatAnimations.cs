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

            double Multiplier = 1.0/step;

            for (int i = 1; i < step / 2; i++)
            {
                Multiplier *= 2;
                Console.WriteLine("Multiplier " + Multiplier);
                Storyboard.Children.Add(CreateAnimation(Multiplier, period));
            }

            Storyboard.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard.Begin();
        }

        private DoubleAnimation CreateAnimation(double multiplier, double period)
        {
            var animatedDouble = new AnimatedDouble();
            AnimatedDoubles.Add(animatedDouble);
            DoubleAnimation newda = new DoubleAnimation(0, 100, new Duration(TimeSpan.FromMilliseconds(period / multiplier)));
            newda.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard.SetTarget(newda, animatedDouble);
            Storyboard.SetTargetProperty(newda, new PropertyPath(AnimatedDouble.AnimationPositionProperty));
            return newda;
        }
    }
}