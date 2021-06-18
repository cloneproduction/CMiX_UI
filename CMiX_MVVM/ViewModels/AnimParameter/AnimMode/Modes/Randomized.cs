using CMiX.MVVM.Interfaces;
using CMiX.MVVM.MessageService;
using CMiX.MVVM.Models;
using CMiX.MVVM.Resources;
using CMiX.MVVM.Tools;
using CMiX.MVVM.ViewModels.Beat;
using System;
using System.Windows.Media.Media3D;

namespace CMiX.MVVM.ViewModels
{
    public class Randomized : ViewModel, IControl, IAnimMode
    {
        public Randomized(AnimParameter parentSender, RandomizedModel randomizedModel)
        {
            Random = new Random();
            oldRandom = GetNewRandoms(parentSender.Parameters.Length);
            newRandom = GetNewRandoms(parentSender.Parameters.Length);
        }


        public Guid ID { get; set; }
        private Random Random { get; set; }
        public int Count { get; set; }
        public Vector3D[] Location { get; set; }
        public Vector3D[] Scale { get; set; }
        public Vector3D[] Rotation { get; set; }
        public ControlCommunicator Communicator { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

        public void SetViewModel(IModel model)
        {
            throw new NotImplementedException();
        }

        public IModel GetModel()
        {
            RandomizedModel model = new RandomizedModel();
            return model;
        }

        public void SetCommunicator(Communicator communicator)
        {
            throw new NotImplementedException();
        }

        public void UnsetCommunicator(Communicator communicator)
        {
            throw new NotImplementedException();
        }
    }
}