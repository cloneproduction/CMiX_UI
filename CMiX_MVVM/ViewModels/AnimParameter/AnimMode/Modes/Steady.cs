﻿using CMiX.MVVM.Services;

namespace CMiX.MVVM.ViewModels
{
    public class Steady : Sendable, IAnimMode
    {
        public Steady()
        {
            SteadyType = SteadyType.Linear;
        }

        public Steady(Sendable parentSendable) : this()
        {
            SubscribeToEvent(parentSendable);
        }

        public override void OnParentReceiveChange(object sender, ModelEventArgs e)
        {

        }

        private SteadyType _steadyType;
        public SteadyType SteadyType
        {
            get => _steadyType;
            set => SetAndNotify(ref _steadyType, value);
        }
    }
}
