using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Animation;
using CMiX.MVVM.Controls;

namespace CMiX.MVVM.ViewModels
{
    public class BeatAnimations : ViewModel
    {
        public BeatAnimations(double period)
        {
            AnimationCollection = new ObservableCollection<AnimatedDouble>();
            Storyboard = new Storyboard();
            MakeCollection(period);
        }


        private ObservableCollection<AnimatedDouble> _animationCollection;
        public ObservableCollection<AnimatedDouble> AnimationCollection
        {
            get => _animationCollection;
            set => SetAndNotify(ref _animationCollection, value);
        }


        private Storyboard Storyboard { get; set; }
        private double step = 16.0;

        private void MakeCollection(double period)
        {
            Storyboard.Children.Clear();
            AnimationCollection.Clear();

            double Multiplier = 1.0/step;
            for (int i = 1; i < step / 2; i++)
            {
                Multiplier *= 2;
                Storyboard.Children.Add(CreateAnimation(Multiplier, period));
                Console.WriteLine("Multiplier " + Multiplier);
            }

            Storyboard.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard.Begin();
        }

        private DoubleAnimation CreateAnimation(double multiplier, double period)
        {
            var dummyDO = new AnimatedDouble();
            AnimationCollection.Add(dummyDO);
            DoubleAnimation newda = new DoubleAnimation(0, 100, new Duration(TimeSpan.FromMilliseconds(period / multiplier)));
            newda.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard.SetTarget(newda, dummyDO);
            Storyboard.SetTargetProperty(newda, new PropertyPath(AnimatedDouble.AnimationPositionProperty));
            return newda;
        }
    }
}