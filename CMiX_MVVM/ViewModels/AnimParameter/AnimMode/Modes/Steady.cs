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
            SubscribeToEvent(animParameter);
        }

        public override void UpdateOnBeatTick(AnimParameter animParameter, double period)
        {
            //throw new NotImplementedException();
        }

        public override void UpdateParameters(AnimParameter animParameter, double period)
        {
            double offset = animParameter.Range.Distance / animParameter.Counter.Count;
            double startValue;

            if (SteadyType == SteadyType.Linear)
            {
                if(LinearType == LinearType.Left)
                {
                    startValue = animParameter.Range.Minimum;
                    for (int i = 0; i < animParameter.Parameters.Length; i++)
                    {
                        animParameter.Parameters[i] = startValue;
                        startValue += offset;
                    }
                    return;
                }

                if (LinearType == LinearType.Right)
                {
                    startValue = animParameter.Range.Maximum;
                    for (int i = 0; i < animParameter.Parameters.Length; i++)
                    {
                        animParameter.Parameters[i] = startValue;
                        startValue -= offset;
                    }
                    return;
                }

                if (LinearType == LinearType.Center)
                {
                    startValue = animParameter.Range.Distance;
                    for (int i = 0; i < animParameter.Parameters.Length; i++)
                    {
                        animParameter.Parameters[i] = startValue;
                        startValue -= offset;
                    }
                    return;
                }
            }

            if(SteadyType == SteadyType.Random)
            {
                var random = new Random(Seed);
                for (int i = 0; i < animParameter.Parameters.Length; i++)
                {
                    animParameter.Parameters[i] = Utils.Map(random.NextDouble(), 0.0, 1.0, animParameter.Range.Minimum, animParameter.Range.Maximum);
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
                OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }

        private SteadyType _steadyType;
        public SteadyType SteadyType
        {
            get => _steadyType;
            set
            {
                SetAndNotify(ref _steadyType, value);
                OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }

        private LinearType _linearType;
        public LinearType LinearType
        {
            get => _linearType;
            set 
            {
                SetAndNotify(ref _linearType, value);
                OnSendChange(this.GetModel(), this.GetMessageAddress());
            }
        }
    }
}