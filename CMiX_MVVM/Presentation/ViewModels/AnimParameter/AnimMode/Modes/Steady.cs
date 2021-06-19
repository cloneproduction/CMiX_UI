using CMiX.Core.Interfaces;
using CMiX.Core.MessageService;
using CMiX.Core.Models;
using CMiX.Core.Tools;
using CMiX.Core.Presentation.ViewModels.Beat;
using System;

namespace CMiX.Core.Presentation.ViewModels
{
    public class Steady : ViewModel, IControl, IAnimMode
    {
        public Steady(SteadyModel steadyModel)
        {
            this.ID = steadyModel.ID;
            SteadyType = SteadyType.Linear;
            LinearType = LinearType.Center;
            Seed = 0;
        }


        public ControlCommunicator Communicator { get; set; }
        public Guid ID { get; set; }

        private int _seed;
        public int Seed
        {
            get => _seed;
            set
            {
                SetAndNotify(ref _seed, value);

            }
        }

        private SteadyType _steadyType;
        public SteadyType SteadyType
        {
            get => _steadyType;
            set
            {
                SetAndNotify(ref _steadyType, value);

            }
        }

        private LinearType _linearType;
        public LinearType LinearType
        {
            get => _linearType;
            set
            {
                SetAndNotify(ref _linearType, value);

            }
        }


        public void UpdateOnBeatTick(double[] doubleToAnimate, double period, IRange range, Easing easing, BeatModifier beatModifier)
        {

        }

        public void UpdateOnGameLoop(double[] doubleToAnimate, double period, IRange range, Easing easing, BeatModifier beatModifier)
        {
            double offset = range.Width / doubleToAnimate.Length;
            double startValue;

            if (SteadyType == SteadyType.Linear)
            {
                if(LinearType == LinearType.Left)
                {
                    startValue = range.Width;
                    for (int i = 0; i < doubleToAnimate.Length; i++)
                    {
                        doubleToAnimate[i] = startValue;
                        startValue += offset;
                    }
                }
                else if (LinearType == LinearType.Right)
                {
                    startValue = range.Width;
                    for (int i = 0; i < doubleToAnimate.Length; i++)
                    {
                        doubleToAnimate[i] = startValue;
                        startValue -= offset;
                    }
                }
                else if (LinearType == LinearType.Center)
                {
                    startValue = range.Width;
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
                    doubleToAnimate[i] = Utils.Map(random.NextDouble(), 0.0, 1.0, 0.0 - range.Width / 2, 0.0 + range.Width / 2);
                }
            }
        }


        public void SetCommunicator(Communicator communicator)
        {
            Communicator = new ControlCommunicator(this);
            Communicator.SetCommunicator(communicator);
        }

        public void UnsetCommunicator(Communicator communicator)
        {
            Communicator.UnsetCommunicator(communicator);
        }


        public void SetViewModel(IModel model)
        {
            SteadyModel animModeModel = model as SteadyModel;
            this.ID = animModeModel.ID;
            this.SteadyType = animModeModel.SteadyType;
            this.LinearType = animModeModel.LinearType;
            this.Seed = animModeModel.Seed;
        }

        public IModel GetModel()
        {
            SteadyModel model = new SteadyModel();
            model.ID = this.ID;
            model.SteadyType = this.SteadyType;
            model.LinearType = this.LinearType;
            model.Seed = this.Seed;
            return model;
        }
    }
}