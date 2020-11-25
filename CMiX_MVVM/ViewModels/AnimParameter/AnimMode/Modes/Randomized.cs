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
            //this.SetViewModel(message.Obj as RandomizedModel);
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

        public void UpdateOnBeatTick(AnimParameter animParameter, double period)
        {
            oldRandom = newRandom;
            newRandom = GetNewRandoms(animParameter.Counter.Count);
        }

        public void UpdateParameters(AnimParameter animParameter, double period)
        {
            for (int i = 0; i < animParameter.Parameters.Length; i++)
            {
                animParameter.Parameters[i] = Utils.Map(Utils.Lerp(oldRandom[i], newRandom[i], Easings.Interpolate((float)period, animParameter.Easing.SelectedEasing)), 0.0, 1.0, animParameter.Range.Minimum, animParameter.Range.Maximum);
            }
        }
    }
}