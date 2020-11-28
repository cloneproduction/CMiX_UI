using CMiX.MVVM.Models;
using CMiX.MVVM.Resources;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class Randomized : Sender, IAnimMode
    {
        public Randomized(string name, IColleague parentSender) : base (name, parentSender)
        {
            Random = new Random();
            newRandom = GetNewRandoms(((AnimParameter)parentSender).Counter.Count); // UGLY
        }

        public override void Receive(Message message)
        {
            this.SetViewModel(message.Obj as RandomizedModel);
        }

        private Random Random { get; set; }

        private double[] oldRandom;
        private double[] newRandom;

        private double[] GetNewRandoms(int count)
        {
            var rands = new double[count];
            for (int i = 0; i < count; i++)
            {
                rands[i] = Random.NextDouble();
            }
            return rands;
        }

        public double[] UpdateOnBeatTick(double[] doubleToAnimate, double period, Range range, Easing easing)
        {
            oldRandom = newRandom;
            newRandom = GetNewRandoms(doubleToAnimate.Length);
            return doubleToAnimate;
        }

        public double[] UpdateOnGameLoop(double[] doubleToAnimate, double period, Range range, Easing easing)
        {
            for (int i = 0; i < doubleToAnimate.Length; i++)
            {
                doubleToAnimate[i] = Utils.Map(Utils.Lerp(oldRandom[i], newRandom[i], Easings.Interpolate((float)period, easing.SelectedEasing)), 0.0, 1.0, range.Minimum, range.Maximum);
            }
            return doubleToAnimate;
        }
    }
}