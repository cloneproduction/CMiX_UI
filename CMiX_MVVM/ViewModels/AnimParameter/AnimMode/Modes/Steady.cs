using CMiX.MVVM.Resources;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class Steady : AnimMode
    {
        public Steady(AnimParameter animParameter)
        {
            SteadyType = SteadyType.Linear;
            LinearType = LinearType.Center;
            Seed = 0;
            AnimParameter = animParameter;
            SetSpread();
        }

        public Steady(AnimParameter animParameter, Sendable parentSendable) : this(animParameter)
        {
            SubscribeToEvent(parentSendable);
        }

        public override void UpdateOnBeatTick(double period)
        {

        }

        public override double[] UpdatePeriod(double period)
        {
            return AnimParameter.Spread;
        }

        public void SetSpread()
        {
            AnimParameter.Spread = new double[AnimParameter.Counter.Count];
            double offset = AnimParameter.Range.Distance / AnimParameter.Counter.Count;
            double startValue;

            if (SteadyType == SteadyType.Linear)
            {
                if(LinearType == LinearType.Left)
                {
                    startValue = 0.0;
                    for (int i = 0; i < AnimParameter.Spread.Length; i++)
                    {
                        AnimParameter.Spread[i] = startValue;
                        startValue += offset;
                    }
                    return;
                }

                if (LinearType == LinearType.Right)
                {
                    startValue = AnimParameter.Range.Distance;
                    for (int i = 0; i < AnimParameter.Spread.Length; i++)
                    {
                        AnimParameter.Spread[i] = startValue;
                        startValue -= offset;
                    }
                    return;
                }

                if (LinearType == LinearType.Center)
                {
                    startValue = AnimParameter.Range.Distance;
                    for (int i = 0; i < AnimParameter.Spread.Length; i++)
                    {
                        AnimParameter.Spread[i] = startValue;
                        startValue -= offset;
                    }
                    return;
                }
            }

            if(SteadyType == SteadyType.Random)
            {
                var random = new Random(Seed);
                for (int i = 0; i < AnimParameter.Spread.Length; i++)
                {
                    AnimParameter.Spread[i] = Utils.Map(random.NextDouble(), 0.0, 1.0, AnimParameter.Range.Minimum, AnimParameter.Range.Maximum);
                }
                return;
            }
        }


        private int _seed;
        public int Seed
        {
            get => _seed;
            set
            {
                SetAndNotify(ref _seed, value);
                SetSpread();
            }
        }

        private SteadyType _steadyType;
        public SteadyType SteadyType
        {
            get => _steadyType;
            set
            {
                SetAndNotify(ref _steadyType, value);
                SetSpread();
            }
        }

        private LinearType _linearType;
        public LinearType LinearType
        {
            get => _linearType;
            set
            {
                SetAndNotify(ref _linearType, value);
                SetSpread();
            }
        }
    }
}