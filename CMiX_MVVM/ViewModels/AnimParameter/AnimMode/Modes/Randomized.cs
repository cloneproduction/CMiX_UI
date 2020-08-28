﻿using CMiX.MVVM.Models;
using CMiX.MVVM.Resources;
using CMiX.MVVM.Services;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class Randomized : AnimMode, IAnimMode
    {
        public Randomized(AnimParameter animParameter)
        {
            AnimParameter = animParameter;
            Random = new Random();
        }

        public Randomized(AnimParameter animParameter, Sendable parentSendable) : this(animParameter)
        {
            SubscribeToEvent(parentSendable);
        }

        private Random Random { get; set; }
        public AnimParameter AnimParameter { get; set; }

        private double oldRandom;
        private double newRandom;

        public override void UpdateOnBeatTick(double period)
        {
            oldRandom = newRandom;
            newRandom = Random.NextDouble();
        }

        public override double UpdatePeriod(double period, AnimParameter animParameter)
        {
            return Utils.Map(Utils.Lerp(oldRandom, newRandom, Easings.Interpolate((float)period, animParameter.Easing.SelectedEasing)), 0.0, 1.0, animParameter.Range.Minimum, animParameter.Range.Maximum);
        }
    }
}