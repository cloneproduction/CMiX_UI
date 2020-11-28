using CMiX.MVVM.Resources;
using CMiX.MVVM.Services;
using CMiX.MVVM.ViewModels.Mediator;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class Steady : Sender, IAnimMode
    {
        public Steady(string name, IColleague parentSender) : base (name, parentSender)
        {
            SteadyType = SteadyType.Linear;
            LinearType = LinearType.Center;
            Seed = 0;
        }

        public override void Receive(Message message)
        {
            //this.SetViewModel()
        }

        public double[] UpdateOnBeatTick(double[] doubleToAnimate, double period, Range range, Easing easing)
        {
            return doubleToAnimate;
        }

        public double[] UpdateOnGameLoop(double[] doubleToAnimate, double period, Range range, Easing easing)
        {
            double offset = range.Distance / doubleToAnimate.Length;
            double startValue;

            if (SteadyType == SteadyType.Linear)
            {
                if(LinearType == LinearType.Left)
                {
                    startValue = range.Minimum;
                    for (int i = 0; i < doubleToAnimate.Length; i++)
                    {
                        doubleToAnimate[i] = startValue;
                        startValue += offset;
                    }
                }
                else if (LinearType == LinearType.Right)
                {
                    startValue = range.Maximum;
                    for (int i = 0; i < doubleToAnimate.Length; i++)
                    {
                        doubleToAnimate[i] = startValue;
                        startValue -= offset;
                    }
                }
                else if (LinearType == LinearType.Center)
                {
                    startValue = range.Distance;
                    for (int i = 0; i < doubleToAnimate.Length; i++)
                    {
                        doubleToAnimate[i] = startValue;
                        startValue -= offset;
                    }
                }
            }

            if(SteadyType == SteadyType.Random)
            {
                var random = new Random(Seed);
                for (int i = 0; i < doubleToAnimate.Length; i++)
                {
                    doubleToAnimate[i] = Utils.Map(random.NextDouble(), 0.0, 1.0, range.Minimum, range.Maximum);
                }
            }

            return doubleToAnimate;
        }

        private int _seed;
        public int Seed
        {
            get => _seed;
            set
            {
                SetAndNotify(ref _seed, value);
                this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.Address, this.GetModel()));
            }
        }

        private SteadyType _steadyType;
        public SteadyType SteadyType
        {
            get => _steadyType;
            set
            {
                SetAndNotify(ref _steadyType, value);
                this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.Address, this.GetModel()));
            }
        }

        private LinearType _linearType;
        public LinearType LinearType
        {
            get => _linearType;
            set 
            {
                SetAndNotify(ref _linearType, value);
                this.Send(new Message(MessageCommand.UPDATE_VIEWMODEL, this.Address, this.GetModel()));
            }
        }
    }
}