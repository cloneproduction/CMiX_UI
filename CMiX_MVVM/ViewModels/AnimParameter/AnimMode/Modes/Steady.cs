using CMiX.MVVM.Models;
using CMiX.MVVM.Services;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class Steady : AnimMode
    {
        public Steady(AnimParameter animParameter)
        {
            SteadyType = SteadyType.Linear;
            AnimParameter = animParameter;
        }

        public Steady(AnimParameter animParameter, Sendable parentSendable) : this(animParameter)
        {
            SubscribeToEvent(parentSendable);
        }

        public override void UpdateOnBeatTick(double period)
        {

        }

        public double[] Pouet()
        {
            double[] prout = new double[AnimParameter.Counter.Count];

            var pouet = AnimParameter.Range.Distance / AnimParameter.Counter.Count;


            
            if (SteadyType == SteadyType.Linear)
            {
                for (int i = 0; i < AnimParameter.Counter.Count; i++)
                {
                    prout[i] = 0.0;
                }
            }

            return prout;
        }

        public override double UpdatePeriod(double period)
        {
            return AnimParameter.DefaultValue;
        }

        public Random Random { get; set; }

        private SteadyType _steadyType;
        public SteadyType SteadyType
        {
            get => _steadyType;
            set => SetAndNotify(ref _steadyType, value);
        }
    }
}
