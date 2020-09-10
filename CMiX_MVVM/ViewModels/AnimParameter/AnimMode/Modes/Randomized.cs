using CMiX.MVVM.Resources;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class Randomized : AnimMode
    {
        public Randomized()
        {
            Random = new Random();
            newRandom = Random.NextDouble();
        }

        public Randomized(Sendable parentSendable) : this()
        {
            SubscribeToEvent(parentSendable);
        }

        private Random Random { get; set; }

        private double oldRandom;
        private double newRandom;

        public override void UpdateOnBeatTick(AnimParameter animParameter, double period)
        {
            oldRandom = newRandom;
            newRandom = Random.NextDouble();
        }

        public override void UpdateParameters(AnimParameter animParameter, double period)
        {
            for (int i = 0; i < animParameter.Parameters.Length; i++)
            {
                animParameter.Parameters[i] = Utils.Map(Utils.Lerp(oldRandom, newRandom, Easings.Interpolate((float)period, animParameter.Easing.SelectedEasing)), 0.0, 1.0, animParameter.Range.Minimum, animParameter.Range.Maximum);
            }
        }
    }
}