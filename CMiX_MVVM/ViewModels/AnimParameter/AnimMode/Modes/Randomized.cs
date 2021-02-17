using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Resources;
using CMiX.MVVM.Tools;
using System;
using System.Windows.Media.Media3D;

namespace CMiX.MVVM.ViewModels
{
    public class Randomized : Sender, IAnimMode
    {
        public Randomized(string name, Sender parentSender) : base(name, parentSender)
        {

        }
        public Randomized(string name, AnimParameter parentSender) : base(name, parentSender)
        {
            Random = new Random();
            oldRandom = GetNewRandoms(parentSender.Parameters.Length);
            newRandom = GetNewRandoms(parentSender.Parameters.Length);
        }

        private Random Random { get; set; }
        public int Count { get; set; }
        public Vector3D[] Location { get; set; }
        public Vector3D[] Scale { get; set; }
        public Vector3D[] Rotation { get; set; }

        private double[] oldRandom;
        private double[] newRandom;

        private double[] GetNewRandoms(int count)
        {
            var rands = new double[count];

            for (int i = 0; i < count; i++)
                rands[i] = Random.NextDouble();

            return rands;
        }

        public void UpdateOnBeatTick(double[] doubleToAnimate, double period, IRange range, Easing easing, BeatModifier beatModifier)
        {
            oldRandom = newRandom;
            if (beatModifier.CheckHitOnBeatTick())
                newRandom = GetNewRandoms(doubleToAnimate.Length);
        }

        public void UpdateOnGameLoop(double[] doubleToAnimate, double period, IRange range, Easing easing, BeatModifier beatModifier)
        {
            bool ease = easing.IsEnabled;

            for (int i = 0; i < doubleToAnimate.Length; i++)
            {
                if (ease)
                {
                    double eased = Easings.Interpolate((float)period, easing.SelectedEasing);
                    double lerped = Utils.Lerp(oldRandom[i], newRandom[i], eased);
                    doubleToAnimate[i] = Utils.Map(lerped, 0.0, 1.0, 0.0 - range.Width / 2, 0.0 + range.Width / 2);
                }
                else
                {
                    doubleToAnimate[i] = Utils.Map(newRandom[i], 0.0, 1.0, 0.0 - range.Width / 2, 0.0 + range.Width / 2);
                }
            }
        }

        public override void SetViewModel(IModel model)
        {
            throw new NotImplementedException();
        }

        public override IModel GetModel()
        {
            RandomizedModel model = new RandomizedModel();
            return model;
        }
    }
}