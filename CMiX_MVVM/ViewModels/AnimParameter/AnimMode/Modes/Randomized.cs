using CMiX.MVVM.Resources;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class Randomized : AnimMode
    {
        public Randomized(AnimParameter animParameter)
        {
            Random = new Random();
            newRandom = GetNewRandoms(animParameter.Counter.Count);
            SubscribeToEvent(animParameter);
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

        public override void UpdateOnBeatTick(AnimParameter animParameter, double period)
        {
            oldRandom = newRandom;
            newRandom = GetNewRandoms(animParameter.Counter.Count);
        }

        public override void UpdateParameters(AnimParameter animParameter, double period)
        {
            for (int i = 0; i < animParameter.Parameters.Length; i++)
            {
                animParameter.Parameters[i] = Utils.Map(Utils.Lerp(oldRandom[i], newRandom[i], Easings.Interpolate((float)period, animParameter.Easing.SelectedEasing)), 0.0, 1.0, animParameter.Range.Minimum, animParameter.Range.Maximum);
            }
        }
    }
}