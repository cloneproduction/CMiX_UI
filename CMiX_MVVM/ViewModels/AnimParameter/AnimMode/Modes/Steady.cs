﻿using CMiX.MVVM.Interfaces;
using CMiX.MVVM.Models;
using CMiX.MVVM.Tools;
using CMiX.MVVM.ViewModels.Beat;
using CMiX.MVVM.ViewModels.MessageService;
using System;

namespace CMiX.MVVM.ViewModels
{
    public class Steady : Module, IAnimMode
    {
        public Steady(SteadyModel steadyModel)
        {
            SteadyType = SteadyType.Linear;
            LinearType = LinearType.Center;
            Seed = 0;
        }


        public override void SetReceiver(IMessageReceiver messageReceiver)
        {
            //messageDispatcher.RegisterMessageProcessor(this);
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

        public override void SetViewModel(IModel model)
        {
            SteadyModel animModeModel = model as SteadyModel;
            this.SteadyType = animModeModel.SteadyType;
            this.LinearType = animModeModel.LinearType;
            this.Seed = animModeModel.Seed;
        }

        public override IModel GetModel()
        {
            SteadyModel model = new SteadyModel();
            model.SteadyType = this.SteadyType;
            model.LinearType = this.LinearType;
            model.Seed = this.Seed;
            return model;
        }
    }
}